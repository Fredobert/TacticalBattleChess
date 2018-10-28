using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour {
    public A_Effect burning;
    public A_Effect thunder;
    public A_Effect spiderweb;
    public A_Effect poison;
   





    public void Spawn(GameHelper.EffectType type , Tile tile)
    {
        GameObject z = Instantiate(GetEffect(type).gameObject, tile.transform);
        if (z.GetComponent<A_Effect>().Apply(tile))
        {
            World.indicator.DrawEffect(z.GetComponent<A_Effect>(), tile);
        }
    }

    public A_Effect GetEffect(GameHelper.EffectType type)
    {
        switch (type)
        {
            case GameHelper.EffectType.Burning:
                return burning;
            case GameHelper.EffectType.Thunder:
                return thunder;
            case GameHelper.EffectType.SpiderWeb:
                return spiderweb;
            case GameHelper.EffectType.Poison:
                return poison;
        }
        return null;
    }



}
