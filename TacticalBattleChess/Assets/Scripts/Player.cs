using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public List<Character> units;
    public int teamid;
    public int ap;


    List<PFelement> path = new List<PFelement>();
    List<PFelement> marked = new List<PFelement>();
    public Pathfinder pf;
   
    public Field field;



    public bool busy;
    // Use this for initialization
    void Start()
    {
        if (pf == null)
        {
            GameObject g = GameObject.Find("World");
            field = g.GetComponent<Field>();
            pf = g.GetComponent<Pathfinder>();
            EventManager.OnHoverTile += Hover;
            EventManager.OnSelectChar += SelectChar;
            EventManager.OnSelectTile += SelectTile;
            
        }
        
    }

    bool pathava = false;
    public GameObject HTile;
    public GameObject STile;
    public GameObject SCharacter;
    void Hover(GameObject tile)
    {
        if (field.currentPlayer == teamid && pathava && !busy)
        {
            HTile = tile;
            MarkPath(tile);
        }
    }

    void SelectChar(GameObject character)
    {
        if (field.currentPlayer == teamid&& !busy && character.GetComponent<Character>().team == teamid)
        {
            SCharacter = character;
            InitCharSelect(character);
        }
    }
    void SelectTile(GameObject tile)
    {
        if (field.currentPlayer == teamid  && !busy && pathava)
        {
            STile = tile;
            Move(tile);
        }
    }

    //player turn called from field update
    public bool turn() {
        return false;
    }




    public void InitCharSelect(GameObject character)
    {
        if(SCharacter != null)
        {
            Unselect();
        }
        busy = true;
        Character c = character.GetComponent<Character>();
        pf.generatePath(c.standingOn.GetComponent<PFelement>(), c.movment);
        StartCoroutine(GenPath());
    }

    public void Unselect()
    {
        for (int i = 0; i < marked.Count; i++)
        {
            marked[i].gameObject.GetComponent<Tile>().reset();
        }
    }

    public void Move(GameObject tile)
    {
        Unselect();
        busy = true;
        pathava = false;
        for (int i = 0; i < path.Count; i++)
        {
            path[i].gameObject.GetComponent<Tile>().unmark();
        }
        SCharacter.GetComponent<Character>().standingOn.GetComponent<PFelement>().walkable = true;
        STile.GetComponent<PFelement>().walkable = false;
        SCharacter.GetComponent<Character>().standingOn = STile;


        StartCoroutine(Move(SCharacter, path, 20f));
    }

    public void MarkPath(GameObject target)
    {
        for (int i = 0; i < path.Count; i++)
        {
            path[i].gameObject.GetComponent<Tile>().unmark();
        }
        path = pf.GetPath(target.GetComponent<PFelement>());
        for (int i = 0; i < path.Count; i++)
        {
            if (path[i].walkable)
            {
                path[i].gameObject.GetComponent<Tile>().mark();
            }
        }
    }

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
        marked = pf.GetinRange();
        for (int j = 0; j < marked.Count; j++)
        {
            marked[j].gameObject.GetComponent<Tile>().closed();
        }
        busy = false;
        pathava = true;
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
        field.FinishTurn();
    }

}
