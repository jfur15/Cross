﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            other.gameObject.GetComponent<SegmentController>().SetActiveSpawners(true);

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            other.gameObject.GetComponent<SegmentController>().SetActiveSpawners(false);
        }
    }
}
