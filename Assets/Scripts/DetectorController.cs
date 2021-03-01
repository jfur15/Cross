using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorController : MonoBehaviour
{
    int groundCounter = 0;
    List<GameObject> pool = new List<GameObject>();
    int pitCounter = 0;

    public bool ground
    {
        get
        {
            if (groundCounter > 0)
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
            groundCounter++;
            pool.Clear();
            //doesit = true;
        }
        if (other.CompareTag("pit"))
        {
            pitCounter++;
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
            groundCounter--;
        }
        if (other.CompareTag("pit"))
        {
            pitCounter--;
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
