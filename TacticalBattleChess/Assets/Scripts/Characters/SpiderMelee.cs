using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMelee : Character {


    void Start()
    {
        abilitys.Add(GetComponent<SpiderBite>());
        abilitys.Add(GetComponent<PoisonWave>());
    }

    protected override void AbInit()
    {
        abilitys.Add(GetComponent<SpiderBite>());
        abilitys.Add(GetComponent<PoisonWave>());
    }

    public override void Effect(int z, GameHelper.AbilityType type)
    {
        if (type == GameHelper.AbilityType.Poison)
        {
            z = 0; //immun to poison
        }
        base.Effect(z, type);
    }
}
