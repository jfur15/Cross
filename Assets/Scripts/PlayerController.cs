using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Avector is the original, bvector is the vector to lerp to
    Vector3 aVector = new Vector3();
    Vector3 bVector = new Vector3();

    float lerpTimer = 0f;
    float lerpDuration = 0f;

    public int unitsPerSecond = 5;

    int pitCounter = 0;
    int platCounter = 0;

    //Debug variable.
    Vector3 initialPosition = new Vector3();

    //Prefab for detecting ground on all four sides
    public GameObject detectorCube;

    //Contains the current platform to snap to
    GameObject snapObject = null;
    Vector3 snapPos = new Vector3(0,0,0);

    Dictionary<Vector2, GameObject> dcmap = new Dictionary<Vector2, GameObject>();
    Dictionary<Vector2, DetectorController> dcdcmap = new Dictionary<Vector2, DetectorController>();
    public bool platform
    {
        get
        {
            if (platCounter > 0)
            {
                return true;
            }
            return false;
        }
    }
    public bool pit
    {
        get
        {
            if (pitCounter > 0)
            {
                return true;
            }
            return false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //lerpDuration is the time in seconds for movement
        lerpDuration = 1f / unitsPerSecond;

        //We set aVector and bVector to your initial position
        aVector = transform.position;
        bVector = aVector;


        //We cache the initial position
        initialPosition = transform.position;

        //We create the four detectorcubes
        dcmap.Add(Vector2.up, Instantiate(detectorCube));
        dcmap.Add(Vector2.down, Instantiate(detectorCube));
        dcmap.Add(Vector2.left, Instantiate(detectorCube));
        dcmap.Add(Vector2.right, Instantiate(detectorCube));


        foreach (var item in dcmap)
        {
            //Set each detectorcube to the corresponding direction
            item.Value.transform.position = transform.position + new Vector3(item.Key.x, 0, item.Key.y);

            //Toggle the collider so we reset the collision logic
            item.Value.GetComponent<Collider>().enabled = false;
            item.Value.GetComponent<Collider>().enabled = true;

            dcdcmap.Add(item.Key, item.Value.GetComponent<DetectorController>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            kill();
        }

        if (other.CompareTag("ground"))
        {
            snapObject = null;
            platCounter = 0;
        }

        if (other.CompareTag("platform"))
        {
            platCounter++;
        }

        if (other.CompareTag("pit"))
        {
            pitCounter++;
        }
    }
    public void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("platform"))
        {
            platCounter--;
            //doesit = true;
        }
        if (other.CompareTag("pit"))
        {
             pitCounter--;
            //doesit = true;
        }
    }
    void kill()
    {
        transform.position = initialPosition;
        bVector = initialPosition;
        aVector = initialPosition;
        lerpTimer = 0f;

        snapObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (platCounter < 0)
        {
            platCounter = 0;
        }

        //Debug.Log("snapPos : " + snapPos);

        //Debug resetting
        if (Input.GetKey(KeyCode.Space))
        {
            kill();
        }

        //If we are not attached to a platform
        if (snapObject != null)
        {
            Vector3 go = snapObject.transform.position + snapPos;
            go.y = transform.position.y;
            transform.position = go;
        }

        //If we are not moving
        if (lerpTimer > lerpDuration)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Move(Vector2.right);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Move(Vector2.left);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Move(Vector2.up);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Move(Vector2.down);
            }
        }

        //If we are moving
        if (lerpTimer <= lerpDuration)
        {
            if (snapObject!=null)
            {
                Vector3 go = snapObject.transform.position + snapPos;
                go.y = transform.position.y;
                bVector = go;
            }
            lerpTimer += Time.deltaTime;
            float x = Mathf.Lerp(aVector.x, bVector.x, lerpTimer / lerpDuration);
            float y = Mathf.Lerp(aVector.y, bVector.y, lerpTimer / lerpDuration);
            float z = Mathf.Lerp(aVector.z, bVector.z, lerpTimer / lerpDuration);

            transform.position = new Vector3(x, y, z);
        }

        //If we are not moving
        if(lerpTimer > lerpDuration)
        {
            //If we are colliding with a pit object
            if (pit)
            {
                if (!platform)
                {
                    kill();
                }
            }

            //If we are colliding with a platform object
            if (platform)
            {
                if (snapObject == null)
                {
                    kill();
                }
            }

            
        }

        //Move all detector cubes along with player
        foreach (var item in dcmap)
        {
            item.Value.transform.position = transform.position + new Vector3(item.Key.x, 0, item.Key.y);
        }

    }

    void Move(Vector2 v)
    {
        DetectorController dcCube = dcdcmap[v];

        GameObject dCube = dcmap[v];

        if (dcCube.ground == true)
        {
            //Walk normally
            float xx = (float)Math.Round(dCube.transform.position.x * 2, MidpointRounding.AwayFromZero) / 2;
            float yy = (float)Math.Round(dCube.transform.position.z * 2, MidpointRounding.AwayFromZero) / 2;
            if (xx % 2 == 0)
            {
                xx += 0.5f;
            }
            if (yy % 2 == 0)
            {
                yy += 0.5f;
            }
            aVector = transform.position;
            bVector = new Vector3(xx, transform.position.y, yy);
            lerpTimer = 0f;
            snapObject = null;
        }

        if (dcCube.pit == true && dcCube.SnapObject() == null)
        {
            //Walk straight forward into the pit
            aVector = transform.position;
            bVector = transform.position + new Vector3(v.x, 0, v.y);
            lerpTimer = 0f;
            snapObject = null;
        }

        if (dcCube.SnapObject() != null)
        {
            snapObject = dcCube.SnapObject();

            float xx = Math.Abs(snapObject.transform.position.x - dcCube.transform.position.x) * -Math.Sign(snapObject.transform.position.x - dcCube.transform.position.x);
            float zz = Math.Abs(snapObject.transform.position.z - dcCube.transform.position.z) * -Math.Sign(snapObject.transform.position.z - dcCube.transform.position.z);
            
            if (Math.Max(snapObject.transform.lossyScale.x, snapObject.transform.lossyScale.z) % 2 == 0)
            {

                if (snapObject.transform.localScale.x < snapObject.transform.localScale.z)
                {
                    zz = (float)Math.Round(zz) + 0.5f;
                    xx = (float)Math.Round(xx);
                }
                else
                {
                    xx = (float)Math.Round(xx) + 0.5f;
                    zz = (float)Math.Round(zz);

                }

            }
            else
            {
                if (snapObject.transform.localScale.x < snapObject.transform.localScale.z)
                {
                    zz = (float)Math.Round(zz);
                    xx = (float)Math.Round(xx);
                }
                else
                {
                    xx = (float)Math.Round(xx);
                    zz = (float)Math.Round(zz);

                }
            }


            if (zz > snapObject.transform.lossyScale.z/2)
            {
                zz--;
            }
            if (xx > snapObject.transform.lossyScale.x/2)
            {
                xx--; 
            }

            snapPos = new Vector3(xx, transform.position.y, zz);
            aVector = transform.position;
            bVector = new Vector3(snapObject.transform.position.x, 0, snapObject.transform.position.z) + snapPos;
            lerpTimer = 0f;
        }
    }
}
