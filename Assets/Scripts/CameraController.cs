using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 12, player.transform.position.z);
        transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y+2, player.transform.position.z);

    }
}
