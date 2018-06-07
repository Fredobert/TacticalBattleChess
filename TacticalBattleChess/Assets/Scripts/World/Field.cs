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

            GameObject chara = Resources.Load("Char") as GameObject;
            if (parent == null)
            {
                parent = new GameObject("StandardGrid");
            }

            //build tile grid
            for (int i = 0; i < gamefield.GetLength(0); i++)
            {
                for (int j = 0; j < gamefield.GetLength(1); j++)
                {

                    gamefield[i, j] = Instantiate(prefab, new Vector3((i + 1) * 1.1f, (j + 1) * 1.1f, 0), Quaternion.identity);
                    gamefield[i, j].name = "Tile" + i + "-" + j;
                    gamefield[i, j].transform.SetParent(parent.transform);
                    gamefield[i, j].GetComponent<PFelement>().id = i + "-" + j;
                    if (r.Next(10) < 2)
                    {

                        gamefield[i, j].GetComponent<PFelement>().walkable = false;
                        gamefield[i, j].GetComponent<Tile>().visited();
                    }
                    else
                    {
                        gamefield[i, j].GetComponent<PFelement>().walkable = true;
                    }


                    if ((i == 3 && j == 3) || (i == 1 && j == 1) || (i == 0 && j == 4))
                    {
                        C = Instantiate(chara, new Vector3((i + 1) * 1.1f, (j + 1) * 1.1f, -1), Quaternion.identity);
                        Character c = C.GetComponent<Character>();
                        c.x = i;
                        c.y = j;
                        gamefield[i, j].GetComponent<PFelement>().walkable = false;
                        c.standingOn = gamefield[i, j];
                    }
                }
            }

            //dirty way
            pf = gameObject.GetComponent<Pathfinder>();

            int zx;
            int zy;
            //set neighbours
            for (int i = 0; i < gamefield.GetLength(0); i++)
            {
                for (int j = 0; j < gamefield.GetLength(1); j++)
                {
                    int[] z = { i + 1, j, i - 1, j, i, j + 1, i, j - 1 };
                    List<PFelement> n = new List<PFelement>();
                    for (int k = 0; k < z.Length; k += 2)
                    {
                        zx = z[k];
                        zy = z[k + 1];
                        if (zx > -1 && zx < gamefield.GetLength(0) && zy > -1 && zy < gamefield.GetLength(1))
                        {
                                n.Add(gamefield[zx, zy].GetComponent<PFelement>());
                        }
                    }
                    gamefield[i, j].GetComponent<PFelement>().neighboors = n;
                    


                }
            }


        }
    }
    private bool test = false;
    private bool calcpath = false;
    private bool pathav = false;
    private int tt = 0;
    List<PFelement> path = new List<PFelement>();
    List<PFelement> marked = new List<PFelement>();
    void Update()
    {

        if (pathav)
        {
            if (sm.Csel)
            {
                sm.Thover = false;
                for (int i = 0; i < marked.Count; i++)
                {
                    marked[i].gameObject.GetComponent<Tile>().reset();
                }
                pf.generatePath(sm.selectedCharacter.GetComponent<Character>().standingOn.GetComponent<PFelement>(), 2);

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
                        marked = pf.GetinRange();
                        for (int j = 0; j < marked.Count; j++)
                        {
                            marked[j].gameObject.GetComponent<Tile>().closed();
                        }

                        break;
                    }
                }

            }
            else if (test)
            {
                if (sm.Tsel&& path.Count >0)
                {
                    //really dirty at the moment
                    //sm.Tsel = false;
                    //sm.Thover = false;
                    //test = false;
                    pathav = false;
                    sm.Tsel = false;
                    sm.Thover = false;
                    test = false;

                    

                    sm.selectedCharacter.GetComponent<Character>().standingOn.GetComponent<PFelement>().walkable = true;
                    sm.selectedTile.GetComponent<PFelement>().walkable = false;
                   // sm.selectedCharacter.transform.position = sm.selectedTile.transform.position;
                   // sm.selectedCharacter.transform.position += new Vector3(0, 0, -1);
                    sm.selectedCharacter.GetComponent<Character>().x = sm.selectedTile.GetComponent<Tile>().x;
                    sm.selectedCharacter.GetComponent<Character>().y = sm.selectedTile.GetComponent<Tile>().y;
                    sm.selectedCharacter.GetComponent<Character>().standingOn = sm.selectedTile;
                    for (int i = 0; i < path.Count; i++)
                    {
                        path[i].gameObject.GetComponent<Tile>().unmark();
                    }
                    sm.Thover = false;
                    StartCoroutine(Move(sm.selectedCharacter, path, 10f));
                    //sm.Csel = true;


                }
                else if (sm.Thover)
                {
                    sm.Thover = false;
                    for (int i = 0; i < path.Count; i++)
                    {
                        path[i].gameObject.GetComponent<Tile>().unmark();
                    }
                    path = pf.GetPath(sm.hover.GetComponent<PFelement>());
                    for (int i = 0; i < path.Count; i++)
                    {
                        if (path[i].walkable)
                        {
                            path[i].gameObject.GetComponent<Tile>().mark();
                        }

                    }
                }
                else if (sm.Tsel)
                {
                    sm.Tsel = false;
                }

            }
        }
        else if (sm.Csel)
        {
            pathav = true;
        }
        else if (sm.Tsel)
        {
            sm.Tsel = false;
        }
    }


    //follow a path needs testing 
    int currentPath = 0;
    Vector3 currentPos;
    IEnumerator Move(GameObject cha,List<PFelement> path,float speed)
    {
        currentPath = path.Count - 1;
        currentPos = new Vector3(path[currentPath].transform.position.x, path[currentPath].transform.position.y, -1f);
        while (currentPath > -1)
        {
          
            cha.transform.position =   Vector3.MoveTowards(cha.transform.position,currentPos, speed * Time.deltaTime);
            if (cha.transform.position == currentPos)
            {
               
                if (currentPath-- > 0)
                {
                    currentPos = new Vector3(path[currentPath].transform.position.x, path[currentPath].transform.position.y, -1f);
                }
            }
            yield return null;
        }

        sm.Csel = true;
        
    }
}

