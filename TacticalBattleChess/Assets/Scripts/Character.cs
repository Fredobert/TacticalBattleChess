using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICharacter
{
    GameObject Me
    {
        get;
        set;
    }

    int X
    {
        get;
        set;
    }
    int Y
    {
        get; set;
    }
}

public class Character : MonoBehaviour {


    private SelectManager sm;
    public int x;
    public int y;
    public int team;
    public GameObject standingOn;
    public Material material;
    // Use this for initialization
    void Start () {
        GameObject field = GameObject.Find("World");
        sm = field.GetComponent<SelectManager>();
        GetComponent<MeshRenderer>().material = material;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        print("selected a char");
        sm.SelectChar(gameObject);
    }
}
