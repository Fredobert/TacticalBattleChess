using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlewind : Ability {


    public int damage = 2;
    public int turns = 3;
    public float speed = 1f;



    // Use this for initialization
    void Start () {
		
	}

    public override void CastAbility(Character character, PFelement pfe)
    {
        StartCoroutine(Animation(pfe));
    }

    public override List<PFelement> possibleCasts(Character character, PFelement pfe)
    {
        return new List<PFelement> { pfe };
    }

    IEnumerator Animation(PFelement target)
    {
      
        for (int i = 0; i < turns; i++)
        {
            for (int j = 0; j < target.neighboors.Count; j++)
            {
                if (target.neighboors[j] != null && target.neighboors[j].GetComponent<Tile>().character != null)
                {
                    target.neighboors[j].GetComponent<Tile>().character.GetComponent<Character>().DealDamage(damage);
                }
            }
            yield return new WaitForSeconds(speed);
        }
    }
}
