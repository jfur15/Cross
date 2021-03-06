﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Cell
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

    public Material myMaterial;

    Cell[,] mapp;
    GameObject[,] mappO;
    public List<Vector2Int> blanks = new List<Vector2Int>();
    // Start is called before the first frame update
    private void Awake()
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
                colors[j, i] = pixel;
            }
        }
        for (int i = mapTexture.height - 1; i >= 0; i--)
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
                    bool vert = colors[i + 1, j].a >= 1 && colors[i - 1, j].a >= 1;
                    bool horiz = colors[i, j + 1].a >= 1 && colors[i, j - 1].a >= 1;
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
                    newBuilding.transform.localScale = new Vector3(cellSize, Random.Range(cellSize / 2, cellSize * 2), cellSize);
                    //newBuilding.transform.localScale = new Vector3(cellSize, cellSize/2, cellSize);
                    newBuilding.transform.position = new Vector3(pos.x, newBuilding.transform.localScale.y / 2 - 1, pos.y);
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
                    mappO[x, y].GetComponent<SegmentController>().cellType = mapp[x, y];
                }
                if (mapp[x, y] == Cell.horizontal)
                {

                    GameObject newSafe = Instantiate(safeObject);
                    newSafe.transform.localScale = new Vector3(cellSize, newSafe.transform.localScale.y, cellSize);
                    newSafe.transform.position = new Vector3(pos.x, newSafe.transform.position.y, pos.y);
                    newSafe.transform.Rotate(transform.up, 90, Space.Self);
                    newSafe.GetComponent<SegmentController>().CreateSpawnerOnOff(true);
                    //newSafe.GetComponent<Renderer>().material.color = Color.blue; 
                    //newSafe.GetComponent<Renderer>().material = myMaterial;
                    //foreach(GameObject g in newSafe.transform.GetComponentsInChildren<GameObject>())
                    {
                        //g.transform.RotateAround
                       // g.transform.Rotate(newSafe.transform.up, -90);
                    }
                    newSafe.GetComponent<SegmentController>().SetCoordinates(new Vector2(x, y));
                    mappO[x, y] = newSafe;
                    mappO[x, y].GetComponent<SegmentController>().cellType = mapp[x, y];

                }
                if (mapp[x, y] == Cell.vertical)
                {

                    GameObject newSafe = Instantiate(safeObject);
                    newSafe.transform.localScale = new Vector3(cellSize, newSafe.transform.localScale.y, cellSize);
                    newSafe.transform.position = new Vector3(pos.x, newSafe.transform.position.y, pos.y);
                    newSafe.GetComponent<SegmentController>().CreateSpawnerOnOff(false);
                    //newSafe.GetComponent<Renderer>().material.color = Color.magenta;
                    newSafe.GetComponent<SegmentController>().SetCoordinates(new Vector2(x, y));
                    mappO[x, y] = newSafe;
                    mappO[x, y].GetComponent<SegmentController>().cellType = mapp[x, y];
                }
            }
        }

        //CreateMission();
        Debug.Log("hello");
    }

    void Start()
    {
    }
    public GameObject GetSegment(Vector2Int vs)
    {
        return mappO[vs.x, vs.y];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
