using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Un Select Abiliyt Path usw müssen vlt heir verwendet werden
[System.Serializable]
public class HumanController {

    public enum States { Init,Waiting,Ready,Selecting,Select,SelectForAbility,Casting,Moving,Ability};
    public States state = States.Init;

    public HumanController()
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
        switch (state)
        {
            case States.Waiting:
            case States.Ready:
                player.Hover(tile);
                break;
            case States.Select:
                player.DrawPath(tile);
                break;
            case States.Casting:
                player.AbilityHover(tile);
                break;
        }
    }
    void SelectTile(Tile tile, GameObject obj)
    {
        switch (state)
        {
            case States.Ready:
                if (tile.GetCharacter() != null && tile.GetCharacter().team == player.teamid && player.SelectCharacter(tile.GetCharacter()))
                {
                    state = States.Selecting;
                }
                break;
            case States.Select:
                if (tile.GetCharacter()!= null && tile.GetCharacter().team == player.teamid && player.GetSelectedCharacter() != tile.GetCharacter())
                {
                    player.UnSelect();
                    if (player.SelectCharacter(tile.GetCharacter()))
                    {
                        state = States.Selecting;
                    }
                    else
                    {
                        //Could not Select Character 
                        state = States.Ready;
                    }
                   
                }
                else if (player.Move(tile))
                {
                    player.UnSelect();
                    state = States.Ready;
                }
                break;
            case States.Casting:
                if (player.CastAbility(tile))
                {
                    player.UnAbility();
                    //CastAbility when Ability has no Coroutine AbilityFinished is called before state = States.Ability 
                    if (state == States.Casting)
                    {
                        state = States.Ability;
                    }
                }
                break;
        }
    }
    void SelectAbility(Ability ability, Character character)
    {
        switch (state)
        {
            case States.Ready:
            case States.Select:
                if (player.GetSelectedCharacter() != character)
                {
                    player.UnSelect();
                    player.SelectCharacter(character);
                    sAbility = ability;
                    sCharacter = character;
                    state = States.SelectForAbility;
                }
                else if (player.SelectAbility(ability,character))
                {
                    state = States.Casting;
                }
                break;
            case States.Casting:
                if (player.GetSelectedCharacter() != character)
                {
                    player.SelectCharacter(character);
                    state = States.Selecting;
                }
                else if (player.SelectAbility(ability, character))
                {
                    state = States.Casting;
                }else if (player.CastAbility(character.standingOn))
                {
                    player.UnAbility();
                    state = States.Ready;
                }
                break;
            default:
                break;
        }
    }
    void Deselect(Tile tile)
    {
        switch (state)
        {
            case States.Select:
                player.UnSelect();
                state = States.Ready;
                break;
            case States.Casting:
                player.UnAbility();
                state = States.Ready;
                break;
        }
    }

    Ability sAbility;
    Character sCharacter;
    public void FinischSelecting()
    {
        if (state == States.Selecting)
        {
            player.MarkRange();
            state = States.Select;
        }
        else if(state == States.SelectForAbility)
        {
            player.SelectAbility(sAbility, sCharacter);
            state = States.Casting;
        }

    }

    public void FinishMoving()
    {
        state = States.Ready;
    }
    public void FinishedAbility()
    {
        state = States.Ready;
    }

    public void Turn()
    {
        state = States.Ready;
    }

    public void Finish()
    {
        state = States.Waiting;
    }


    //Controller can have multiple human players
    public void Next(HumanPlayer player)
    {
        this.player = player;
    }

}

