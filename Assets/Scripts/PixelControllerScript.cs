using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelControllerScript : MonoBehaviour
{
    float blinkTimer = 0f;
    float blinkSpeed = 0.5f;
    bool blinking = false;
    RectTransform rectTransform;
    // Start is called before the first frame update
    void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        //rectTransform.localScale = new Vector3(1, 1, 1);
        //transform.localScale = new Vector3(1, 1, 1);
    }
    private void Start()
    {

    }
    public void SetBlinking(bool blink)
    {
        blinking = blink;
        if (!blink)
        {
            BlinkOn();
        }
    }
    public void TurnOn()
    {
        rectTransform.localScale = new Vector3(1, 1, 1);
    }
    public void TurnOff()
    {
        SetBlinking(false);
        rectTransform.localScale = new Vector3(0,0,0);
    }
    void BlinkOn()
    {

        transform.localScale = new Vector3(1, 1, 1);
        blinkTimer = blinkSpeed;
    }
    void BlinkOff()
    {

        transform.localScale = new Vector3(0, 0, 0);
        blinkTimer = blinkSpeed / 4;
    }
    // Update is called once per frame
    void Update()
    {
        if (blinking)
        {
            blinkTimer -= Time.deltaTime;
            if (blinkTimer <= 0)
            {

                if (transform.localScale.magnitude < 1)
                {
                    BlinkOn();
                }
                else
                {
                    BlinkOff();
                }
            }
        }
        
    }

    public void SetPosition(Vector2 coo)
    {
        rectTransform.localPosition = new Vector3(coo.x - 32, coo.y, 0);
    }
}
