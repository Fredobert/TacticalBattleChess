using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceStance : Ability {

    public int hpgain = 6;
    public int maxhpgain = 6;


    public override void CastAbility(Character character, PFelement pfe)
    {
        character.maxhealth += maxhpgain;
        character.Heal(hpgain);
       
    }

    public override List<PFelement> possibleCasts(Character character, PFelement pfe)
    {
        return new List<PFelement> { pfe };
    }


    // Use this for initialization
    void Start () {
		
	}
}
