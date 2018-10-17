using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlewind : Ability {


    public int damage = 2;
    public int turns = 3;
    public float speed = 1f;



    // Use this for initialization
    void Start () {
        base.Init();
	}

    public override void CastAbility(Character character, Tile tile)
    {
        StartCoroutine(Animation(tile));
    }

    public override List<Tile> PossibleCasts(Character character, Tile tile)
    {
        return new List<Tile> { tile };
    }

    IEnumerator Animation(Tile target)
    {
      
        for (int i = 0; i < turns; i++)
        {
            for (int j = 0; j < target.neighboors.Count; j++)
            {
                if (target.neighboors[j] != null && target.neighboors[j].GetCharacter() != null)
                {
                    target.neighboors[j].Effect(damage,GameHelper.AbilityType.Normal);
                }
                if (target.diagonalNeighboors[j] != null && target.diagonalNeighboors[j].GetCharacter() != null)
                {
                    target.diagonalNeighboors[j].Effect(damage, GameHelper.AbilityType.Normal);
                }
            }
            yield return new WaitForSeconds(speed);
            Finished();
        }
    }
    public override List<Tile> DrawIndicator(Tile tile)
    {
        List<Tile> indicatorTiles = new List<Tile>();
        for (int j = 0; j < tile.neighboors.Count; j++)
        {
            if (tile.neighboors[j] != null)
            {
                World.indicator.DrawDamage(tile.neighboors[j], GameHelper.AbilityType.Normal, damage*turns);
                indicatorTiles.Add(tile.neighboors[j]);
            }
            if (tile.diagonalNeighboors[j])
            {
                World.indicator.DrawDamage(tile.diagonalNeighboors[j], GameHelper.AbilityType.Normal, damage * turns);
                indicatorTiles.Add(tile.diagonalNeighboors[j]);
            }
        }
        return indicatorTiles;
    }
}
