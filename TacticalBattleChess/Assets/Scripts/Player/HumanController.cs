using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : ScriptableObject {

    private Field field;

    // Use this for initialization
    public void Init()
    {
        EventManager.OnHoverTile += Hover;
        EventManager.OnSelectTile += SelectTile;
        EventManager.OnAbilityClick += SelectAbility;
        EventManager.OnDeselect += Deselect;
 
    }
    //always current Human Player
    public HumanPlayer player;


    void Hover(Tile tile, GameObject obj)
    {
         player.Hover(tile);
    }
    void SelectTile(Tile tile, GameObject obj)
    {
        player.SelectTile(tile);
    }
    void SelectAbility(Ability ability, Character character)
    {
        player.SelectAbility(ability, character);
    }
    void Deselect(Tile tile)
    {
        player.Deselect(tile);
    }
   
   public void Next(HumanPlayer player)
    {
        this.player = player;
    }
}

