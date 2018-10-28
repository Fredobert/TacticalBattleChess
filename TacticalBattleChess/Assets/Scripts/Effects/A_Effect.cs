using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class A_Effect : MonoBehaviour {

    public abstract bool Apply(Tile tile);

    public abstract List<Tile> DrawIndicator(Tile tile);

    public abstract GameHelper.EffectType Type();

    public virtual void AddDuration(float duration)
    {
        //do nothing default
    }
}
