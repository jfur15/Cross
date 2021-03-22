using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelControllerScript : MonoBehaviour
{
    float blinkTimer = 0f;
    float blinkSpeed = 1f;
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
        rectTransform.localScale = new Vector3(1, 1, 1);

    }
    public void SetBlinking()
    {

        //InvokeRepeating("Blink", 0, 0.4f);
    }
    // Update is called once per frame
    void Update()
    {

    }

    void Blink()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
    public void SetPosition(Vector2 coo)
    {
        rectTransform.localPosition = new Vector3(coo.x - 32, coo.y, 0);
    }
}
