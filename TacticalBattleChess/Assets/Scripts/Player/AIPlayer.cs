using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : SimplePlayer {

    public struct AbilityAction
    {
        public int turnDelay;
        public Ability ability;
        public Character character;
        public Tile tile;
        public AbilityAction(Character character, Ability ability, Tile tile, int turnDelay)
        {
            this.turnDelay = turnDelay;
            this.ability = ability;
            this.tile = tile;
            this.character = character;
        }

        public bool Ready()
        {
            
            turnDelay = turnDelay - 1;
            return turnDelay <= 0;
        }
    }

    public int currentUnit = -1;
    Game game;
    World world;
    List<AIUnit> aiUnits;
    List<AbilityAction> actions = new List<AbilityAction>();
    bool abilityModus = false;
    void Start()
    {
        GameObject g = GameObject.Find("World");
        game = g.GetComponent<Game>();
        world = g.GetComponent<World>();
        aiUnits = new List<AIUnit>();
        for (int i = 0; i < units.Count; i++)
        {
            aiUnits.Add(units[i].GetComponent<AIUnit>());
            aiUnits[i].Init(this);
        }
    }


    public void Move(Tile tile, Character character)
    {
        world.Move(tile, character);
    }

    public void CastAbility(AbilityAction aa)
    {
        CastAbility(aa.character, aa.ability, aa.tile);
    }
    public void CastAbility(Character character, Ability ability, Tile tile)
    {
        World.indicator.RemoveAbility(ability, tile);
        world.CastAbility(character, ability, tile);
    }
    
    public void AddAbility(Character character,Ability ability, Tile tile, int turnDelay)
    {
        ability.PossibleCasts(character,character.standingOn);
        World.indicator.DrawAbility(ability, tile);
        if (turnDelay <= 0)
        {
            CastAbility(character, ability, tile);
            World.indicator.RemoveAbility(ability, tile);
        }
        else
        {
            actions.Add(new AbilityAction(character, ability, tile, turnDelay));
        }
    }
    
 
    public void UnitFinish()
    {
        DoUnits();
    }
    public override void Finish()
    {
        game.FinishTurn();
    }

    public override void FinishedAbility()
    {
        if (abilityModus)
        {
            DoAbilitys();
        }
      
    }

    public override void FinishedMoving()
    {
        aiUnits[currentUnit].FinishedMoving();
    }

    public override void FinishSelecting(List<Tile> inRange)
    {

        //if unit is Selected from DoUnits, Unit can begin
        Debug.Log("unit: " + currentUnit);
        aiUnits[currentUnit].Begin();


    }
    void FinishedAIAbilitys()
    {
        currentUnit = -1;
        abilityModus = false;
        DoUnits();
    }

    public void DoAbilitys()
    {
        currentUnit++;
        if (currentUnit >= actions.Count)
        {
            FinishedAIAbilitys();
            return;
        }
        if (actions[currentUnit].Ready())
        {
            AbilityAction aa = actions[currentUnit];
            actions.RemoveAt(currentUnit);
            currentUnit--;
            CastAbility(aa);
        }
        else
        {
            DoAbilitys();
        }
    }

    public void DoUnits()
    {
        currentUnit++;
        if (currentUnit >= units.Count)
        {
            Finish();
            return;
        }
        //start Selecting
        world.SelectCharacter(units[currentUnit], false);
    }

    public override void TurnStart()
    {
        currentUnit = -1;
        if (units.Count == 0)
        {
            Finish(); //End Game or so 
        }
        abilityModus = true;
        DoAbilitys();
       // DoUnits();
    }

	public List<Character> GetEnemies()
    {
        return game.player1.units;
    }

    public override void KillCharacter(Character character)
    {
       AbilityAction aa =  actions.Find(x => x.character == character);


        World.indicator.RemoveAbility(aa.ability, aa.tile);
        units.Remove(character);
        aiUnits.Remove(character.GetComponent<AI_Spider>());
        actions.Remove(aa);
    }
}
