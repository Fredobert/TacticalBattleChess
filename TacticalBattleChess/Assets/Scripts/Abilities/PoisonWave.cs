using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonWave : Ability {


    public int damage = 4;
    public GameHelper.AbilityType type = GameHelper.AbilityType.Poison;
    public override void CastAbility(Character character, Tile tile)
    {
        for (int j = 0; j < character.standingOn.neighboors.Count; j++)
        {
            if (character.standingOn.neighboors[j] != null)
            {
                character.standingOn.neighboors[j].Effect(damage, type);
                World.effectSpawner.Spawn(GameHelper.EffectType.Poison, character.standingOn.neighboors[j]);
            }
            if (character.standingOn.diagonalNeighboors[j] != null)
            {
                character.standingOn.diagonalNeighboors[j].Effect(damage, type);
                World.effectSpawner.Spawn(GameHelper.EffectType.Poison, character.standingOn.diagonalNeighboors[j]);
            }
        }
        Finished();
    }

    public override List<Tile> DrawIndicator(Tile tile)
    {
        List<Tile> indicatorTiles = new List<Tile>();
        for (int j = 0; j < tile.neighboors.Count; j++)
        {
            if (tile.neighboors[j] != null)
            {
                World.indicator.DrawDamage(tile.neighboors[j], type, damage);
                indicatorTiles.Add(tile.neighboors[j]);
            }
            if (tile.diagonalNeighboors[j])
            {
                World.indicator.DrawDamage(tile.diagonalNeighboors[j], type, damage);
                indicatorTiles.Add(tile.diagonalNeighboors[j]);
            }
        }
        return indicatorTiles;
    }

    public override List<Tile> PossibleCasts(Character character, Tile tile)
    {
        List<Tile> tiles = new List<Tile>();
        tiles.Add(character.standingOn);
        return tiles;
    }
}
