using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour {
    public A_Effect burning;
    public A_Effect thunder;


    public void Spawn(GameHelper.EffectType type , Tile tile)
    {
        GameObject z = Instantiate(GetEffect(type).gameObject, tile.transform);
        z.GetComponent<A_Effect>().Apply(tile);
    }

    public A_Effect GetEffect(GameHelper.EffectType type)
    {
        switch (type)
        {
            case GameHelper.EffectType.Burning:
                return burning;
            case GameHelper.EffectType.Thunder:
                return thunder;
        }
        return null;
    }



}
