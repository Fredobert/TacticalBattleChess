using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {


    //for Pathfinder
    public Tile from;
    public int pid = 0;
    public int g = 1;
    //pre set
    public List<Tile> neighboors;
    public List<Tile> diagonalNeighboors;
    public string id;



    public TileContent tileContent;
    private Field field;
    public TileHelper tilehelper;

    // Use this for initialization

    public void Init(string id)
    {
        this.id = id;
    }


    void Start () {
		GameObject fieldO = GameObject.Find("World");
        field = fieldO.GetComponent<Field>();
        tilehelper = GetComponent<TileHelper>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //only for Left Click
    void OnMouseDown()
    {
        EventManager.SelectTile(this,gameObject);
    }

    //for right click
    void OnMouseOver()
    {
    if (Input.GetMouseButtonDown(1))
    {
        EventManager.Deselect(this);
    }    
    }

    private void OnMouseExit()
    {
        
    }
    void OnMouseEnter()
    {
        EventManager.HoverTile(this,gameObject);
    }


    public bool Walkable()
    {
        if (tileContent == null)
        {
            return false;
        }
        return tileContent.Walkable();
    }

    public Character GetCharacter()
    {
        if (tileContent != null)
        {
            return tileContent.GetCharacter();
        }
        return null;
    }

    public void Effect(int z, GameHelper.AbilityType type)
    {
        if (tileContent != null)
        {
            tileContent.Effect(z, type);
        }
    }

}
