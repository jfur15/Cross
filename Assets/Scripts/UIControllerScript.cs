using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControllerScript : MonoBehaviour
{
    public MinimapControllerScript minimapController;
    public TimerControllerScript timerController;
    public ScoreControllerScript scoreController;
    bool takenMission = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Called when the player enters a cube
    public void HoverEnter(Vector2Int coords, int time)
    {
        if (!takenMission)
        {
            minimapController.ShowMission(coords, time);

        }
    }

    //Called when the player exits a cube
    public void HoverExit()
    {
        if (!takenMission)
        {
            minimapController.HideMission();
        }
    }


    //Called when the player presses space within a cube
    public void StartMission(GameObject missionCube)
    {
        if (!takenMission)
        {

            MissionController mcs = missionCube.GetComponent<MissionController>();
            minimapController.TakeMission(missionCube);
            takenMission = true;
            timerController.AddTime(mcs.time);
        }
    }

    //Called when the TimerController runs out
    public void TimeOut()
    {
        if (scoreController.GetScore() > 0)
        {
            ScoreboardControllerScript.NewScore(scoreController.GetScore());
        }
        SceneManager.LoadScene("ScoreScene");
    }

    //Called when the player runs into a mission end cube
    public void EndEnter()
    {
        HoverExit();
        takenMission = false;
        minimapController.EndMission();
        scoreController.AddScore();
        //set timer
        //unshow elemtns
    }
}
