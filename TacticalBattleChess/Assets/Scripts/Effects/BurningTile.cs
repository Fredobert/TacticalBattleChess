using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningTile : A_Effect {

    public int turns = 4;
    public int damage = 1;
    public GameHelper.AbilityType damageType = GameHelper.AbilityType.Fire;


    private Tile tile;
    private bool active = false;

    public override void Apply(Tile tile)
    {
        if (tile.tileContent != null)
        {
            this.tile = tile;
            active = true;
            tile.tileContent.OnWalkOver += WalkOver;
            EventManager.OnTurnEnd += TurnEnd;
            tile.tileContent.GetComponent<EffectHandler>().AddEffect(this);
        }
    }
	
    public void WalkOver(Character character)
    {
        character.Effect(damage, damageType);
    }
	
    public void TurnEnd(int id)
    {
        if (tile.tileContent.GetCharacter() != null)
        {
            tile.tileContent.Effect(damage, damageType);
        }
        turns--;
        if (turns == 0)
        {
            Kill();
        }
    }

	//not tested
    public void Kill()
    {
        if (active)
        {
            tile.tileContent.OnWalkOver -= WalkOver;
            EventManager.OnTurnEnd -= TurnEnd;
            tile.tileContent.GetComponent<EffectHandler>().RemoveEffect(this);
            World.indicator.RemoveEffect(this, tile);
            Destroy(this.gameObject);
        }
    }

    public override List<Tile> DrawIndicator(Tile tile)
    {
        List<Tile> indicatorTiles = new List<Tile>();
        World.indicator.DrawDamage(tile,damageType, damage);
        indicatorTiles.Add(tile);
        return indicatorTiles;
    }
}
