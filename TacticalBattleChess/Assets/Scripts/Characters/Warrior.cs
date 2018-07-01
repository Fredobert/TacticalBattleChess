using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Character
{
    // Use this for initialization
    void Start()
    {
        abilitys.Add(GetComponent<DefenceStance>());
        abilitys.Add(GetComponent<Whirlewind>());
        //GetComponent<MeshRenderer>().material = material;
    }

    protected override void AbInit()
    {
        abilitys.Add(GetComponent<DefenceStance>());
        abilitys.Add(GetComponent<Whirlewind>());
    }
}