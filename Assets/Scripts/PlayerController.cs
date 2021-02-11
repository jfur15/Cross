using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int units = 1;

    Vector3 aVector = new Vector3();
    Vector3 bVector = new Vector3();

    float lerpTimer = 0f;
    public int unitsPerSecond = 5;
    float lerpDuration = 0f;
    int pitCounter = 0;
    int platCounter = 0;


    Vector3 initialPosition = new Vector3();

    public GameObject detectorCube;

    GameObject snapObject = null;
    Vector3 snapPos = new Vector3(0,0,0);

    Dictionary<Vector2, GameObject> dcmap = new Dictionary<Vector2, GameObject>();
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

        }
        /*
        dcceUp.transform.position = transform.position + new Vector3(Vector2.up.x, 0, Vector2.up.y);
        dcceDown.transform.position = transform.position + new Vector3(Vector2.down.x, 0, Vector2.down.y);
        dcceLeft.transform.position = transform.position + new Vector3(Vector2.left.x, 0, Vector2.left.y);
        dcceRight.transform.position = transform.position + new Vector3(Vector2.right.x, 0, Vector2.right.y);
        */

        //dc.transform.position = transform.position + new Vector3(v.x, 0, v.y);

        //DetectorController dcc = dc.GetComponent<DetectorController>();

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

        }
        if (other.CompareTag("platform"))
        {
            platCounter++;
        }
        if (other.CompareTag("pit"))
        {
            pitCounter++;
            //doesit = true;
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
        Debug.Log("Pit : " + pitCounter);
        Debug.Log("Plat: " + platCounter);
        if (Input.GetKey(KeyCode.Space))
        {
            kill();
        }
        if (snapObject != null)
        {
            Vector3 go = snapObject.transform.position + snapPos;
            go.y = transform.position.y;
            transform.position = go;
        }
        if (lerpTimer > lerpDuration)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Spawn(Vector2.right);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Spawn(Vector2.left);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Spawn(Vector2.up);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Spawn(Vector2.down);
            }
        }

        if (lerpTimer <= lerpDuration)
        {
            lerpTimer += Time.deltaTime;
            float x = Mathf.Lerp(aVector.x, bVector.x, lerpTimer / lerpDuration);
            float y = Mathf.Lerp(aVector.y, bVector.y, lerpTimer / lerpDuration);
            float z = Mathf.Lerp(aVector.z, bVector.z, lerpTimer / lerpDuration);

            transform.position = new Vector3(x, y, z);
        }
        if(lerpTimer > lerpDuration)
        {
            if (pit)
            {
                if (!platform)
                {
                    if (snapObject==null)
                    {

                        kill();
                    }
                }
            }
            //check if bottom has object!
            //if not, KILL
            //if so, ATTACH
        }

        foreach (var item in dcmap)
        {
            item.Value.transform.position = transform.position + new Vector3(item.Key.x, 0, item.Key.y);
        }

    }


    void Spawn(Vector2 v)
    {
        DetectorController dcCube = dcmap[v].GetComponent<DetectorController>();

        if (dcCube.doesit == true)
        {

            aVector = transform.position;
            bVector = transform.position + new Vector3(v.x, 0, v.y);
            lerpTimer = 0f;
            snapObject = null;

        }

        if (dcCube.pit == true && dcCube.SnapObject() == null)
        {

            aVector = transform.position;
            bVector = transform.position + new Vector3(v.x, 0, v.y);
            lerpTimer = 0f;
            snapObject = null;
        }

        if (dcCube.SnapObject() != null)
        {
            snapObject = dcCube.SnapObject();
            //snapPos = snapObject.transform.InverseTransformPoint(dcCube.transform.position);

            //get snapobject size
            //

            float xx = Math.Abs(snapObject.transform.position.x - dcCube.transform.position.x) * -Math.Sign(snapObject.transform.position.x - dcCube.transform.position.x);
            float zz = Math.Abs(snapObject.transform.position.z - dcCube.transform.position.z) * -Math.Sign(snapObject.transform.position.z - dcCube.transform.position.z);
            float gap = snapObject.GetComponent<VehicleController>().GetSpeed() * lerpDuration;
            if (Math.Max(snapObject.transform.localScale.x, snapObject.transform.localScale.z) % 2 == 0)
            {

                if (snapObject.transform.localScale.x < snapObject.transform.localScale.z)
                {
                    zz = (float)Math.Round(zz)+ gap;
                    xx = (float)Math.Round(xx);
                }
                else
                {
                    xx = (float)Math.Round(xx) + gap;
                    zz = (float)Math.Round(zz);

                }

            }
            else
            {
                if (snapObject.transform.localScale.x < snapObject.transform.localScale.z)
                {
                    zz = (float)Math.Floor(zz) + 0.5f + gap;
                    xx = (float)Math.Round(xx);
                }
                else
                {
                    xx = (float)Math.Floor(xx) + 0.5f+gap;
                    zz = (float)Math.Round(zz);

                }

            }

            //ADD obstacle SPEED PLUS CHARAC SPEED


            snapPos = new Vector3(xx, transform.position.y, zz);
            aVector = transform.position;
            //bVector = dcCube.transform.position;
            bVector = new Vector3(snapObject.transform.position.x, 0, snapObject.transform.position.z) + snapPos;
            lerpTimer = 0f;
        }

        /*
        if (dcCube.SnapObject() != null)
        {
            snapObject = dcCube.SnapObject();
            //snapPos = snapObject.transform.InverseTransformPoint(dcCube.transform.position);

            float xx = Math.Abs(snapObject.transform.position.x - dcCube.transform.position.x) * -Math.Sign(snapObject.transform.position.x - dcCube.transform.position.x);
            float zz = Math.Abs(snapObject.transform.position.z - dcCube.transform.position.z) * -Math.Sign(snapObject.transform.position.z - dcCube.transform.position.z);
            snapPos = new Vector3(xx, transform.position.y, zz);
            aVector = transform.position;
            //bVector = dcCube.transform.position;
            bVector = new Vector3(snapObject.transform.position.x, 0, snapObject.transform.position.z) + snapPos;
            lerpTimer = 0f;
        }*/

    }
}
