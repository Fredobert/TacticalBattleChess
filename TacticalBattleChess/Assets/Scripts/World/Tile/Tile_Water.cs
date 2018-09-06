using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Water : TileContent
{

    private void OnEnable()
    {
        type = GameHelper.TileType.Water;
    }

    public override void Effect(int z, GameHelper.EffectType type)
    {
        switch (type)
        {
            case GameHelper.EffectType.Fire:
                //deal half fire damage
                z /= 2;
                break;
            case GameHelper.EffectType.Thunder:
                //thunder damage deals also damage to neighbor water tiles
                for (int i = 0; i < tile.neighboors.Count; i++)
                {
                    if (tile.neighboors[i] != null && tile.neighboors[i].tileContent != null && tile.neighboors[i].tileContent.type == GameHelper.TileType.Water)
                    {
                        tile.neighboors[i].tileContent.DoEffect(z, type);
                    }
                }
                break;
        }
        base.Effect(z, type);
    }

}
