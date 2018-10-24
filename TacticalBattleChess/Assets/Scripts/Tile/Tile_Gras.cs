using System.Collections.Generic;
using UnityEngine;

public class Tile_Gras : TileContent
{
    private void OnEnable()
    {
        type = GameHelper.TileType.Gras;
    }
}
