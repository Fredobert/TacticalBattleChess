using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//  TODO    REMOVE GAMEOBJECTS ONLY COMPONENTS
public class Player : MonoBehaviour {
    public List<Character> units;
    public int teamid;
    public int ap = 3;


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
        EventManager.OnSelectChar += SelectChar;
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
        if (field.currentPlayer == teamid && pathava && !busy)
        {
            HTile = tile;
            MarkPath(tile);
        }
    }

    void SelectChar(Character character, GameObject obj)
    {
        if (character.team == teamid)
        {
            SCharacter = character;
            for (int i = 0; i < marked.Count; i++)
            {
                if (marked[i] != null)
                {
                    marked[i].reset();
                }
            }
            pathava = false;
            field.SelectCharacter(character, true);
            //InitCharSelect(character);
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
                    SelectChar(tile.GetCharacter(), tile.GetCharacter().gameObject);
                    
                }else if (SCharacter != null)
                {
                    STile = tile;
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
                SCharacter = null;
                pathava = false;
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
     
            if (character != SCharacter || SCharacter == null)
            {
                CharInit = true;
                SelectChar(character,character.gameObject);
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
            if (markAb[i] != null)
            {
                markAb[i].range();
            }

        }
    }

    public void FinishSelecting(List<Tile> inRange)
    {
        marked = inRange;
        for (int i = 0; i < inRange.Count; i++)
        {
            field.MarkTile(inRange[i],Field.MarkType.Marked);
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
        doAction();
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


    public void doAction()
    {
        if (--ap < 0)
        {
            field.FinishTurn();
        }
    }

}
