using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour {

    public List<A_Effect> effects = new List<A_Effect>();

    public void AddEffect(A_Effect effect)
    {
        effects.Add(effect);
    }

    //public A_Effect Contains()


    public void RemoveEffect(A_Effect effect)
    {
        effects.Remove(effect);
    }
}
