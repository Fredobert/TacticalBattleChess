using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {


    public delegate void SelectCharAction(GameObject character);
    public static event SelectCharAction OnSelectChar;

    public static void SelectCharacter(GameObject character)
    {
        if (OnSelectChar != null)
        {
            OnSelectChar(character);
        }
    }


    public delegate void SelectTileAction(GameObject tile);
    public static event SelectTileAction OnSelectTile;

    public static void SelectTile(GameObject tile)
    {
        if (OnSelectTile != null)
        {
            OnSelectTile(tile);
        }
    }

    public delegate void HoverTileAction(GameObject tile);
    public static event HoverTileAction OnHoverTile;

    public static void HoverTile(GameObject tile)
    {
        if (OnHoverTile != null)
        {
            OnHoverTile(tile);
        }
    }

    public delegate void AbilityAction(int id);
    public static event AbilityAction OnAbilityClick;

    public static void Ability(int id)
    {
        if (OnAbilityClick != null)
        {
            OnAbilityClick(id);
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






}
