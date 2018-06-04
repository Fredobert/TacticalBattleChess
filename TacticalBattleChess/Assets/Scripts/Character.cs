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

    // Use this for initialization
    void Start () {
        GameObject field = GameObject.Find("World");
        sm = field.GetComponent<SelectManager>();
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
