using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject vehicleObject;
    float spawnTimer = 0f;
    public float spawnDuration = 2f;
    public float gap = .5f;
    GameObject segment;
    BoxCollider segmentBox;

    public Vector2 dir = Vector2.right;
    // Start is called before the first frame update
    void Start()
    {
        segment = transform.parent.gameObject;

        segmentBox = segment.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnDuration)
        {

            GameObject gb = Instantiate(vehicleObject);

            Bounds bds = gb.GetComponent<BoxCollider>().bounds;




            Vector3 loc = transform.position;
            Vector2 dirtap = new Vector2(-dir.x * (bds.size.x / 2), -dir.y * (bds.size.z / 2));
            gb.transform.position = new Vector3(loc.x + dirtap.x, loc.y, loc.z + dirtap.y);
            VehicleController vc = gb.GetComponent<VehicleController>();
            vc.Set(dir);
            vc.bc = segmentBox;
            gb.transform.parent = segment.transform;
            /*
            VehicleControllerNew vcc = gb.GetComponent<VehicleControllerNew>();
            vcc.Set(dir);
            vcc.lerpDuration = spawnDuration * gap;
            vcc.moveDuration = spawnDuration * gap;
            vcc.bc = segmentBox;
            vcc.parentObject = segment;*/
            spawnTimer = 0f;
        }
    }
}
