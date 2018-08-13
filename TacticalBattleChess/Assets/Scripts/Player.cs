using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//  TODO    REMOVE GAMEOBJECTS ONLY COMPONENTS refactoring
public class Player : MonoBehaviour {
    public List<Character> units;
    public int teamid;
    public int ap = 2;
    public int maxap = 2;
    public int freeMove = 1;

    List<Tile> path = new List<Tile>();
    List<Tile> marked = new List<Tile>();
    public Pathfinder pf;

    public Field field;


    public bool busy;
    // Use this for initialization
    void Start()
    {
        GameObject g = GameObject.Find("World");
        field = g.GetComponent<Field>();
        pf = field.pf;
        EventManager.OnHoverTile += Hover;
        EventManager.OnSelectTile += SelectTile;
        EventManager.OnAbilityClick += AbilitySelected;
        ap = 3;
        pathava = false;
    }

    bool pathava;
    public Tile HTile;
    public Tile STile;
    public Character SCharacter;

    void Hover(Tile tile, GameObject obj)
    {
        if (field.currentPlayer == teamid)
        {
            if (HTile != null && (SCharacter == null || SCharacter.standingOn != HTile))
            {
                HTile.UnHover();
            }
            if (SCharacter == null || SCharacter.standingOn != tile)
            {
                tile.Hover();
                if (field.GetComponent<UiHandler>().markActive)
                {
                    field.GetComponent<UiHandler>().HideMarker();
                }
            }
            if (tile.GetCharacter() != null)
            {
                field.GetComponent<UiHandler>().Mark(tile.GetCharacter());
            }
            HTile = tile;
            if (field.currentPlayer == teamid && pathava && !busy)
            {
                MarkPath(tile);
            }
        }
    }

    void SelectChar(Character character, Tile tile)
    {
        if (character.team == teamid)
        {
            if (SCharacter != null)
            {
                SCharacter.standingOn.reset();
                SCharacter.standingOn.UnHover();
            }
            SCharacter = character;
            for (int i = 0; i < marked.Count; i++)
            {
                if (marked[i] != null)
                {
                   marked[i].reset();
                }
            }
            pathava = false;
            tile.Click();
            tile.mark();
            field.SelectCharacter(character, true);
     //       InitCharSelect(character);
        }
    }
    void SelectTile(Tile tile, GameObject obj)
    {
        if (field.currentPlayer == teamid && !busy)
        {
            if (!AbilityModus)
            {
                if (tile.GetCharacter() != null )
                {
                    SelectChar(tile.GetCharacter(), tile);
                    
                }else if (SCharacter != null)
                {
                    STile = tile;
                    SCharacter.standingOn.unmark();
                    SCharacter.standingOn.UnHover();
                    field.Move(tile, SCharacter);
                }
            }
            else
            {
                for (int i = 0; i < markAb.Count; i++)
                {
                    if (markAb[i] != null)
                    {
                        markAb[i].reset();
                    }
                }
                AbilityModus = false;
                field.CastAbility(SCharacter, SAbility, tile);
                SCharacter.standingOn.reset();
                SCharacter.standingOn.UnHover();
                SCharacter = null;
                pathava = false;
                ActionAbility();
            }

        }
    }

    //Refacotor Ability Later
    Ability SAbility;
    List<Tile> markAb = new List<Tile>();
    bool AbilityModus = false;
    bool CharInit = false;
    void AbilitySelected(Ability ability, Character character)
    {
        if (field.currentPlayer == teamid && !busy)
        {
            SAbility = ability;
     
            if (SCharacter == null||character != SCharacter )
            {
                CharInit = true;
                SelectChar(character,character.standingOn);
            }
            else
            {
                MarkCastPossibilitys();
            }        
        }
    }
    public void MarkCastPossibilitys(){
        markAb = SAbility.possibleCasts(SCharacter,SCharacter.standingOn);
        AbilityModus = true;
        Unselect();
        for (int i = 0; i < markAb.Count; i++)
        {
            if (markAb[i] != null && markAb[i] != SCharacter.standingOn)
            {
                markAb[i].range();
            }
        }
    }

    public void FinishSelecting(List<Tile> inRange)
    {
        marked = inRange;
        for (int i = 0; i < marked.Count; i++)
        {
            if (marked[i] != null && marked[i] != SCharacter.standingOn)
            {
                marked[i].range();
            }
        }
        pathava = true;

        if (CharInit)
        {
            CharInit = false;
            MarkCastPossibilitys();
        }
        
    }

    public void FinishedMoving()
    {
        for (int i = 0; i < marked.Count; i++)
        {
            if (marked[i] != null)
            {
                marked[i].reset();
            }
        }

        for (int i = 0; i < path.Count; i++)
        {
            path[i].reset();
        }
        pathava = false;
        SCharacter = null;
        MoveAction();
    }

   //End of Ability snipped

    public void Unselect()
    {
        for (int i = 0; i < marked.Count; i++)
        {
           marked[i].reset();
        }
    }

    public void MarkPath(Tile target)
    {
        for (int i = 0; i < path.Count; i++)
        {
            path[i].unmark();
        }
        path = field.GetPath(target,SCharacter.movment);
        for (int i = 0; i < path.Count; i++)
        {
            if (path[i].Walkable())
            {
                path[i].mark();
            }
        }
    }
    public void TurnStart()
    {
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
                field.FinishTurn();
            }
        }
    }

    public void ActionAbility()
    {
        if (--ap <= 0)
        {
            field.FinishTurn();
        }
    }

}
