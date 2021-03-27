using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerControllerScript : MonoBehaviour
{
    public float timer = 5;
    Text text;
    public GameObject uiControllerObject;
    UIControllerScript uiControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        uiControllerScript = uiControllerObject.GetComponent<UIControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= 1 * Time.deltaTime;
        text.text = ((int)timer).ToString();

        if (timer <= 1)
        {
            uiControllerScript.TimeOut();
            Destroy(this);
        }
    }

    public void AddTime(int sec)
    {
        timer += sec;
    }
}
