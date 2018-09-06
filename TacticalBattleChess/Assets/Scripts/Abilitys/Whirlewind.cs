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

    public override List<Tile> possibleCasts(Character character, Tile tile)
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
                    target.neighboors[j].Effect(damage,GameHelper.EffectType.Normal);
                }
            }
            yield return new WaitForSeconds(speed);
        }
    }
}
