using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{

    //Objects prefabs
    public List<GameObject> characterPrefabs = new List<GameObject>();
    public List<GameObject> tilePrefabs = new List<GameObject>();
    public List<GameObject> contentPrefabs = new List<GameObject>();
    public GameObject charuiprefab; //<- todo needs own handler

    //references
    public static Field field;
    public static Pathfinder pf;
    public static EffectSpawner effectSpawner;
    public static Indicator indicator;
    public float moveSpeed = 60f;
    private Game game;
    public bool busy = false;

    void Start()
    {
        game = GetComponent<Game>();
        effectSpawner = GetComponent<EffectSpawner>();
        field = GetComponent<Field>();
        effectSpawner = GetComponent<EffectSpawner>();
        indicator = GetComponent<Indicator>();
        pf = new Pathfinder();
    }


    //Create
    public void AddCharPrefab(GameObject instantiatedChar, Tile tile, int team)
    {
        Transform parent = GameObject.Find(GetComponent<Field>().parentname).transform;
        Vector3 tilePos = tile.transform.position;
        tilePos.z = -1 + tilePos.z;
        instantiatedChar.transform.SetParent(parent.transform, false);
        instantiatedChar.transform.position = tilePos;
        Character c = instantiatedChar.GetComponent<Character>();
        if (team == 0)
        {
            c.transform.Rotate(0f, 180f, 0f);
            instantiatedChar.GetComponent<Renderer>().sharedMaterial = GetComponent<Game>().player1.TeamCharacterMaterial;
        }
        else
        {
            instantiatedChar.GetComponent<Renderer>().sharedMaterial = GetComponent<Game>().player2.TeamCharacterMaterial;
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
        tile.tileContent.BaseInit(tile, GetComponent<Field>());
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

    public bool CastAbility(Character character, Ability ability, Tile target)
    {
        if (ability.PossibleCasts(character, character.standingOn).Contains(target))
        {
            EventManager.Ability();
            character.CastAbility(ability, target);
            return true;
        }
        return false;
    }

    public void FinishedAbility()
    {
        game.GetCurrentPlayer().FinishedAbility();
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
        World.pf.generatePath(character.standingOn, character.movment);
        StartCoroutine(GenPath());
        return true;
    }



    public bool Move(Tile tile, Character character)
    {
        if (character == selectedChar && GetPath(tile, character.movment).Count > 0 && !busy)
        {
            busy = true;
            character.standingOn.tileContent.character = null;
            tile.tileContent.character = character;
            character.standingOn = tile;
            StartCoroutine(Move(character.gameObject, GetPath(tile, character.movment), moveSpeed));
            return true;
        }
        return false;
    }


    public List<Tile> GetPath(Tile tile, int range)
    {
        return World.pf.GetPath(tile, range);
    }

    public static void Kill(Character character)
    {
        character.alive = false;
        character.gameObject.SetActive(false);
        character.standingOn.tileContent.character = null;
        character.standingOn = null;
    }
    public void KillEffect(A_Effect effect)
    {

    }


    public List<Tile> GetAllSameConnectedTile(Tile tile, GameHelper.TileType type)
    {
        int pid = Pathfinder.UsePid();
        List<Tile> tiles = new List<Tile>();
        GetAllSameConnectedTile(tiles, pid, tile, type);
        return tiles;
    }

    private void GetAllSameConnectedTile(List<Tile> tiles,int pid,Tile tile,GameHelper.TileType type)
    {
        Tile z;
        for (int i = 0; i < tile.neighboors.Count; i++)
        {
            z = tile.neighboors[i];
            if (z != null && z.tileContent != null && z.tileContent.type == type && z.pid < pid)
            {
                tiles.Add(z);
                z.pid = pid;
                GetAllSameConnectedTile(tiles, pid, z,type);
            }
        }
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
        game.GetCurrentPlayer().FinishSelecting(pf.GetinRange());
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
                    currentPos = new Vector3(path[currentPath].transform.position.x, path[currentPath].transform.position.y, -1f + path[currentPath].transform.position.z);
                    //need stop condition!
                    path[currentPath].tileContent.WalkOver(cha.GetComponent<Character>());
                }
            }
            yield return null;
        }
        busy = false;
        EventManager.Move();
        game.GetCurrentPlayer().FinishedMoving();
    }
  
}