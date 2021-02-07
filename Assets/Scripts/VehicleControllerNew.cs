using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleControllerNew : VehicleController
{
    Vector3 aVector = new Vector3();
    Vector3 bVector = new Vector3();

    float lerpTimer = 0f;
    public float lerpDuration = .25f;


    float moveTimer = 3f;
    public float moveDuration = .25f;
    protected override void Update()
    {
        if (lerpTimer > lerpDuration)
        {

        }
        if (moveTimer < moveDuration && lerpTimer > lerpDuration)
        {
            moveTimer += Time.deltaTime;
        }
        if (moveTimer > moveDuration)
        {
            aVector = transform.position;
            bVector = transform.position + new Vector3(dir.x, 0, dir.y);
            lerpTimer = 0f;
            moveTimer = 0f;
        }


        if (lerpTimer <= lerpDuration)
        {
            lerpTimer += Time.deltaTime;
            float x = Mathf.Lerp(aVector.x, bVector.x, lerpTimer / lerpDuration);
            float y = Mathf.Lerp(aVector.y, bVector.y, lerpTimer / lerpDuration);
            float z = Mathf.Lerp(aVector.z, bVector.z, lerpTimer / lerpDuration);

            transform.position = new Vector3(x, y, z);
        }
    }
}
