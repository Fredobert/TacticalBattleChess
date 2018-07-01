using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class Ability : MonoBehaviour {

    public Sprite icon;

    public abstract List<PFelement> possibleCasts(Character character, PFelement pfe);


    public abstract void CastAbility(Character character,PFelement pfe);


	// Use this for initialization
	void Start () {
		
	}
}
