using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class Ability : MonoBehaviour {

    public Sprite icon;

    public abstract List<PFelement> possibleCasts(PFelement pfe);


    public abstract void CastAbility(PFelement pfe);


	// Use this for initialization
	void Start () {
		
	}
}
