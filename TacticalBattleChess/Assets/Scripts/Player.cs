using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public List<Character> units;
    public int teamid;
    public int ap = 3;


    List<PFelement> path = new List<PFelement>();
    List<PFelement> marked = new List<PFelement>();
    public Pathfinder pf;

    public Field field;


    public bool busy;
    // Use this for initialization
    void Start()
    {
        if (pf == null)
        {
            GameObject g = GameObject.Find("World");
            field = g.GetComponent<Field>();
            pf = g.GetComponent<Pathfinder>();
            EventManager.OnHoverTile += Hover;
            EventManager.OnSelectChar += SelectChar;
            EventManager.OnSelectTile += SelectTile;
            EventManager.OnAbilityClick += AbilitySelected;
            ap = 3;
            pathava = false;
        }

    }

    bool pathava;
    public GameObject HTile;
    public GameObject STile;
    public GameObject SCharacter;

    void Hover(GameObject tile)
    {
        if (field.currentPlayer == teamid && pathava && !busy)
        {
            HTile = tile;
            MarkPath(tile);
        }
    }

    void SelectChar(GameObject character)
    {
        if (field.currentPlayer == teamid && !busy)
        {
            if (!AbilityModus && character.GetComponent<Character>().team == teamid)
            {
                SCharacter = character;

                for (int i = 0; i < marked.Count; i++)
                {
                    if (marked[i] != null)
                    {
                        marked[i].GetComponent<Tile>().refresh();
                    }

                }
                pathava = false;
                field.SelectCharacter(character.GetComponent<Character>(), true);
                //InitCharSelect(character);
            }
            else
            {
                for (int i = 0; i < markAb.Count; i++)
                {
                    if (markAb[i] != null)
                    {
                        markAb[i].GetComponent<Tile>().refresh();
                    }

                }
                AbilityModus = false;
                field.CastAbility(SCharacter.GetComponent<Character>(), SAbility, character.GetComponent<Character>().standingOn.GetComponent<PFelement>());
                SCharacter = null;
                pathava = false;
            }
        }
    }
    void SelectTile(GameObject tile)
    {
        if (field.currentPlayer == teamid && !busy)
        {
            if (!AbilityModus)
            {
                STile = tile;
                field.Move(tile, SCharacter.GetComponent<Character>());
            }
            else
            {
                for (int i = 0; i < markAb.Count; i++)
                {
                    if (markAb[i] != null)
                    {
                        markAb[i].GetComponent<Tile>().refresh();
                    }

                }
                AbilityModus = false;
                field.CastAbility(SCharacter.GetComponent<Character>(), SAbility, tile.GetComponent<PFelement>());
                SCharacter = null;
                pathava = false;
            }

        }
    }

    //Refacotor Ability Later
    Ability SAbility;
    List<PFelement> markAb = new List<PFelement>();
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
                SelectChar(character.gameObject);
                
            }
            else
            {
                MarkCastPossibilitys();
            }        
        }
    }
    public void MarkCastPossibilitys(){
        markAb = SAbility.possibleCasts(SCharacter.GetComponent<Character>().standingOn.GetComponent<PFelement>());
        AbilityModus = true;
        Unselect();
        for (int i = 0; i < markAb.Count; i++)
        {
            if (markAb[i] != null)
            {
                markAb[i].GetComponent<Tile>().closed();
            }

        }
    }

    public void FinishSelecting(List<PFelement> inRange)
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
                marked[i].GetComponent<Tile>().refresh();
            }
        }

        for (int i = 0; i < path.Count; i++)
        {
            path[i].GetComponent<Tile>().refresh();
        }
        pathava = false;
        doAction();
    }

   //End of Ability snipped

    public void Unselect()
    {
        for (int i = 0; i < marked.Count; i++)
        {
            marked[i].gameObject.GetComponent<Tile>().reset();
        }
    }

    public void MarkPath(GameObject target)
    {
        for (int i = 0; i < path.Count; i++)
        {
            path[i].gameObject.GetComponent<Tile>().unmark();
        }
        path = field.GetPath(target.GetComponent<PFelement>());
        for (int i = 0; i < path.Count; i++)
        {
            if (path[i].walkable)
            {
                path[i].gameObject.GetComponent<Tile>().mark();
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
