using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour {

    public GameObject selectedTile = null;
    public GameObject selectedCharacter = null;
    public bool Tsel = false;
    public bool Csel = false;
    public bool Thover = false;
    public GameObject hover = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Hover(GameObject go)
    {
        hover = go;
        Thover = true;
    }

    public void SelectTile(GameObject go)
    {
        selectedTile = go;
        Tsel = true;
    }

    public void SelectChar(GameObject go)
    {
        selectedCharacter = go;
        Csel = true;
    }



}
