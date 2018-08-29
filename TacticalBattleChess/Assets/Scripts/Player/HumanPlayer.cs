using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : MonoBehaviour {


    public int teamid;
    private Field field;
    public static HumanController hc;
    public int ap = 2;
    public int maxap = 2;
    public int freeMove = 1;
    // Use this for initialization
    void Start()
    {
        if (hc == null)
        {
            hc = new HumanController();
            hc.Init();
        }
        GameObject g = GameObject.Find("World");
        field = g.GetComponent<Field>();

        Reset();
    }

    bool SelectMode;
    bool busy;
    bool AbilityMode;

    Tile HTile;
    Tile STile;
    Ability SAbility;

    List<Tile> path = new List<Tile>();
    List<Tile> marked = new List<Tile>();
    List<Tile> markAb = new List<Tile>();

    //Basic Methods
    public void Hover(Tile tile)
    {
        if (busy || field.busy)
        {
            return;
        }
        //if Tile contains Character Mark him else unmark him   Todo Show Character Range and stats
        if (tile.GetCharacter() != null )
        {
            field.GetComponent<UiHandler>().Mark(tile.GetCharacter());
        }
        else if(HTile != null)
        {
            if (field.GetComponent<UiHandler>().markActive)
            {
                field.GetComponent<UiHandler>().HideMarker();
            }
        }
        if (HTile != null&& STile != HTile)
        {
            HTile.UnHover();
        }
        // only work if current Player = a Human Player
        if (field.currentPlayer == teamid)
        {
            if (SelectMode&& !AbilityMode)
            {

                MarkPath(tile);
            }
        }
        if (STile != tile)
        {
            tile.Hover();
        }
        HTile = tile;
    }
    public void SelectTile(Tile tile)
    {
        if (busy || field.busy || field.currentPlayer != teamid)
        {
            return;
        }

        if (AbilityMode)
        {
            if (markAb.Contains(tile) && field.CastAbility(STile.GetCharacter(), SAbility, tile))
            {
                UnAbility();
                UnSelect();
                ActionAbility();
            }
            return;
        }
        //Else if tile Contains Char
        if (tile.GetCharacter() != null && tile.GetCharacter().team == teamid)
        {
            if (SelectMode)
            {
                UnSelect();
            }
            tile.Click();
            tile.mark();
            SelectChar(tile);
        }
        if (SelectMode)
        {
            //ugly
            STile.reset();
            STile.UnHover();
            if (marked.Contains(tile) && field.Move(tile, STile.GetCharacter()))
            {
                tile.UnHover();
                UnSelect();
                busy = true;
                MoveAction();
            }
            else
            {
                STile.Click();
                STile.mark();
            }
            return;
        }
    }
    public void SelectAbility(Ability ability, Character character)
    {
        if (busy || field.busy || field.currentPlayer != teamid || character.team != teamid)
        {
            return;
        }
        if (AbilityMode)
        {
            STile.reset();
            STile.UnHover();
            UnAbility();
        }
        SAbility = ability;
        AbilityMode = true;
        //Ability is pressed without selecting first
        if (STile == null || STile.GetCharacter() == null || STile.GetCharacter() != character)
        {
            if (SelectMode)
            {
                UnSelect();
            }
            SelectChar(character.standingOn);
            return;
        }
        UnSelect();
        MarkAbility(ability, character.standingOn);
    }
    public void Deselect(Tile tile)
    {
        if (busy || field.busy || field.currentPlayer != teamid)
        {
            return;
        }
        if (AbilityMode)
        {
            UnAbility();
            MarkRange();
            STile.Click();
            STile.mark();
            return;
        }
        if (SelectMode)
        {
            UnSelect();
        }
    }
    //Finish Methods
    public void FinishSelecting(List<Tile> inRange)
    {
        marked = inRange;
        busy = false;
        if (AbilityMode)
        {
            MarkAbility(SAbility, STile);
            return;
        }
        MarkRange();
    }
    public void FinishedMoving()
    {

        busy = false;
    }

    //Help Methods
    void SelectChar(Tile characterTile)
    {
        if (characterTile.GetCharacter() != null && field.SelectCharacter(characterTile.GetCharacter(), false))
        {
            busy = true;
            SelectMode = true;
            STile = characterTile;
        }
    }

    void MarkRange()
    {
        for (int i = 0; i < marked.Count; i++)
        {
            if (marked[i] != null && marked[i] != STile.GetCharacter().standingOn)
            {
                marked[i].range();
            }
        }
    }
    void MarkAbility(Ability ability, Tile tile)
    {
        if (STile != null)
        {
            STile.Click();
        }
        markAb = ability.possibleCasts(tile.GetCharacter(), tile);
        for (int i = 0; i < markAb.Count; i++)
        {
            if (markAb[i] != null && markAb[i] != tile.GetCharacter().standingOn)
            {
                markAb[i].range();
            }
        }
    }

    void MarkPath(Tile target)
    {
        for (int i = 0; i < path.Count; i++)
        {
            path[i].unmark();
        }
        path = field.GetPath(target, STile.GetCharacter().movment);
        for (int i = 0; i < path.Count; i++)
        {
            if (path[i].Walkable())
            {
                path[i].mark();
            }
        }
    }

    void UnSelect()
    {
        if (STile != null)
        {
            STile.reset();
            STile.UnHover();
        }
        for (int i = 0; i < marked.Count; i++)
        {
            if (marked[i] != null)
            {
                marked[i].reset();
            }
        }
        SelectMode = false;
        marked = new List<Tile>();
        path = new List<Tile>();
    }

    void UnAbility()
    { 
        STile.UnHover();
        for (int i = 0; i < markAb.Count; i++)
        {
            if (markAb[i] != null && markAb[i] != STile.GetCharacter().standingOn)
            {
                markAb[i].reset();
            }
        }
        AbilityMode = false;
        SAbility = null;
        markAb = new List<Tile>();
    }
    void Reset()
    {
        if (SelectMode)
        {
            UnSelect();
        }
        if (AbilityMode)
        {
            UnAbility();
        }
        busy = false;
    }

    public void TurnStart()
    {
        hc.Next(this);
        ap = maxap;
        freeMove = 1;
    }

    public void MoveAction()
    {
        if (freeMove > 0)
        {
            freeMove--;
        }
        else
        {
            if (--ap <= 0)
            {
                Finish();
            }
        }
    }

    public void ActionAbility()
    {
        if (--ap <= 0)
        {
            Finish();
        }
    }

    public void Finish()
    {
        Reset();
        field.FinishTurn();
    }
        
}
