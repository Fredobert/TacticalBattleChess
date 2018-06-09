using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Field : MonoBehaviour
{
    //parameter fields
    public int xlength = 5;
    public int ylength = 5;
    public GameObject parent = null;
    public bool pturn = true;
    public int currentPlayer;
    //maybe par fields
    //fields
    public Material team1;
    public Material team2;
    public Player player1;
    public Player player2;


    GameObject prefab;
    GameObject chara;
    GameObject player;

    GameObject C;
    GameObject[,] gamefield;
    System.Random r;

    SelectManager sm;
    Pathfinder pf;
    // Use this for initialization

    public void Create()
    {
        Start();
    }


    void Start()
    {
        r = new System.Random();
        sm = GetComponent<SelectManager>();

        print("init grid");
        //init fields
        prefab = Resources.Load("Tile") as GameObject;
        player = Resources.Load("Player") as GameObject;
        //init all tiles build field
        gamefield = new GameObject[xlength, ylength];
        GameObject p1 =   Instantiate(player);
        GameObject p2 =   Instantiate(player);
        p1.name = "player1";
        p2.name = "player2";
        player1 = p1.GetComponent<Player>();
        player2 = p2.GetComponent<Player>();


        player1.teamid = 0;
        player2.teamid = 1;

        
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
                gamefield[i, j].GetComponent<PFelement>().walkable = true;



                //team1
                if ((i == 2 && j == 0) || (i == 3 && j == 0) || (i == 4 && j == 0) || (i == 5 && j == 0))
                {
                    C = Instantiate(chara, new Vector3((i + 1) * 1.1f, (j + 1) * 1.1f, -1), Quaternion.identity);
                    Character c = C.GetComponent<Character>();
                    c.material = team1;
                    c.team = 0;
                    c.x = i;
                    c.y = j;
                    gamefield[i, j].GetComponent<PFelement>().walkable = false;
                    c.standingOn = gamefield[i, j];
                    gamefield[i, j].GetComponent<Tile>().character = C;
                } //team2
                else if ((i == 2 && j == 7) || (i == 3 && j == 7) || (i == 4 && j == 7) || (i == 5 && j == 7))
                {
                    C = Instantiate(chara, new Vector3((i + 1) * 1.1f, (j + 1) * 1.1f, -1), Quaternion.identity);
                    Character c = C.GetComponent<Character>();
                    c.material = team2;
                    c.team = 1;
                    c.x = i;
                    c.y = j;
                    gamefield[i, j].GetComponent<PFelement>().walkable = false;
                    c.standingOn = gamefield[i, j];
                    gamefield[i, j].GetComponent<Tile>().character = C;
                }
                else if (r.Next(10) < 2)
                {

                    gamefield[i, j].GetComponent<PFelement>().walkable = false;
                    gamefield[i, j].GetComponent<Tile>().visited();
                }
            }
        }

        pf = gameObject.GetComponent<Pathfinder>();

        int zx;
        int zy;
        //set neighbours
        for (int i = 0; i < gamefield.GetLength(0); i++)
        {
            for (int j = 0; j < gamefield.GetLength(1); j++)
            {
                int[] z = { i, j + 1, i + 1, j, i, j - 1,i - 1, j  };
                List<PFelement> n = new List<PFelement>();
                for (int k = 0; k < z.Length; k += 2)
                {
                    zx = z[k];
                    zy = z[k + 1];
                    if (zx > -1 && zx < gamefield.GetLength(0) && zy > -1 && zy < gamefield.GetLength(1))
                    {
                        n.Add(gamefield[zx, zy].GetComponent<PFelement>());
                    }
                    else
                    {
                        n.Add(null);
                    }
                }
                gamefield[i, j].GetComponent<PFelement>().neighboors = n;
            }
        }
    }



    public void FinishTurn()
    {
        currentPlayer = (currentPlayer+1)%2;
    }
    void Update()
    {
   
    }

}

