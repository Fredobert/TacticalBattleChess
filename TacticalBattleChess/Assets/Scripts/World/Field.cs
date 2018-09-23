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
    //maybe par fields
    //fields

    public HumanPlayer player1;
    public HumanPlayer player2;

    [HideInInspector]
    public int currentPlayer = 0;
    public List<Tile> allTiles;
    [HideInInspector]
    public Transform parent;
    [HideInInspector]
    public Pathfinder pf;
    //fix gamefield is null on play
    public GameObject[,] gamefield;
    [HideInInspector]
    public enum MarkType {Path, Standard, Marked };

    //prefab section
   public GameObject tileprefab;
   public GameObject StandardTileContent;
   public GameObject charprefab;
   public GameObject player1prefab;
    public GameObject player2prefab;
    public GameObject charuiprefab;

    public List<GameObject> characterPrefabs = new List<GameObject>();
    public List<GameObject> tilePrefabs = new List<GameObject>();
    public List<GameObject> contentPrefabs = new List<GameObject>();

    GameObject C;


    //test
    public float isox = 1f;
    public float isoy = 1f;
    public void GenerateMap()
    {
        pf = new Pathfinder();
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

        //init player
        GameObject p1 = Instantiate(player1prefab);
        GameObject p2 = Instantiate(player2prefab);
        p1.transform.SetParent(parent.transform);
        p2.transform.SetParent(parent.transform);
        p1.name = "player1";
        p2.name = "player2";
        player1 = p1.GetComponent<HumanPlayer>();
        player2 = p2.GetComponent<HumanPlayer>();

        player1.teamid = 0;
        player2.teamid = 1;


        //init all tiles build field
        gamefield = new GameObject[xlength, ylength];
        //build tile grid
        for (int i = 0; i < gamefield.GetLength(0); i++)
        {
            for (int j = 0; j < gamefield.GetLength(1); j++)
            {
                //iso version
                //gamefield[i, j] = Instantiate(tileprefab, new Vector3(((i+j) * isox) * padding, ((i - j) * isoy) * padding, (gamefield.GetLength(1)-j)/10f + (i )/100f), Quaternion.identity);
                gamefield[i, j] = Instantiate(tileprefab, new Vector3(((i +j/3.3f) * isox) * padding,  j * isoy * padding, 1-((gamefield.GetLength(1) - j) / 10f + (i) / 100f)), Quaternion.identity);

                gamefield[i, j].name = "Tile" + i + "-" + j;
                gamefield[i, j].transform.SetParent(parent.transform);
                Tile t = gamefield[i, j].GetComponent<Tile>();
                GameObject g = Instantiate(StandardTileContent);
                AddTileContent(g, t);
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


  
    void Start()
    {

        //Quick and Dirty solution  Problem Gameobject/Script variables get cleared on Play
        player1 = GameObject.Find("player1").GetComponent<HumanPlayer>();
        player2 = GameObject.Find("player2").GetComponent<HumanPlayer>();
       // player1.TurnStart();
        print("started");
    }

    //Create
    public void AddCharPrefab(GameObject instantiatedChar, Tile tile, int team)
    {
        parent = GameObject.Find(parentname).transform;
        Vector3 tilePos = tile.transform.position;
        tilePos.z = -1 + tilePos.z;
        instantiatedChar.transform.SetParent(parent.transform, false);
        instantiatedChar.transform.position = tilePos;
        Character c = instantiatedChar.GetComponent<Character>();
        if (team == 0)
        {
           c.transform.Rotate(0f, 180f, 0f);
            instantiatedChar.GetComponent<Renderer>().sharedMaterial = player1.TeamCharacterMaterial;
        }
        else
        {
            instantiatedChar.GetComponent<Renderer>().sharedMaterial = player2.TeamCharacterMaterial;
        }
        c.team = team;
        c.standingOn = tile;
        tile.tileContent.character = c;
        c.Init();
    }

    public void AddTileContent(GameObject instantiatedTileContent, Tile tile)
    {
        if (tile.tileContent != null)
        {
            if (Application.isEditor)
            {
                DestroyImmediate(tile.tileContent.gameObject);
            }
            else
            {
                Destroy(tile.tileContent.gameObject);
            }
        }
        tile.tileContent = instantiatedTileContent.GetComponent<TileContent>();
        instantiatedTileContent.transform.SetParent(tile.transform, false);
        instantiatedTileContent.transform.position = tile.transform.position;
        tile.tileContent.BaseInit(tile, this);
    }

    public void AddContent(GameObject instantiatedContent, Tile tile)
    {
        if (tile.tileContent != null)
        {
            if (tile.tileContent.content != null)
            {
                if (Application.isEditor)
                {
                    DestroyImmediate(tile.tileContent.content.gameObject);
                }
                else
                {
                    Destroy(tile.tileContent.content.gameObject);
                }
            }
            tile.tileContent.content = instantiatedContent.GetComponent<Content>();
            tile.tileContent.content.Init(tile);
            instantiatedContent.transform.SetParent(tile.transform, false);
            Vector3 vec = tile.transform.position;
            vec.z = -1f + vec.z;
            instantiatedContent.transform.position = vec;
        }

    }

    //GAMESECTION
    public bool busy = false;
    public bool CastAbility(Character character, Ability ability,Tile target )
    {
        if (ability.possibleCasts(character, character.standingOn).Contains(target))
        {
            EventManager.Ability();
            character.CastAbility(ability, target);
            return true;
        }
        return false;
    }
 



    private Character selectedChar;

    public bool SelectCharacter(Character character, bool mark)
    {
        if (busy)
        {
            return false;
        }
        busy = true;
        selectedChar = character;
        pf.generatePath(character.standingOn, character.movment);
        StartCoroutine(GenPath());
        return true;
    }



    public bool Move(Tile tile, Character character)
    {
        if (character == selectedChar && GetPath(tile, character.movment).Count >0 && !busy)
        {
            busy = true;
            character.standingOn.tileContent.character = null;
            tile.tileContent.character = character;
            character.standingOn = tile;
            StartCoroutine(Move(character.gameObject, GetPath(tile,character.movment), 20f));
            return true;
        }
        return false;
    }

 
    public List<Tile> GetPath(Tile tile,int range)
    {
      return pf.GetPath(tile,range);
    }

    public static void Kill(Character character)
    {
        character.alive = false;
        character.gameObject.SetActive(false);
        character.standingOn.tileContent.character = null;
        character.standingOn = null;
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
        getCurrentPlayer().FinishSelecting(pf.GetinRange());
        busy = false;
    }

    //follow a path needs testing 
    int currentPath = 0;
    Vector3 currentPos;
    IEnumerator Move(GameObject cha, List<Tile> path, float speed)
    {

        currentPath = path.Count - 1;
        currentPos = new Vector3(path[currentPath].transform.position.x, path[currentPath].transform.position.y, -1f + path[currentPath].transform.position.z);
        while (currentPath > -1)
        {

            cha.transform.position = Vector3.MoveTowards(cha.transform.position, currentPos, speed * Time.deltaTime);
            if (cha.transform.position == currentPos)
            {

                if (currentPath-- > 0)
                {
                    currentPos = new Vector3(path[currentPath].transform.position.x, path[currentPath].transform.position.y, -1f +path[currentPath].transform.position.z);
                }
            }
            yield return null;
        }
        busy = false;
        EventManager.Move();
        getCurrentPlayer().FinishedMoving();
    }

    //END


    public HumanPlayer getCurrentPlayer()
    {
        if (currentPlayer == 0)
        {
            return player1;
        }
        return player2;
    }


    //Events


    public void FinishTurn()
    {
        EventManager.TurnEnd(currentPlayer);
        currentPlayer = (currentPlayer+1)%2;
        getCurrentPlayer().TurnStart();
        EventManager.TurnStart(currentPlayer);
  
    }

    void Update()
    {
   
    }

}

