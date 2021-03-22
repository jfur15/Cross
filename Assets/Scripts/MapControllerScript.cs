using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Cell
{
    horizontal,
    vertical,
    safe,
    wall
}
public class MapControllerScript : MonoBehaviour
{
    public Texture2D mapTexture;

    public GameObject buildingObject;
    public GameObject safeObject;

    Cell[,] mapp;
    GameObject[,] mappO;
    public List<Vector2Int> blanks = new List<Vector2Int>();
    // Start is called before the first frame update
    void Start()
    {
        //import png
        //get cell size
        //set ZERO point
        //go through pnga


        mapp = new Cell[mapTexture.width, mapTexture.height];
        mappO = new GameObject[mapTexture.width, mapTexture.height];
        Color[,] colors = new Color[mapTexture.width, mapTexture.height];

        var xx = mapTexture.GetPixels();

        for (int i = 0; i < mapTexture.height; i++)
        {
            for (int j = 0; j < mapTexture.width; j++)
            {
                Color pixel = xx[(i * 32) + j];
                colors[j,i] = pixel;
            }
        }
        for(int i = mapTexture.height-1; i >=0 ; i--)
        {

            for (int j = mapTexture.height - 1; j >= 0; j--)
            {
                Color pixel = colors[i, j];
                if (pixel.a < 1)
                {
                    mapp[i, j] = Cell.wall;
                }

                else
                {
                    bool vert = colors[i+1, j].a >= 1 && colors[i - 1, j].a >= 1;
                    bool horiz = colors[i, j+1].a >= 1 && colors[i, j-1].a >= 1;
                    bool vertOR = colors[i + 1, j].a >= 1 || colors[i - 1, j].a >= 1;
                    bool horizOR = colors[i, j + 1].a >= 1 || colors[i, j - 1].a >= 1;

                    if (vert && !horizOR)
                    {
                        mapp[i, j] = Cell.vertical;

                    }
                    else if (horiz && !vertOR)
                    {

                        mapp[i, j] = Cell.horizontal;
                    }
                    else
                    {
                        mapp[i, j] = Cell.safe;

                    }
                    
                }
            }
        }

        int cellSize = 8;

        for (int x = 0; x < mapTexture.height; x++)
        {
            for (int y = 0; y < mapTexture.width; y++)
            {

                Vector2 pos = new Vector2((x * cellSize), (y * cellSize));

                if (mapp[x, y] == Cell.wall)
                {
                    GameObject newBuilding = Instantiate(buildingObject);
                    newBuilding.transform.localScale = new Vector3(cellSize, Random.Range(cellSize/2, cellSize*2), cellSize);
                    //newBuilding.transform.localScale = new Vector3(cellSize, cellSize/2, cellSize);
                    newBuilding.transform.position = new Vector3(pos.x, newBuilding.transform.localScale.y/2 - 1, pos.y);
                    mappO[x, y] = newBuilding;
                }


                if (mapp[x, y] == Cell.safe)
                {
                    blanks.Add(new Vector2Int(x, y));
                    GameObject newSafe = Instantiate(safeObject);
                    newSafe.transform.localScale = new Vector3(cellSize, newSafe.transform.localScale.y, cellSize);
                    newSafe.transform.position = new Vector3(pos.x, newSafe.transform.position.y, pos.y);
                    newSafe.GetComponent<SegmentController>().SetCoordinates(new Vector2(x, y));

                    mappO[x, y] = newSafe;
                }
                if (mapp[x, y] == Cell.horizontal)
                {

                    GameObject newSafe = Instantiate(safeObject);
                    newSafe.transform.localScale = new Vector3(cellSize, newSafe.transform.localScale.y, cellSize);
                    newSafe.transform.position = new Vector3(pos.x, newSafe.transform.position.y, pos.y);
                    newSafe.GetComponent<SegmentController>().CreateSpawnerOnOff(true);
                    newSafe.GetComponent<Renderer>().material.color = Color.blue;
                    newSafe.GetComponent<SegmentController>().SetCoordinates(new Vector2(x, y));
                    mappO[x, y] = newSafe;

                }
                if (mapp[x, y] == Cell.vertical)
                {

                    GameObject newSafe = Instantiate(safeObject);
                    newSafe.transform.localScale = new Vector3(cellSize, newSafe.transform.localScale.y, cellSize);
                    newSafe.transform.position = new Vector3(pos.x, newSafe.transform.position.y, pos.y);
                    newSafe.GetComponent<SegmentController>().CreateSpawnerOnOff(false); 
                    newSafe.GetComponent<Renderer>().material.color = Color.magenta;
                    newSafe.GetComponent<SegmentController>().SetCoordinates(new Vector2(x, y));
                    mappO[x, y] = newSafe;
                }
            }
        }

        //CreateMission();
        Debug.Log("hello");
    }
    public GameObject GetSegment(Vector2Int vs)
    {
        return mappO[vs.x, vs.y];
    }
   /* void CreateMission()
    {
        //check for empty cells
        Vector2Int startPoint = blanks[Random.Range(0, blanks.Count - 1)];
        Vector2Int endPoint = blanks[Random.Range(0, blanks.Count - 1)];

        GameObject newSafe = mappO[startPoint.x, startPoint.y];

        MissionController mission = Instantiate(missionCubeObject).GetComponent<MissionController>();
        mission.SetPoints(startPoint, endPoint);
        mission.transform.position = new Vector3(newSafe.transform.position.x - newSafe.transform.localScale.x / 2 + 0.5f, -0.12f, newSafe.transform.position.z - newSafe.transform.localScale.z / 2 + 0.5f);

        Vector2 poss = new Vector2(Random.Range(1, 7), Random.Range(1, 7));
        mission.transform.position += new Vector3(poss.x, 0, poss.y);

    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
