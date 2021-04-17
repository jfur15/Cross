using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum MissionState
{
    NoMission,
    PossibleMission,
    OnMission
}

public class UIControllerScript : MonoBehaviour
{
    public MinimapControllerScript minimapController;
    public TimerControllerScript timerController;
    public ScoreControllerScript scoreController;
    bool takenMission = false;
    MissionState missionState;

    // Start is called before the first frame update
    void Start()
    {

        SetState(MissionState.NoMission);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetState(MissionState newState)
    {

    }

    public void HoverEnter(Vector2Int coords, int time)
    {
        //show UI elemtns
        if (!takenMission)
        {
            minimapController.ShowMission(coords, time);

        }
    }
    public void HoverExit()
    {
        if (!takenMission)
        {

            minimapController.HideMission();
        }
        //hide UI elemtns
    }

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

    public void TimeOut()
    {
        SceneManager.LoadScene("MenuScene");
    }

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
