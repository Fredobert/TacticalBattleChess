using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceStance : Ability {

    public int hpgain = 6;
    public int maxhpgain = 6;


    public override void CastAbility(Character character, Tile tile)
    {
        character.maxhealth += maxhpgain;
        character.Heal(hpgain);
        Finished();
    }

    public override List<Tile> DrawIndicator(Tile tile)
    {
        List<Tile> indicatorTiles = new List<Tile>();
        World.indicator.DrawDamage(tile,GameHelper.AbilityType.Heal, hpgain);
        indicatorTiles.Add(tile);
        return indicatorTiles;
    }

    public override List<Tile> PossibleCasts(Character character, Tile tile)
    {
        return new List<Tile> { tile };
    }


    // Use this for initialization
    void Start () {
        base.Init();
	}
}
