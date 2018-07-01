using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability
{

    public int hpgain = 8;
    Field field;


    public override void CastAbility(Character character, PFelement pfe)
    {
        Character healchar = pfe.GetComponent<Tile>().character.GetComponent<Character>();
        healchar.Heal(hpgain);
    }

    public override List<PFelement> possibleCasts(Character character, PFelement pfe)
    {
        List<PFelement> possible = new List<PFelement>();
        for (int i = 0; i < field.allPfs.Count; i++)
        {
            if (field.allPfs[i] != null && field.allPfs[i].GetComponent<Tile>().character != null)
            {
                possible.Add(field.allPfs[i]);
            }

        }
        return possible;
    }


    // Use this for initialization
    void Start()
    {
        GameObject f = GameObject.Find("World");
        field = f.GetComponent<Field>();
    }
}
