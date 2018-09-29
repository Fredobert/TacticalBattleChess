using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Thunder : A_Effect
{

    public int damage = 1;


    private Tile tile;
    private bool active = false;
    public GameObject thunderPregab;

    private List<Tile> waterTiles;
    private List<GameObject> thunders;


    public override void Apply(Tile tile)
    {
        if (tile.tileContent != null && tile.tileContent.type == GameHelper.TileType.Water)
        {
            this.tile = tile;
            active = true;
            tile.tileContent.GetComponent<EffectHandler>().AddEffect(this);
            waterTiles = new List<Tile>();
            GetAllWaterTiles(waterTiles, Pathfinder.UsePid(), tile);
            thunders = new List<GameObject>();
            for (int i = 0; i < waterTiles.Count; i++)
            {
                thunders.Add(Instantiate(thunderPregab, waterTiles[i].transform));
            }
            StartCoroutine(Effect());
        }
    }

    private void GetAllWaterTiles(List<Tile> watertiles, int pid, Tile tile)
    {
        Tile z;
        for (int i = 0; i < tile.neighboors.Count; i++)
        {
            z = tile.neighboors[i];
            if (z != null && z.tileContent != null && z.tileContent.type == GameHelper.TileType.Water && z.pid < pid)
            {
                watertiles.Add(z);
                z.pid = pid;
                GetAllWaterTiles(watertiles, pid, z);
            }
        }
    }


    IEnumerator Effect()
    {
        for (int i = 0; i < waterTiles.Count; i++)
        {
            waterTiles[i].tileContent.Effect(damage, GameHelper.AbilityType.Thunder);
        }
        yield return new WaitForSeconds(1f);
        Kill();
    }
    //not tested
    public void Kill()
    {
        if (active)
        {
            for (int i = 0; i < thunders.Count; i++)
            {
                Destroy(thunders[i]);
            }
            thunders = new List<GameObject>();
            tile.tileContent.GetComponent<EffectHandler>().RemoveEffect(this);
            Destroy(this);
        }
    }

}
