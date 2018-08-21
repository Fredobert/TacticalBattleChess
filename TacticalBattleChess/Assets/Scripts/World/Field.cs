using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
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
    public Material team1;
    public Material team2;

    public Player player1;
    public Player player2;

    [HideInInspector]
    public int currentPlayer;
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
   public GameObject playerprefab;
   public GameObject charuiprefab;

    public List<GameObject> characterPrefabs = new List<GameObject>();
    public List<GameObject> tilePrefabs = new List<GameObject>();
    public List<GameObject> contentPrefabs = new List<GameObject>();

    GameObject C;


    System.Random r;

    //test
    public float isox = 1f;
    public float isoy = 1f;
    public void GenerateMap()
    {
        GetComponent<UiHandler>().RemoveUI();
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
        r = new System.Random();
        allTiles = new List<Tile>();

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
                //iso version
                //gamefield[i, j] = Instantiate(tileprefab, new Vector3(((i+j) * isox) * padding, ((i - j) * isoy) * padding, (gamefield.GetLength(1)-j)/10f + (i )/100f), Quaternion.identity);
                gamefield[i, j] = Instantiate(tileprefab, new Vector3(((i +j/3.3f) * isox) * padding,  j * isoy * padding, 1-((gamefield.GetLength(1) - j) / 10f + (i) / 100f)), Quaternion.identity);

                gamefield[i, j].name = "Tile" + i + "-" + j;
                gamefield[i, j].transform.SetParent(parent.transform);
                Tile t = gamefield[i, j].GetComponent<Tile>();
                GameObject g = Instantiate(StandardTileContent);
                TileContent tc = g.GetComponent<TileContent>();
                t.tileContent = tc;
                g.transform.SetParent(t.transform);
                g.transform.position = t.transform.position;
                t.Init(i + "-:" + j);
                tc.AInit();
                allTiles.Add(t);
            }
        }

        int zx;
        int zy;
        //set neighbours
        for (int i = 0; i < gamefield.GetLength(0); i++)
        {
            for (int j = 0; j < gamefield.GetLength(1); j++)
            {
                int[] z = { i, j + 1, i + 1, j, i, j - 1, i - 1, j };
                List<Tile> n = new List<Tile>();
                for (int k = 0; k < z.Length; k += 2)
                {
                    zx = z[k];
                    zy = z[k + 1];
                    if (zx > -1 && zx < gamefield.GetLength(0) && zy > -1 && zy < gamefield.GetLength(1))
                    {
                        n.Add(gamefield[zx, zy].GetComponent<Tile>());
                    }
                    else
                    {
                        n.Add(null);
                    }
                }
                gamefield[i, j].GetComponent<Tile>().neighboors = n;
            }
        }
    }



    void Start()
    {

        //Quick and Dirty solution  Problem Gameobject/Script variables get cleared on Play
        player1 = GameObject.Find("player1").GetComponent<Player>();
        player2 = GameObject.Find("player2").GetComponent<Player>();
        print("started");
    }

    //Create
    public void AddCharPrefab(GameObject instantiatedChar, Tile tile, int team)
    {
        parent = GameObject.Find(parentname).transform;
        Vector3 tilePos = tile.transform.position;
        tilePos.z = -1 - tilePos.z;
        instantiatedChar.transform.position = tilePos;
        instantiatedChar.transform.SetParent(parent.transform);
        Character c = instantiatedChar.GetComponent<Character>();
        if (team == 0)
        {
            c.transform.Rotate(0f, 180f, 0f);
        }
        c.team = team;
        c.standingOn = tile;
        tile.tileContent.character = c;
        c.Init();
        CharUIElement cue = Instantiate(charuiprefab).GetComponent<CharUIElement>();
        if (Application.isEditor)
        {
            EditorUtility.SetDirty(GetComponent<UiHandler>());
        }

        cue.character = c;
        cue.Init();
        GetComponent<UiHandler>().AddUI(cue);
    }

    public void AddTileContent(GameObject instantiatedTileContent,Tile tile)
    {
        if (Application.isEditor)
        {
            DestroyImmediate(tile.tileContent.gameObject);
        }
        else
        {
            Destroy(tile.tileContent.gameObject);
        }
        tile.tileContent = instantiatedTileContent.GetComponent<TileContent>();
        instantiatedTileContent.transform.position = tile.transform.position;
        instantiatedTileContent.transform.parent = tile.transform;
        tile.tileContent.AInit();
    }

    public void AddContent(GameObject instantiatedContent, Tile tile)
    {
        if (tile.tileContent != null )
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
            Vector3 vec = tile.transform.position;
            vec.z = -1f -vec.z;
            instantiatedContent.transform.position = vec;
            instantiatedContent.transform.parent = tile.transform;
        }

    }

    //GAMESECTION

    public void CastAbility(Character character, Ability ability,Tile target )
    {
        EventManager.Ability();
        character.CastAbility(ability, target);
    }
    public void MarkTile(Tile tile, MarkType mt)
    {
        switch (mt)
        {
            case (MarkType.Marked):
                tile.range();
                break;
            case (MarkType.Path):
                tile.mark();
                break;
            case (MarkType.Standard):
                tile.reset();
                break;
            default:
                break;
        }
    }
    //not functional at the moment
    private bool busy;



    private Character selectedChar;

    public bool SelectCharacter(Character character, bool mark)
    {
        if (busy)
        {
            return false;
        }
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
            //not finshed need to generae path bevor

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
        Player z = getCurrentPlayer();
        getCurrentPlayer().FinishSelecting(pf.GetinRange());
        busy = false;
    }

    //follow a path needs testing 
    int currentPath = 0;
    Vector3 currentPos;
    IEnumerator Move(GameObject cha, List<Tile> path, float speed)
    {

        currentPath = path.Count - 1;
        currentPos = new Vector3(path[currentPath].transform.position.x, path[currentPath].transform.position.y, -1f - path[currentPath].transform.position.z);
        while (currentPath > -1)
        {

            cha.transform.position = Vector3.MoveTowards(cha.transform.position, currentPos, speed * Time.deltaTime);
            if (cha.transform.position == currentPos)
            {

                if (currentPath-- > 0)
                {
                    currentPos = new Vector3(path[currentPath].transform.position.x, path[currentPath].transform.position.y, -1f -path[currentPath].transform.position.z);
                }
            }
            yield return null;
        }
        EventManager.Move();
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

