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
    public Texture2D map;
    public GameObject buildingObject;
    public GameObject safeObject;

    // Start is called before the first frame update
    void Start()
    {
        //import png
        //get cell size
        //set ZERO point
        //go through pnga


        Cell[,] mapp = new Cell[map.width, map.height];
        Color[,] colors = new Color[map.width, map.height];
        var xx = map.GetPixels();

        for (int i = 0; i < map.height; i++)
        {
            for (int j = 0; j < map.width; j++)
            {
                Color pixel = xx[(i * 32) + j];
                colors[i, j] = pixel;
            }
        }
        for(int i = 0; i < map.height; i++)
        {

            for (int j = 0; j < map.width; j++)
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
                        mapp[i, j] = Cell.horizontal;

                    }
                    else if (horiz && !vertOR)
                    {

                        mapp[i, j] = Cell.vertical;
                    }
                    else
                    {
                        mapp[i, j] = Cell.safe;

                    }
                    
                }
            }
        }

        int cellSize = 8;

        for (int y = 0; y < map.height; y++)
        {
            for (int x = 0; x < map.width; x++)
            {

                Vector2 pos = new Vector2((x * cellSize), (y * cellSize));

                if (mapp[x, y] == Cell.wall)
                {
                    GameObject newBuilding = Instantiate(buildingObject);
                    newBuilding.transform.localScale = new Vector3(cellSize, Random.Range(cellSize/2, cellSize*2), cellSize);
                    //newBuilding.transform.localScale = new Vector3(cellSize, cellSize/2, cellSize);
                    newBuilding.transform.position = new Vector3(pos.x, newBuilding.transform.localScale.y/2 - 1, pos.y);
                }


                if (mapp[x, y] == Cell.safe)
                {

                    GameObject newSafe = Instantiate(safeObject);
                    newSafe.transform.localScale = new Vector3(cellSize, newSafe.transform.localScale.y, cellSize);
                    newSafe.transform.position = new Vector3(pos.x, newSafe.transform.position.y, pos.y);
                }
                if (mapp[x, y] == Cell.horizontal)
                {

                    GameObject newSafe = Instantiate(safeObject);
                    newSafe.transform.localScale = new Vector3(cellSize, newSafe.transform.localScale.y, cellSize);
                    newSafe.transform.position = new Vector3(pos.x, newSafe.transform.position.y, pos.y);
                    newSafe.GetComponent<SegmentController>().CreateSpawnerOnOff(true);
                    newSafe.GetComponent<Renderer>().material.color = Color.blue;
                }
                if (mapp[x, y] == Cell.vertical)
                {

                    GameObject newSafe = Instantiate(safeObject);
                    newSafe.transform.localScale = new Vector3(cellSize, newSafe.transform.localScale.y, cellSize);
                    newSafe.transform.position = new Vector3(pos.x, newSafe.transform.position.y, pos.y);
                    newSafe.GetComponent<SegmentController>().CreateSpawnerOnOff(false); 
                    newSafe.GetComponent<Renderer>().material.color = Color.magenta;
                }
            }
        }


        Debug.Log("hello");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
