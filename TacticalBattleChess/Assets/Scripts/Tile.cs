using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    private SelectManager sm;

    public bool walkable = true;
    public int x;
    public int y;
    //for pathfinding
    public Tile from;
    public int pid;
    //patfinding cheat
    public int g;

    public Material unmarked;
    public Material marked;
    public Material visit;
    public Material cloas;


	// Use this for initialization

	void Start () {
		GameObject field = GameObject.Find("World");
        sm = field.GetComponent<SelectManager>();
        pid = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        sm.SelectTile(gameObject);
    }

     void OnMouseEnter()
    {
        print("hover");
        sm.Hover(gameObject);
    }

    //bainstorming
    public void Effect()
    {

    }

    public void visited()
    {
       GetComponent<MeshRenderer>().material = visit;
    }

    public void closed()
    {
        GetComponent<MeshRenderer>().material = cloas;
    }

    public void mark()
    {
        GetComponent<MeshRenderer>().material = marked;
    }
    public void unmark()
    {
        GetComponent<MeshRenderer>().material = unmarked;
    }
}
