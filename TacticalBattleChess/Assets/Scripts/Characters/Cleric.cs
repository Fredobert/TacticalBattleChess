using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleric : Character
{


    // Use this for initialization
    void Start()
    {
        abilitys.Add(GetComponent<Heal>());
        abilitys.Add(GetComponent<LightBeam>());
        //GetComponent<MeshRenderer>().material = material;
    }

    protected override void AbInit()
    {
        abilitys.Add(GetComponent<Heal>());
        abilitys.Add(GetComponent<LightBeam>());
    }
}
