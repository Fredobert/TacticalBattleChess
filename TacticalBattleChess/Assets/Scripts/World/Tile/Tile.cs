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


    public Renderer rend;
    public TileContent tileContent;
    private Field field;
    //Mats

    // Use this for initialization

    public void Init(string id)
    {
        this.id = id;
    }


    void Start () {
		GameObject fieldO = GameObject.Find("World");
        field = fieldO.GetComponent<Field>();
        rend = GetComponent<Renderer>();
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
    //bainstorming
    public void Effect()
    {

    }
    //needs redesign
    public void range()
    {
        if(tileContent != null)
        {
            tileContent.mat.SetFloat("_RangeActive", 1.0f);
            tileContent.mat.SetFloat("_MarkActive", 0f);
        }
       
    }
    public void mark()
    {
        if (tileContent != null)
        {
            tileContent.mat.SetFloat("_RangeActive", 0f);
            tileContent.mat.SetFloat("_MarkActive", 1.0f);
        }
    }
    public void unmark()
    {

        if (tileContent != null)
        {
            tileContent.mat.SetFloat("_RangeActive", 1.0f);
            tileContent.mat.SetFloat("_MarkActive", 0f);
        }
    }
    public void reset()
    {
        if (tileContent != null)
        {
            tileContent.mat.SetFloat("_RangeActive", 0f);
            tileContent.mat.SetFloat("_MarkActive", 0f);
        }
    }

    public void Hover()
    {
        if (tileContent != null)
        {
            tileContent.mat.SetFloat("_OutlineActive", 1f);
            tileContent.mat.SetFloat("_Outline", 0.0284f);
            if (tileContent.character != null)
            {
                tileContent.character.GetComponent<Renderer>().material.SetFloat("_OutlineActive", 1f);
                tileContent.character.GetComponent<Renderer>().material.SetFloat("_Outline", 0.0284f);
            }
            else if (tileContent.content != null)
            {
                tileContent.content.GetComponent<Renderer>().material.SetFloat("_OutlineActive", 1f);
                tileContent.content.GetComponent<Renderer>().material.SetFloat("_Outline", 0.0284f);
            }
        }
    }
    public void Click()
    {
        if (tileContent != null)
        {
            tileContent.mat.SetFloat("_OutlineActive", 1f);
            tileContent.mat.SetFloat("_Outline", 0.05f);
        }
        if (tileContent.character != null)
        {
            tileContent.character.GetComponent<Renderer>().material.SetFloat("_OutlineActive", 1f);
            tileContent.character.GetComponent<Renderer>().material.SetFloat("_Outline", 0.05f);
        }
        else if (tileContent.content != null)
        {
            tileContent.content.GetComponent<Renderer>().material.SetFloat("_OutlineActive", 1f);
            tileContent.content.GetComponent<Renderer>().material.SetFloat("_Outline", 0.05f);
        }
    }
    public void UnHover()
    {
        if (tileContent != null)
        {
            tileContent.mat.SetFloat("_OutlineActive", 0f);
        }
        if (tileContent.character != null)
        {
            tileContent.character.GetComponent<Renderer>().material.SetFloat("_OutlineActive", 0f);
        }
        else if (tileContent.content != null)
        {
            tileContent.content.GetComponent<Renderer>().material.SetFloat("_OutlineActive", 0f);
        }
    }

}
