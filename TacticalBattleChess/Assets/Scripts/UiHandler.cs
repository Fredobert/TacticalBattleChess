using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour {

   public Button firstAbility;
    public Button secondAbility;

    // Use this for initialization
    void Start () {
        firstAbility.onClick.AddListener(FirstAbilityOnClick);
        secondAbility.onClick.AddListener(SecondAbilityOnClick);
	}
	
    void FirstAbilityOnClick()
    {
        EventManager.Ability(0);
    }

    void SecondAbilityOnClick()
    {
        EventManager.Ability(1);
    }

}
