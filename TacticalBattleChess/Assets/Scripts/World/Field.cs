using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Field : MonoBehaviour
{
    //parameter fields
    public int xlength = 5;
    public int ylength = 5;
    public bool created = false;
    public Tile[,] tilefield;
    public GameObject parent = null;
    //maybe par fields
    //fields
    GameObject prefab;
    GameObject chara;

    GameObject C;
    GameObject[,] gamefield;
    System.Random r;
    List<ICharacter> chars;

    SelectManager sm;
    Pathfinder pf;
    // Use this for initialization

    public void Create()
    {
        Start();
        created = true;
    }


    void Start()
    {
        r = new System.Random();
        sm = GetComponent<SelectManager>();
        if (!created)
        {
            print("init grid");
            //init fields
            chars = new List<ICharacter>();
            prefab = Resources.Load("Tile") as GameObject;
            //init all tiles build field
            gamefield = new GameObject[xlength, ylength];
            tilefield = new Tile[xlength, ylength];
            GameObject chara = Resources.Load("Char") as GameObject;
            if (parent == null)
            {
                parent = new GameObject("StandardGrid");
            }
            
            //build grid
            for (int i = 0; i < gamefield.GetLength(0); i++)
            {
                for (int j = 0; j < gamefield.GetLength(1); j++)
                {

                    gamefield[i, j] = Instantiate(prefab, new Vector3((i + 1) * 1.1f, (j + 1) * 1.1f, 0), Quaternion.identity);
                    gamefield[i, j].name = "Tile" + i + "-" + j;
                    gamefield[i, j].transform.SetParent(parent.transform);

                    tilefield[i, j] = gamefield[i, j].GetComponent<Tile>();
                    tilefield[i, j].x = i;
                    tilefield[i, j].y = j;
                    if (r.Next(10) <3) {

                        tilefield[i, j].walkable = false;
                        tilefield[i, j].visited();
                    }
                 

                    if (i == 3 && j == 3)
                    {
                        C = Instantiate(chara, new Vector3((i + 1) * 1.1f, (j + 1) * 1.1f, -1), Quaternion.identity);
                        Character c = C.GetComponent<Character>();
                        c.x = i;
                        c.y = j;
                        tilefield[i, j].walkable = false;
                    }
                    if (i == 1 && j == 1)
                    {
                        C = Instantiate(chara, new Vector3((i + 1) * 1.1f, (j + 1) * 1.1f, -1), Quaternion.identity);
                        Character c = C.GetComponent<Character>();
                        c.x = i;
                        c.y = j;
                        tilefield[i, j].walkable = false;
                    }
                    if (i == 0 && j == 4)
                    {
                        C = Instantiate(chara, new Vector3((i + 1) * 1.1f, (j + 1) * 1.1f, -1), Quaternion.identity);
                        Character c = C.GetComponent<Character>();
                        c.x = i;
                        c.y = j;
                        tilefield[i, j].walkable = false;
                    }

                }
            }

            //dirty way
            pf = gameObject.GetComponent<Pathfinder>();
            pf.field = tilefield;
        }

    }
    private bool test = false;
    private bool calcpath = false;
    private bool pathav = false;
    private int tt = 0;
    List<Tile> path = new List<Tile>();
    void Update()
    {

        if (pathav)
        {
            if (sm.Csel)
            {
                sm.Thover = false;
                pf.generatePath(sm.selectedCharacter.GetComponent<Character>().x, sm.selectedCharacter.GetComponent<Character>().y, 55);
                
                calcpath = true;
                sm.Csel = false;
            }
            else if (calcpath)
            {
                //set search speed 
                for (int i = 0; i < 25; i++)
                {
                    if (pf.next())
                    {
                        calcpath = false;
                        test = true;
                        break;
                    }
                }

            }
            else if (test)
            {
                if (sm.Tsel)
                {
                    //sm.Tsel = false;
                    //sm.Thover = false;
                    //test = false;
                    pathav = false;
                    sm.Tsel = false;
                    sm.Thover = false;
                    test = false;
                    tilefield[sm.selectedCharacter.GetComponent<Character>().x, sm.selectedCharacter.GetComponent<Character>().y].walkable = true;
                    tilefield[sm.selectedTile.GetComponent<Tile>().x, sm.selectedTile.GetComponent<Tile>().y].walkable = false;
                    sm.selectedCharacter.transform.position = sm.selectedTile.transform.position;
                    sm.selectedCharacter.transform.position += new Vector3(0, 0, -1);
                    sm.selectedCharacter.GetComponent<Character>().x = sm.selectedTile.GetComponent<Tile>().x;
                    sm.selectedCharacter.GetComponent<Character>().y = sm.selectedTile.GetComponent<Tile>().y;

                    sm.Csel = true;
                    

                }
                else if (sm.Thover)
                {
                    sm.Thover = false;
                    for (int i = 0; i < path.Count; i++)
                    {
                        path[i].unmark();
                    }
                    path = pf.GetPath(sm.hover.GetComponent<Tile>().x, sm.hover.GetComponent<Tile>().y);
                    for (int i = 0; i < path.Count; i++)
                    {
                        if (path[i].walkable)
                        {
                            path[i].mark();
                        }
                       
                    }
                }
                else if (sm.Tsel)
                {
                    sm.Tsel = false;
                }

            }
        }
        else if(sm.Csel)
        {
            pathav = true;
        }else if (sm.Tsel)
        {
            sm.Tsel = false;
        }
    }
}

