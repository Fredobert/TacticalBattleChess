using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour {

   public Button firstAbility;

	// Use this for initialization
	void Start () {
        firstAbility.onClick.AddListener(FirstAbilityOnClick);
	}
	
    void FirstAbilityOnClick()
    {
        EventManager.Ability1();
    }

}
