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

    Vector2 coordinates = new Vector2();

    List<GameObject> spawners = new List<GameObject>();

    public Cell cellType;

    /*
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("activeplayer"))
        {
            gameObject.SetActive(true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("activeplayer"))
        {
            gameObject.SetActive(false);
        }
    }
    */
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


       //gameObject.SetActive(false);
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
    public void SetActiveSpawners(bool enable)
    {
        foreach (GameObject item in spawners)
        {
            item.SetActive(enable);
        }
        
    }
    public void SetCoordinates(Vector2 x)
    {
        coordinates = x;

        //Create debug coordinates
        /*
        GameObject xx = new GameObject();
        TextMesh tm = xx.AddComponent<TextMesh>();
        tm.text = coordinates.ToString();
        tm.transform.SetParent(gameObject.transform);
        tm.transform.position = gameObject.transform.position;
        */

    }
    public Vector2 GetCoordinates()
    {
        return coordinates;
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
    public void CreateSpawnerOnOff(bool horizontal)
    {

        Bounds mybds = GetComponent<BoxCollider>().bounds;

        Vector2 mydir = Vector2.right;
        if (!horizontal)
        {
            mydir = Vector2.up;
            for (int i = 0; i <= mybds.size.x + 1; i++)
            {
                GameObject spa = Instantiate(spawner);
                SpawnController spawn = spa.GetComponent<SpawnController>();

                spawn.dir = mydir;


                float xv = transform.position.x - (int)(mybds.size.x  / 2) + ((i) - 0.5f);
                float zv = transform.position.z - ((mybds.size.z * mydir.y / 2));
                float yv = 0f;
                spa.transform.position = new Vector3(xv, yv, zv);

                spa.transform.parent = transform;
                mydir = -mydir;
                spawners.Add(spa);
            }

        }
        else
        {
            for (int i = 0; i <= mybds.size.x+1; i++)
            {
                GameObject spa = Instantiate(spawner);
                SpawnController spawn = spa.GetComponent<SpawnController>();

                spawn.dir = mydir;


                float xv = transform.position.x - ((mybds.size.x*mydir.x / 2));
                float yv = 0f;
                float zv = transform.position.z - (int)(mybds.size.z / 2) + ((i) - 0.5f);
                spa.transform.position = new Vector3(xv, yv, zv);

                spa.transform.parent = transform;
                mydir = -mydir;

                spawners.Add(spa);
            }
        }
        SetActiveSpawners(false);



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
