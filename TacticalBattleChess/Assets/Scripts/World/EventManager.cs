using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    /*
    public delegate void SelectCharAction(Character character,GameObject obj);
    public static event SelectCharAction OnSelectChar;

    public static void SelectCharacter(Character character, GameObject obj)
    {
        if (OnSelectChar != null)
        {
            OnSelectChar(character,obj);
        }
    }
    */

    public delegate void SelectTileAction(Tile tile, GameObject obj);
    public static event SelectTileAction OnSelectTile;

    public static void SelectTile( Tile tile, GameObject obj)
    {
        if (OnSelectTile != null)
        {
            OnSelectTile(tile,obj);
        }
    }


    public delegate void DeselectAction(Tile tile);
    public static event DeselectAction OnDeselect;
    public static void Deselect(Tile tile)
    {
        if (OnDeselect != null)
        {
            OnDeselect(tile);
        }
    }

    public delegate void LeaveAction(Tile tile);
    public static event LeaveAction OnLeave;
    public static void Leave(Tile tile)
    {
        if(OnLeave != null)
        {
            OnLeave(tile);
        }
    }

    public delegate void HoverTileAction( Tile tile, GameObject obj);
    public static event HoverTileAction OnHoverTile;

    public static void HoverTile( Tile tile, GameObject obj)
    {
        if (OnHoverTile != null)
        {
            OnHoverTile( tile, obj);
        }
    }

    public delegate void AbilityClickAction(Ability ability, Character character);
    public static event AbilityClickAction OnAbilityClick;

    public static void AbilityClick(Ability ability, Character character)
    {
        if (OnAbilityClick != null)
        {
            OnAbilityClick(ability,character);
        }
    }



    public delegate void TurnEndAction(int id);
    public static event TurnEndAction OnTurnEnd;

    public static void TurnEnd(int id)
    {
        if (OnTurnEnd != null)
        {
            OnTurnEnd(id);
        }
    }

    public delegate void TurnStartAction(int id);
    public static event TurnStartAction OnTurnStart;

    public static void TurnStart(int id)
    {
        if (OnTurnStart != null)
        {
            OnTurnStart(id);
        }
    }


    public delegate void MoveAction();
    public static event MoveAction OnMoveAction;

    public static void Move()
    {
        if (OnMoveAction != null)
        {
            OnMoveAction();
        }
    }

   public delegate void AbilityAction();
    public static event AbilityAction OnAbilityAction;

    public static void Ability()
    {
        if (OnAbilityAction != null)
        {
            OnAbilityAction();
        }
    }
}
