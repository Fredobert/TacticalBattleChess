using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class Field : MonoBehaviour
{


    //parameter fields
    public int xlength = 5;
    public int ylength = 5;

    [Range(1f,3f)]
    public float padding = 2f;
    //   public float size = 1f;
    public string parentname = "StandardGrid";
    



    public List<Tile> allTiles;
    [HideInInspector]
    public Transform parent;
    //fix gamefield is null on play
    public GameObject[,] gamefield;

    //prefab section
   public GameObject tileprefab;
   public GameObject StandardTileContent;

    //test
    public float isox = 1f;
    public float isoy = 1f;
    public float offx = 3.3f;
        
    public void GenerateMap()
    {
        World.pf = new Pathfinder();
        if (parent != null)
        {
            DestroyImmediate(parent.gameObject);

        }
        else if (GameObject.Find(parentname))
        {
            DestroyImmediate(GameObject.Find(parentname));
        }
        parent = new GameObject(parentname).transform;
        allTiles = new List<Tile>();

        //init all tiles build field
        gamefield = new GameObject[xlength, ylength];
        //build tile grid
        for (int i = 0; i < gamefield.GetLength(0); i++)
        {
            for (int j = 0; j < gamefield.GetLength(1); j++)
            {
                //iso version
                //gamefield[i, j] = Instantiate(tileprefab, new Vector3(((i+j) * isox) * padding, ((i - j) * isoy) * padding, (gamefield.GetLength(1)-j)/10f + (i )/100f), Quaternion.identity);
                gamefield[i, j] = Instantiate(tileprefab, new Vector3(((i +j/offx) * isox) * padding,  j * isoy * padding, 1-((gamefield.GetLength(1) - j) / 10f + (i) / 100f)), Quaternion.identity);

                gamefield[i, j].name = "Tile" + i + "-" + j;
                gamefield[i, j].transform.SetParent(parent.transform);
                Tile t = gamefield[i, j].GetComponent<Tile>();
                GameObject g = Instantiate(StandardTileContent);
                GetComponent<World>().AddTileContent(g, t);
                t.Init(i + "-:" + j);
                allTiles.Add(t);
            }
        }
        parent.transform.SetParent(gameObject.transform, false);
        parent.transform.position = gameObject.transform.position;
        int zx;
        int zy;
        int dx;
        int dy;
        //set neighbours
        for (int i = 0; i < gamefield.GetLength(0); i++)
        {
            for (int j = 0; j < gamefield.GetLength(1); j++)
            {
                int[] z = { i, j + 1, i + 1, j, i, j - 1, i - 1, j };
                int[] diag = { i +1, j +1, i +1, j-1, i-1, j-1 , i-1 , j+1 };
                List<Tile> n = new List<Tile>();
                List<Tile> d = new List<Tile>();
                for (int k = 0; k < z.Length; k += 2)
                {
                    zx = z[k];
                    zy = z[k + 1];

                    dx = diag[k];
                    dy = diag[k + 1];
                    //add neighboors
                    if (zx > -1 && zx < gamefield.GetLength(0) && zy > -1 && zy < gamefield.GetLength(1))
                    {
                        n.Add(gamefield[zx, zy].GetComponent<Tile>());
                    }
                    else
                    {
                        n.Add(null);
                    }
                    //add diagonal neighboors
                    if (dx > -1 && dx < gamefield.GetLength(0) && dy > -1 && dy < gamefield.GetLength(1))
                    {
                        d.Add(gamefield[dx, dy].GetComponent<Tile>());
                    }
                    else
                    {
                        d.Add(null);
                    }
                }
                gamefield[i, j].GetComponent<Tile>().neighboors = n;
                gamefield[i, j].GetComponent<Tile>().diagonalNeighboors = d;
            }
        }
    }

}

