using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBite : Ability
{
    public int damage = 5;
    public GameHelper.AbilityType type = GameHelper.AbilityType.Normal;

    public override void CastAbility(Character character, Tile tile)
    {
        tile.Effect(damage, type);
        Finished();
    }

    public override List<Tile> DrawIndicator(Tile tile)
    {
        List<Tile> indicatorTiles = new List<Tile>();
        World.indicator.DrawDamage(tile, type, damage);
        indicatorTiles.Add(tile);
        return indicatorTiles;
    }

    public override List<Tile> PossibleCasts(Character character, Tile tile)
    {
        return character.standingOn.neighboors;
    }
}
