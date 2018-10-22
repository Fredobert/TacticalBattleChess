using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRanged : Character {


    void Start()
    {
        abilitys.Add(GetComponent<PoisonSpit>());
        abilitys.Add(GetComponent<SpiderWeb>());
    }

    protected override void AbInit()
    {
        abilitys.Add(GetComponent<PoisonSpit>());
        abilitys.Add(GetComponent<SpiderWeb>());
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
