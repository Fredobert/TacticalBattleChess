using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Character
{


    // Use this for initialization
    void Start()
    {
        abilitys.Add(GetComponent<Fireball>());
        abilitys.Add(GetComponent<Thunder>());
        //GetComponent<MeshRenderer>().material = material;
    }

    protected override void AbInit()
    {
        abilitys.Add(GetComponent<Fireball>());
        abilitys.Add(GetComponent<Thunder>());
    }
}
