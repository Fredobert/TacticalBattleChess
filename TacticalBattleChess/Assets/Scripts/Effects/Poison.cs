using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : A_Effect
{
    public int maxturns = 8;
    public int turns = 8;
    public int damage = 1;
    public GameHelper.AbilityType damageType = GameHelper.AbilityType.Poison;


    private Tile tile;
    private bool active = false;

    public override bool Apply(Tile tile)
    {
        EffectHandler handler = tile.tileContent.GetComponent<EffectHandler>();
        A_Effect effect = handler.GetFirst(GameHelper.EffectType.SpiderWeb);
        if (effect != null)
        {
            effect.AddDuration(1.0f);
            Destroy(this.gameObject);
            return false;
        }else  if (tile.tileContent != null)
        {
            this.tile = tile;
            active = true;
            tile.tileContent.OnWalkOver += WalkOver;
            EventManager.OnTurnEnd += TurnEnd;
            tile.tileContent.GetComponent<EffectHandler>().AddEffect(this);
        }
        return true;
    }

    public override void AddDuration(float duration)
    {
        turns += (int)(maxturns * duration);
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
            World.indicator.RemoveEffect(this,tile);
            Destroy(this.gameObject);
        }
    }

    public override List<Tile> DrawIndicator(Tile tile)
    {
        List<Tile> indicatorTiles = new List<Tile>();
        World.indicator.DrawDamage(tile, damageType, damage);
        indicatorTiles.Add(tile);
        return indicatorTiles;
    }

    public override GameHelper.EffectType Type()
    {
        return GameHelper.EffectType.Poison;
    }
}
