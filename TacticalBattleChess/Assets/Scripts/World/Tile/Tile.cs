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
    public string id;



    public TileContent tileContent;
    private Field field;
    //Mats
    public Material unmarked;
    public Material marked;
    public Material rangeIndicator;
    public Material prevMat;

    // Use this for initialization

    public void Init(string id)
    {
        this.id = id;
    }


    void Start () {
		GameObject fieldO = GameObject.Find("World");
        field = fieldO.GetComponent<Field>(); 
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        EventManager.SelectTile(this,gameObject);
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
    //bainstorming
    public void Effect()
    {

    }


    public void range()
    {
        tileContent.GetComponent<Renderer>().material = rangeIndicator;
        prevMat = rangeIndicator;
    }

    public void mark()
    {
        tileContent.GetComponent<Renderer>().material = marked;
    }
    public void unmark()
    {
        tileContent.GetComponent<Renderer>().material = prevMat;
    }
    public void reset()
    {
        tileContent.GetComponent<Renderer>().material = unmarked;
        prevMat = unmarked;
    }
}
