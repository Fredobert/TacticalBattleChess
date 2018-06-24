using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    //quick solution
    public bool gras = true;

    public Material unmarked;
    public Material marked;
    public Material rock;
    public Material rangeIndicator;
    public GameObject character;
    public Material prevMat;

	// Use this for initialization

	void Start () {
		GameObject field = GameObject.Find("World");
        if (gras)
        {
            prevMat = unmarked;
        }
        else
        {
            prevMat = rock;
            visited();
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        EventManager.SelectTile(gameObject);
    }

     void OnMouseEnter()
    {
        EventManager.HoverTile(gameObject);
    }

    //bainstorming
    public void Effect()
    {

    }

    public void visited()
    {
       GetComponent<MeshRenderer>().material = rock;
        prevMat = rock;
    }

    public void closed()
    {
        GetComponent<MeshRenderer>().material = rangeIndicator;
        prevMat = rangeIndicator;
    }

    public void mark()
    {
        GetComponent<MeshRenderer>().material = marked;
    }
    public void unmark()
    {
        GetComponent<MeshRenderer>().material = prevMat;
    }
    public void refresh()
    {
        if (GetComponent<PFelement>().walkable)
        {
            GetComponent<MeshRenderer>().material = unmarked;
            prevMat = unmarked;
        }
        else
        {
            GetComponent<MeshRenderer>().material = rock;
            prevMat = rock;
        }
    }
    public void reset()
    {
        GetComponent<MeshRenderer>().material = unmarked;
        prevMat = unmarked;
    }
}
