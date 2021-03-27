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

    public void HoverEnter(Vector2Int coords)
    {
        //show UI elemtns
        if (!takenMission)
        {
            minimapController.ShowMission(coords);

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

            minimapController.TakeMission(missionCube);
            takenMission = true;
            timerController.AddTime(30);
        }
    }

    public void TimeOut()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void EndEnter()
    {
        HoverExit();
        takenMission = false;
        minimapController.EndMission();
        //set timer
        //unshow elemtns
    }
}
