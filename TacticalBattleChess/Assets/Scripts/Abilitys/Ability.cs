using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class Ability : MonoBehaviour {

    public Sprite icon;

    public abstract List<Tile> possibleCasts(Character character, Tile tile);


    public abstract void CastAbility(Character character,Tile tile);


	// Use this for initialization
	void Start () {
		
	}
}
