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

        for (int i = 0; i < map.width; i++)
        {
            for (int j = 0; j < map.height; j++)
            {
                Color pixel = xx[(i * 32) + j];
                colors[i, j] = pixel;
            }
        }
        for(int i = 0; i < map.width; i++)
        {

            for (int j = 0; j < map.height; j++)
            {
                Color pixel = colors[i, j];
                if (pixel.a < 1)
                {
                    mapp[i, j] = Cell.wall;
                }

                else
                {
                    bool horiz = colors[i+1, j].b >= 1;
                    bool vert = colors[i, j+1].b >= 1;

                    if (horiz && vert)
                    {

                        mapp[i, j] = Cell.safe;
                    }
                    else if (horiz)
                    {

                        mapp[i, j] = Cell.horizontal;
                    }
                    else
                    {

                        mapp[i, j] = Cell.vertical;
                    }
                    
                }
            }
        }

        int cellSize = 6;

        for (int x = 0; x < map.width; x++)
        {
            for (int y = map.height-1; y >= 0; y--)
            {

                Vector2 pos = new Vector2((x * cellSize) - cellSize / 2, (y * cellSize) - cellSize / 2);

                if (mapp[x, y] == Cell.wall)
                {
                    GameObject newBuilding = Instantiate(buildingObject);
                    newBuilding.transform.localScale = new Vector3(cellSize, Random.Range(cellSize/2, cellSize*4), cellSize);
                    //newBuilding.transform.localScale = new Vector3(cellSize, cellSize/2, cellSize);
                    newBuilding.transform.position = new Vector3(pos.x, newBuilding.transform.localScale.y/2 - 1, pos.y);
                }


                if (mapp[x, y] == Cell.safe)
                {

                    GameObject newSafe = Instantiate(safeObject);
                    newSafe.transform.localScale = new Vector3(cellSize, newSafe.transform.position.y, cellSize);
                    newSafe.transform.position = new Vector3(pos.x, newSafe.transform.position.y, pos.y);
                }
                if (mapp[x, y] == Cell.horizontal)
                {

                    GameObject newSafe = Instantiate(safeObject);
                    newSafe.transform.localScale = new Vector3(cellSize, newSafe.transform.position.y, cellSize);
                    newSafe.transform.position = new Vector3(pos.x, newSafe.transform.position.y, pos.y);
                    //newSafe.GetComponent<SegmentController>().CreateSpawner()
                }
                if (mapp[x, y] == Cell.vertical)
                {

                    GameObject newSafe = Instantiate(safeObject);
                    newSafe.transform.localScale = new Vector3(cellSize, newSafe.transform.position.y, cellSize);
                    newSafe.transform.position = new Vector3(pos.x, newSafe.transform.position.y, pos.y);
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
