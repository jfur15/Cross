using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreControllerScript : MonoBehaviour
{
    int score = 0;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        text.text = "SCORE\n" + score.ToString();

    }

    public void AddOne()
    {
        score += 1;
    }
    public void AddScore()
    {
        score+=10;
    }
}
