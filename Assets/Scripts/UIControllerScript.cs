using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{

}

public class UIControllerScript : MonoBehaviour
{
    public MinimapControllerScript minimapController;
    bool takenMission = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
        }
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
