using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ClearDataControllerScript : MonoBehaviour
{
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        LoadScores();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void LoadScores()
    {

        var x = ScoreboardControllerScript.GetScores();
        string separator = "                    ";
        scoreText.text = "Date" + separator + "Score";
        foreach (var item in x)
        {
            scoreText.text += "\n" + item.Item1.ToString("g") + separator + item.Item2;
        }
    }
    public void ClearData()
    {
        //ScoreboardControllerScript.NewScore(1);
        ScoreboardControllerScript.ClearScores();
        LoadScores();
    }
}
