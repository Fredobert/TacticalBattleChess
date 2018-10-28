using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//prototype!
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

    public bool ContainsType(GameHelper.EffectType type)
    {
        return effects.Exists(x => x.Type() == type);
    }

    public A_Effect GetFirst(GameHelper.EffectType type)
    {
        return effects.Find(x => x.Type() == type);
    }
}
