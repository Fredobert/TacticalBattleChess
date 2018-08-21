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
       
    }

    public override List<Tile> possibleCasts(Character character, Tile tile)
    {
        return new List<Tile> { tile };
    }


    // Use this for initialization
    void Start () {
        base.Init();
	}
}
