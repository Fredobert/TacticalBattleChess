using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : Character {

    // Use this for initialization
    void Start()
    {
        abilitys.Add(GetComponent<Arrow>());
        abilitys.Add(GetComponent<Jump>());
        //GetComponent<MeshRenderer>().material = material;
    }

    protected override void AbInit()
    {
        abilitys.Add(GetComponent<Arrow>());
        abilitys.Add(GetComponent<Jump>());
    }
}
