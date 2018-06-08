using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {


    private SelectManager sm;
    public int x;
    public int y;
    public int team;
    public int movment;
    public GameObject standingOn;
    public Material material;
    // Use this for initialization
    void Start () {
        GameObject field = GameObject.Find("World");
        sm = field.GetComponent<SelectManager>();
        GetComponent<MeshRenderer>().material = material;
        movment = 3;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        print("selected a char");
        EventManager.SelectCharacter(gameObject);
    }
}
