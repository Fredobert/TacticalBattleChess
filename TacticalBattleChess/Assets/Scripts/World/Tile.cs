using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    private SelectManager sm;
    public Material unmarked;
    public Material marked;
    public Material visit;
    public Material cloas;

    public Material prevMat;

	// Use this for initialization

	void Start () {
		GameObject field = GameObject.Find("World");
        prevMat = unmarked;
        sm = field.GetComponent<SelectManager>();
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
        sm.Hover(gameObject);
    }

    //bainstorming
    public void Effect()
    {

    }

    public void visited()
    {
       GetComponent<MeshRenderer>().material = visit;
        prevMat = visit;
    }

    public void closed()
    {
        GetComponent<MeshRenderer>().material = cloas;
        prevMat = cloas;
    }

    public void mark()
    {
        GetComponent<MeshRenderer>().material = marked;
    }
    public void unmark()
    {
        GetComponent<MeshRenderer>().material = prevMat;
    }
    public void reset()
    {
        GetComponent<MeshRenderer>().material = unmarked;
        prevMat = unmarked;
    }
}
