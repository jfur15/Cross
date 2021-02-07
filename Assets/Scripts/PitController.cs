using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitController : MonoBehaviour
{
    int xSize;
    int zSize;
    public GameObject enemyGround;
    public GameObject spawner;
    public GameObject plat;
    // Start is called before the first frame update
    void Start()
    {

        BoxCollider myBcc = GetComponent<BoxCollider>();

        xSize = (int)transform.localScale.x;
        zSize = (int)transform.localScale.z;
        GameObject gbb = Instantiate(enemyGround);
        gbb.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        gbb.transform.position = transform.position;
        gbb.GetComponent<BoxCollider>().size = new Vector3(1.1f, 1, 1.1f);

        Starter(new Vector2(xSize, 0), Vector2.left);
        Starter(new Vector2(0, 5), Vector2.right);
        Starter(new Vector2(xSize, 4), Vector2.left);
        Starter(new Vector2(0, 3), Vector2.right);
        Starter(new Vector2(xSize, 2), Vector2.left);
        Starter(new Vector2(0, 1), Vector2.right);
    }

    void Starter(Vector2 start, Vector2 mydir)
    {

        GameObject spa = Instantiate(spawner);
        Bounds mybds = GetComponent<BoxCollider>().bounds;
        SpawnController spawn = spa.GetComponent<SpawnController>();
        spawn.dir = mydir;
        spawn.vehicleObject = plat;
        float xv = transform.position.x - (mybds.size.x / 2) + (-mydir.x * 1f) + start.x;
        float yv = -1f;
        float zv = transform.position.z - (int)(zSize / 2) + .5f + start.y;
        spa.transform.position = new Vector3(xv, yv, zv);
        spa.transform.parent = transform;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
