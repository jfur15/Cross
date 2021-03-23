using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(0.3f + Mathf.PingPong(Time.time, 0.5f), transform.localScale.y, transform.localScale.z);
    }
}
