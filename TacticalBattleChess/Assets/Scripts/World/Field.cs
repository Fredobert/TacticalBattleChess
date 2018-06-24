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
    public int currentPlayer;
    public string parentname = "StandardGrid";
    //maybe par fields
    //fields
    public Material team1;
    public Material team2;

   
    public Player player1;

    public Player player2;

    [HideInInspector]
    public List<PFelement> allPfs;
    [HideInInspector]
    public Transform parent;
    [HideInInspector]
    public Pathfinder pf;

    //world
    public enum MarkType {Path, Standard, Marked };

    GameObject tileprefab;
    GameObject charprefab;
    GameObject playerprefab;

    GameObject C;
    GameObject[,] gamefield;
    System.Random r;


    public void GenerateMap()
    {

        if (parent != null)
        {
            DestroyImmediate(parent.gameObject);

        }
        else if (GameObject.Find(parentname))
        {
            DestroyImmediate(GameObject.Find(parentname));
        }
        parent = new GameObject(parentname).transform;
        r = new System.Random();
        allPfs = new List<PFelement>();
        //init fields
        tileprefab = Resources.Load("Tile") as GameObject;
        playerprefab = Resources.Load("Player") as GameObject;
        charprefab = Resources.Load("Char") as GameObject;

        //init player
        GameObject p1 = Instantiate(playerprefab);
        GameObject p2 = Instantiate(playerprefab);
        p1.transform.SetParent(parent.transform);
        p2.transform.SetParent(parent.transform);
        p1.name = "player1";
        p2.name = "player2";
        player1 = p1.GetComponent<Player>();
        player2 = p2.GetComponent<Player>();


        player1.teamid = 0;
        player2.teamid = 1;


        //init all tiles build field
        gamefield = new GameObject[xlength, ylength];


        //build tile grid
        for (int i = 0; i < gamefield.GetLength(0); i++)
        {
            for (int j = 0; j < gamefield.GetLength(1); j++)
            {

                gamefield[i, j] = Instantiate(tileprefab, new Vector3((i + 1) * padding, (j + 1) * padding, 0), Quaternion.identity);
                gamefield[i, j].name = "Tile" + i + "-" + j;
                gamefield[i, j].transform.SetParent(parent.transform);
                gamefield[i, j].GetComponent<PFelement>().id = i + "-" + j;
                gamefield[i, j].GetComponent<PFelement>().walkable = true;
                gamefield[i, j].GetComponent<Tile>().gras = true;
                allPfs.Add(gamefield[i, j].GetComponent<PFelement>());


                //team1
                if ((i == 2 && j == 0) || (i == 3 && j == 0) || (i == 4 && j == 0) || (i == 5 && j == 0))
                {
                    C = Instantiate(charprefab, new Vector3((i + 1) * padding, (j + 1) * padding, -1), Quaternion.identity);
                    C.transform.SetParent(parent.transform);
                    Character c = C.GetComponent<Character>();
                    c.material = team1;
                    c.team = 0;
                    c.x = i;
                    c.y = j;
                    gamefield[i, j].GetComponent<PFelement>().walkable = false;
                    c.standingOn = gamefield[i, j];
                    gamefield[i, j].GetComponent<Tile>().character = C;
                    c.Init();
                } //team2
                else if ((i == 2 && j == 7) || (i == 3 && j == 7) || (i == 4 && j == 7) || (i == 5 && j == 7))
                {
                    C = Instantiate(charprefab, new Vector3((i + 1) * padding, (j + 1) * padding, -1), Quaternion.identity);
                    
                    C.transform.SetParent(parent.transform);
                    Character c = C.GetComponent<Character>();
                    c.material = team2;
                    c.team = 1;
                    c.x = i;
                    c.y = j;
                    gamefield[i, j].GetComponent<PFelement>().walkable = false;
                    c.standingOn = gamefield[i, j];
                    gamefield[i, j].GetComponent<Tile>().character = C;
                    c.Init();
                }
                else if (r.Next(10) < 2)
                {

                     gamefield[i, j].GetComponent<PFelement>().walkable = false;
                     gamefield[i, j].GetComponent<Tile>().visited();
                     gamefield[i, j].GetComponent<Tile>().gras = false;
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
                int[] z = { i, j + 1, i + 1, j, i, j - 1, i - 1, j };
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



    void Start()
    {
        //Quick and Dirty solution  Problem Gameobject/Script variables get cleared on Play
        player1 = GameObject.Find("player1").GetComponent<Player>();
        player2 = GameObject.Find("player2").GetComponent<Player>();

    }


   


    //GAMESECTION

    public void CastAbility(Character character, Ability ability,PFelement target )
    {

        ability.CastAbility(target);

    }
    List<Tile> marked = new List<Tile>();
    public void MarkTile(PFelement pfe, MarkType mt)
    {
        Tile t = pfe.GetComponent<Tile>();
        switch (mt)
        {
            case (MarkType.Marked):
                t.closed();
                break;
            case (MarkType.Path):
                t.mark();
                break;
            case (MarkType.Standard):
                t.refresh();
                break;
            default:
                break;
        }
    }
    //not functional at the moment
    private bool busy;



    private Character selectedChar;

    public void SelectCharacter(Character character, bool mark)
    {
        selectedChar = character;
        pf.generatePath(character.standingOn.GetComponent<PFelement>(), character.movment);
        StartCoroutine(GenPath());
    }



    public void Move(GameObject tile, Character character)
    {
        if (character == selectedChar)
        {
            busy = true;
            character.standingOn.GetComponent<PFelement>().walkable = true;
            character.standingOn.GetComponent<Tile>().character = null;
            tile.GetComponent<PFelement>().walkable = false;
            tile.GetComponent<Tile>().character = character.gameObject;
            character.standingOn = tile;
            //not finshed need to generae path bevor
            StartCoroutine(Move(character.gameObject, pf.GetPath(tile.GetComponent<PFelement>()), 20f));
        }
    }

 
    public List<PFelement> GetPath(PFelement pfe)
    {
      return pf.GetPath(pfe);
    }








    //Cououtines

    //Coroutine for pathgenerating
    public int maxpathzyclen = 10;
    IEnumerator GenPath()
    {
        bool computing = true;
        while (computing)
        {
            for (int i = 0; i < maxpathzyclen; i++)
            {
                if (pf.next())
                {
                    computing = false;
                    break;
                }
            }
            yield return null;
        }
        Player z = getCurrentPlayer();
        getCurrentPlayer().FinishSelecting(pf.GetinRange());
        busy = false;
    }

    //follow a path needs testing 
    int currentPath = 0;
    Vector3 currentPos;
    IEnumerator Move(GameObject cha, List<PFelement> path, float speed)
    {
        currentPath = path.Count - 1;
        currentPos = new Vector3(path[currentPath].transform.position.x, path[currentPath].transform.position.y, -1f);
        while (currentPath > -1)
        {

            cha.transform.position = Vector3.MoveTowards(cha.transform.position, currentPos, speed * Time.deltaTime);
            if (cha.transform.position == currentPos)
            {

                if (currentPath-- > 0)
                {
                    currentPos = new Vector3(path[currentPath].transform.position.x, path[currentPath].transform.position.y, -1f);
                }
            }
            yield return null;
        }
        busy = false;
        getCurrentPlayer().FinishedMoving();
    }

    //END


    public Player getCurrentPlayer()
    {
        if (currentPlayer == 0)
        {
            return player1;
        }
        return player2;
    }







    public void FinishTurn()
    {
        EventManager.TurnEnd(currentPlayer);
        currentPlayer = (currentPlayer+1)%2;
        EventManager.TurnStart(currentPlayer);
    }

    void Update()
    {
   
    }

}

