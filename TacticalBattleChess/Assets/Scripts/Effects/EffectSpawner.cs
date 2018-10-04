using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour {
    public A_Effect burning;
    public A_Effect thunder;



    public void Spawn(GameHelper.EffectType type , Tile tile)
    {
        switch (type)
        {
            case GameHelper.EffectType.Burning:
                SpawnIt(burning.gameObject, tile);
                break;
            case GameHelper.EffectType.Thunder:
                SpawnIt(thunder.gameObject, tile);
                break;
            default:
                break;
        }
    }

    private void SpawnIt(GameObject gameObject, Tile tile)
    {
        GameObject z =  Instantiate(gameObject, tile.transform);
        z.GetComponent<A_Effect>().Apply(tile);
    }

}
