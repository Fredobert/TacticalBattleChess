using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability
{

    public int hpgain = 8;
    Field field;


    public override void CastAbility(Character character, Tile tile)
    {
        //Character healchar = tile.GetCharacter();
        tile.Effect(hpgain, GameHelper.EffectType.Heal);
    }

    public override List<Tile> possibleCasts(Character character, Tile tile)
    {
        List<Tile> possible = new List<Tile>();
        for (int i = 0; i < field.allTiles.Count; i++)
        {
            if (field.allTiles[i] != null && field.allTiles[i].GetCharacter() != null)
            {
                possible.Add(field.allTiles[i]);
            }

        }
        return possible;
    }


    // Use this for initialization
    void Start()
    {
        base.Init();
        GameObject f = GameObject.Find("World");
        field = f.GetComponent<Field>();
    }
}
