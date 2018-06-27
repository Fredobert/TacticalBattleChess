using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharUIElement : MonoBehaviour {




    public Character character;

    //preset for Unity
    public Text CharName;
    public Image healthBarBG;
    public Image healthBar;
    public Text healthText;
    public Button ability1;
    public Button ability2;





    // Use this for initialization
    void Start() {
        if (character != null)
        {
            ability1.onClick.AddListener(Ability1Click);
            ability2.onClick.AddListener(Ability2Click);
            ability1.image.sprite = character.ability1.icon;
            healthText.text = character.maxhealth + "/" + character.health;
            ability2.image.sprite = character.ability2.icon;
            character.OnDamageTaken += RefreshHealthBar;

        }
        else
        {
            print("ERROR: Character in UI not Set");
        }
        
    }


    void RefreshHealthBar(int dmg){
        healthText.text = character.health + "/" + character.maxhealth;
        Vector3 scaleTemp = healthBar.transform.localScale;
        scaleTemp.x = (float)character.health  / character.maxhealth;
        healthBar.transform.localScale = scaleTemp;
    }

    void Ability1Click()
    {
        EventManager.Ability(character.ability1,character);
    }

    void Ability2Click()
    {
        EventManager.Ability(character.ability2, character);
    }


}
