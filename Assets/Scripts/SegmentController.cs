using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SegmentController : MonoBehaviour
{
    int xSize;
    int zSize;

    public GameObject groundCollider;
    public GameObject spawner;

    GameObject myGroundCollider;


    // Start is called before the first frame update
    void Start()
    {
        BoxCollider myBcc = GetComponent<BoxCollider>();
        gameObject.tag = "ground";

        xSize = (int) transform.localScale.x;
        zSize = (int)transform.localScale.z;

        //get bottom left corner

        ///*
        myGroundCollider = Instantiate(groundCollider);
        myGroundCollider.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        myGroundCollider.transform.position = transform.position;
        myGroundCollider.GetComponent<BoxCollider>().size = new Vector3(1.1f, 1, 1.1f);

        //Starter(new Vector2(0,5), Vector2.right);
        //Starter(new Vector2(xSize, 3), Vector2.left);

        //gbb.GetComponent<BoxCollider>().bounds.


        /*GameObject gb = Instantiate(vehicleObject);

        Bounds bds = gb.GetComponent<BoxCollider>().bounds;
        Bounds mybds = gbb.GetComponent<BoxCollider>().bounds;


        float xv = transform.position.x - (mybds.size.x / 2) - (bds.size.x/2);
        float yv = 0f;
        float zv = transform.position.z + 0.5f - (int)(zSize / 2);


        Vector3 loc = new Vector3(xv,yv, zv);

        gb.transform.position = loc;
        VehicleController vc = gb.GetComponent<VehicleController>();
        vc.bc = myBcc;
        vc.parentObject = gbb;
        //*/


    }


    public void CreateSpawner(Vector2 start, Vector2 mydir)
    {
        GameObject spa = Instantiate(spawner);
        Bounds mybds = GetComponent<BoxCollider>().bounds;
        SpawnController spawn = spa.GetComponent<SpawnController>();
        spawn.dir = mydir;

        float xv = transform.position.x - (mybds.size.x / 2) + (-mydir.x*1f) +start.x;
        float yv = 0f;
        float zv = transform.position.z - (int)(zSize / 2)+.5f + start.y;
        spa.transform.position = new Vector3(xv, yv, zv);
        spa.transform.parent = transform;

    }

    // Update is called once per frame
    void Update()
    {
        /*
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnDuration)
        {
            BoxCollider myBcc = GetComponent<BoxCollider>();

            GameObject gb = Instantiate(vehicleObject);

            Bounds bds = gb.GetComponent<BoxCollider>().bounds;
            Bounds mybds = GetComponent<BoxCollider>().bounds;


            float xv = transform.position.x - (mybds.size.x / 2) - (bds.size.x / 2);
            float yv = 0f;
            float zv = transform.position.z - 1.5f + (int)(zSize / 2);


            Vector3 loc = new Vector3(xv, yv, zv);

            gb.transform.position = loc;
            VehicleController vc = gb.GetComponent<VehicleController>();
            VehicleControllerNew vcc = gb.GetComponent<VehicleControllerNew>();
            vcc.lerpDuration = spawnDuration * gap;
            vcc.moveDuration = spawnDuration * gap;
            vc.bc = myBcc;
            vc.parentObject = gbb;
            spawnTimer = 0f;
        }
        */
    }
}
