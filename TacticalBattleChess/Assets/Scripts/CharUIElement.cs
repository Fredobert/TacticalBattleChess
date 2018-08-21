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
            Init();
        }
        else
        {
            print("ERROR: Character in UI not Set");
        }
        
    }
    public void Init()
    {
        ability1.onClick.AddListener(Ability1Click);
        ability2.onClick.AddListener(Ability2Click);
        ability1.image.sprite = character.abilitys[0].icon;
        healthText.text = character.maxhealth + "/" + character.health;
        ability2.image.sprite = character.abilitys[1].icon;
        character.OnDamageTaken += RefreshHealthBar;
        character.OnHeal += RefreshHealthBar;
        EventManager.OnTurnStart += TurnStart;
        character.OnAbility += Cast;
    }
    void RefreshHealthBar(int dmg){
        healthText.text = character.health + "/" + character.maxhealth;
        Vector3 scaleTemp = healthBar.transform.localScale;
        scaleTemp.x = (float)character.health  / character.maxhealth;
        healthBar.transform.localScale = scaleTemp;
    }

    void Ability1Click()
    {
        if (character.alive && character.abilitys[0].currentCd == 0)
        {
            EventManager.AbilityClick(character.abilitys[0], character);
        }

    }

    void Ability2Click()
    {
        if (character.alive && character.abilitys[1].currentCd == 0)
        {
            EventManager.AbilityClick(character.abilitys[1], character);
        }
    }

    void Cast(Ability ability)
    {
        if (ability == character.abilitys[0])
        {
            ability1.GetComponentInChildren<Text>().text = character.abilitys[0].currentCd+ "";
        }
        else 
        {
            ability2.GetComponentInChildren<Text>().text = character.abilitys[1].currentCd + "";
        }
    }

    void TurnStart(int id)
    {
        if (character.abilitys[0].currentCd == 0)
        {
            ability1.GetComponentInChildren<Text>().text = "";
        }
        else
        {
            ability1.GetComponentInChildren<Text>().text = character.abilitys[0].currentCd + "";
        }

        if (character.abilitys[1].currentCd == 0)
        {
            ability2.GetComponentInChildren<Text>().text = "";
        }
        else
        {
            ability2.GetComponentInChildren<Text>().text = character.abilitys[1].currentCd + "";
        }
    }


}
