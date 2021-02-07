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

    BoxCollider bc;

    Vector3 initialPosition = new Vector3();

    public GameObject detectorCube;

    GameObject snapObject = null;
    Vector3 snapPos = new Vector3(0,0,0);

    Dictionary<Vector2, GameObject> dcmap = new Dictionary<Vector2, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //This is a tghing?
        lerpDuration = 1f / unitsPerSecond;
        aVector = transform.position;
        bVector = aVector;

        initialPosition = transform.position;

        bc = GetComponent<BoxCollider>();

        dcmap.Add(Vector2.up, Instantiate(detectorCube));
        dcmap.Add(Vector2.down, Instantiate(detectorCube));
        dcmap.Add(Vector2.left, Instantiate(detectorCube));
        dcmap.Add(Vector2.right, Instantiate(detectorCube));


        foreach (var item in dcmap)
        {
            item.Value.transform.position = transform.position + new Vector3(item.Key.x, 0, item.Key.y);
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
            transform.position = initialPosition;
            bVector = initialPosition;
            aVector = initialPosition;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (snapObject != null)
        {
            transform.position = snapObject.transform.position + snapPos;
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
            foreach (var item in dcmap)
            {
                item.Value.transform.position = transform.position + new Vector3(item.Key.x, 0, item.Key.y);
            }
        }


    }

    bool Detect(Vector2 v)
    {
        bool ret = false;
        return ret;
    }

    void Spawn(Vector2 v)
    {
        DetectorController dcCube = dcmap[v].GetComponent<DetectorController>();
        //if (dcce.GetComponent<DetectorController>().doesit == true)
        if(dcCube.doesit == true)
        {

            aVector = transform.position;
            bVector = transform.position + new Vector3(v.x, 0, v.y);
            lerpTimer = 0f;
            snapObject = null;
            
        }
        if (dcCube.SnapObject() != null)
        {
            snapObject = dcCube.SnapObject();
            snapPos = snapObject.transform.InverseTransformPoint(dcCube.transform.position);
            aVector = transform.position;
            bVector = snapObject.transform.position + snapPos;
            //grab center of dcCube
        }
    }
}
