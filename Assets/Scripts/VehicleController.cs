using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class VehicleController : MonoBehaviour
{

    Vector3 initialPosition = new Vector3();
    public BoxCollider bc;
    protected float speed = 2f;
    protected Vector2 dir = Vector2.right;
    float sleepTime = 0f;
    float sleepDuration = 1f;
    Transform tc;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.tag = "enemy";
        initialPosition = transform.position;
    }

    public void Set(Vector2 go)
    {
        dir = go;
    }

    private void OnTriggerExit(Collider collision)
    {
        //if (collision.gameObject.name == transform.parent.gameObject.name)
        if (collision.gameObject.CompareTag(transform.parent.tag))
        {
            Destroy(gameObject);
            /*
            transform.position = initialPosition;
            sleepTime = 0f;
            sleepDuration = Random.Range(0.33f, 1.33f);
            //*/
        }
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        //if (sleepTime > sleepDuration)
        {

            transform.position = transform.position + new Vector3(dir.x * speed * Time.deltaTime, 0, dir.y * speed * Time.deltaTime);
        }
       // else
        {
           // sleepTime += Time.deltaTime;
        }
    }


}
