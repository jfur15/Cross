using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class VehicleController : MonoBehaviour
{

    public BoxCollider bc;
    protected float speed = 2f;
    protected Vector2 dir = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {
    }
    public Vector2 GetDir()
    {
        return dir;
    }
    public float GetSpeed()
    {
        return ((speed * dir.x) +(speed * dir.y));
    }
    public void Set(Vector2 go)
    {
        dir = go;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag(transform.parent.tag))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position = transform.position + new Vector3(dir.x * speed * Time.deltaTime, 0, dir.y * speed * Time.deltaTime);
    }


}
