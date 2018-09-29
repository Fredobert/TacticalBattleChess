using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Water : TileContent
{

    private void OnEnable()
    {
        type = GameHelper.TileType.Water;
    }

    public override void Effect(int z, GameHelper.AbilityType type)
    {
        switch (type)
        {
            case GameHelper.AbilityType.Fire:
                //deal half fire damage
                z /= 2;
                break;
            case GameHelper.AbilityType.Thunder:
                break;
        }
        base.Effect(z, type);
    }

}
