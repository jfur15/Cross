using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorController : MonoBehaviour
{
    int counter = 0;
    List<GameObject> pool = new List<GameObject>();
    public bool doesit
    {
        get
        {
            if (counter>0)
            {
                return true;
            }
            return false;
        } }
    //public bool doesit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject SnapObject()
    {
        if (pool.Count < 1)
        {
            return null;
        }
        return pool[0];
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ground"))
        {
            counter++;
            //doesit = true;
        }

        if (other.CompareTag("platform"))
        {
            if (!pool.Contains(other.gameObject))
            {

                pool.Add(other.gameObject);
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ground"))
        {
            counter--;
            //doesit = false;
        }
        if (other.CompareTag("platform"))
        {
            if (pool.Contains(other.gameObject))
            {

                pool.Remove(other.gameObject);
            }
        }
    }
}
