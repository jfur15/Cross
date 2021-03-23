using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapControllerScript : MonoBehaviour
{
    public GameObject mapController;
    MapControllerScript mcs;
    public GameObject pixelPrefab;
    public GameObject player;

    PixelControllerScript playerPixel;
    PixelControllerScript endPointPixel;

    public GameObject missionPrefab;
    public GameObject missionEndPrefab;

    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        mcs = mapController.GetComponent<MapControllerScript>();
        Texture2D mapTexture =mcs.mapTexture;
        Sprite mapSprite = Sprite.Create(mapTexture, new Rect(0f, 0f, mapTexture.width, mapTexture.height), new Vector2(0.5f,0.5f));
        gameObject.GetComponent<Image>().sprite = mapSprite;

        playerPixel = Instantiate(pixelPrefab).GetComponent<PixelControllerScript>();
        playerPixel.transform.SetParent(gameObject.transform);
        playerPixel.SetBlinking(true);

        endPointPixel = Instantiate(pixelPrefab).GetComponent<PixelControllerScript>();
        endPointPixel.transform.SetParent(gameObject.transform);
        endPointPixel.SetPosition(new Vector2Int(0, 0));
        endPointPixel.GetComponent<CanvasRenderer>().SetColor(Color.red);
        foreach (var startPoint in mcs.blanks)
        {

            Vector2Int endPoint = startPoint;
            while (endPoint == startPoint)
            {
                endPoint = mcs.blanks[Random.Range(0, mcs.blanks.Count - 1)];
            }
            CreateMission(startPoint, endPoint);
        }
    }
    void CreateMission(Vector2Int startPoint, Vector2Int endPoint)
    {
        GameObject newSafe = mcs.GetSegment(startPoint);

        MissionController mission = Instantiate(missionPrefab).GetComponent<MissionController>();
        mission.transform.SetParent(newSafe.transform);
        mission.SetPoints(startPoint, endPoint);
        mission.transform.position = new Vector3(newSafe.transform.position.x - newSafe.transform.localScale.x / 2 + 0.5f, -0.12f, newSafe.transform.position.z - newSafe.transform.localScale.z / 2 + 0.5f);

        Vector2 poss = new Vector2(Random.Range(1, 7), Random.Range(1, 7));
        mission.transform.position += new Vector3(poss.x, 0, poss.y);
        ShowMission(endPoint);
        HideMission();

    }
    void CreateMission()
    {
        //check for empty cells
        var blanks = mcs.blanks;
        Vector2Int startPoint = blanks[Random.Range(0, blanks.Count - 1)];
        Vector2Int endPoint = blanks[Random.Range(0, blanks.Count - 1)];
        CreateMission(startPoint, endPoint);
    }
    public void ShowMission(Vector2Int coords)
    {
        endPointPixel.TurnOn();
        endPointPixel.SetPosition(coords);
    }
    public void HideMission()
    {
        endPointPixel.TurnOff();
    }

    public void TakeMission(GameObject missionCube)
    {
        Vector2Int endPoint = missionCube.GetComponent<MissionController>().endPoint;
        GameObject newSafe = mcs.GetSegment(endPoint);

        MissionEndCubeControllerScript missionendCube = Instantiate(missionEndPrefab).GetComponent<MissionEndCubeControllerScript>();
        missionendCube.transform.SetParent(newSafe.transform);
        missionendCube.transform.position = new Vector3(newSafe.transform.position.x - newSafe.transform.localScale.x / 2 + 0.5f, -0.12f, newSafe.transform.position.z - newSafe.transform.localScale.z / 2 + 0.5f);

        Destroy(missionCube);
    }

    public void EndMission()
    {
        //set new mission cubes
        endPointPixel.TurnOff();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 coo = player.GetComponent<PlayerController>().coordinates;
        playerPixel.SetPosition(coo);
    }
}
