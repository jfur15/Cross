using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    public GameObject mapController;
    public GameObject pixelPrefab;
    Vector2Int startPoint;
    public Vector2Int endPoint;
    public int time = 30;
    PixelControllerScript endPointPixel;
    // Start is called before the first frame update
    void Start()
    {
        //if in range, show stuff
        //gameObject.transform.SetParent(mapController.transform);
        //if not in range, animation loop
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPoints(Vector2Int sp, Vector2Int ep)
    {
        startPoint = sp;
        endPoint = ep;

    }
}
