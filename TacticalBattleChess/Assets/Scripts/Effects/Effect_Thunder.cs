using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Thunder : A_Effect
{

    public int damage = 1;
    public GameHelper.AbilityType damageType = GameHelper.AbilityType.Thunder;

    private Tile tile;
    private bool active = false;
    public GameObject thunderPregab;

    private List<Tile> waterTiles;
    private List<GameObject> thunders;


    public override bool Apply(Tile tile)
    {
        if (tile.tileContent != null && tile.tileContent.type == GameHelper.TileType.Water)
        {
            this.tile = tile;
            active = true;
            tile.tileContent.GetComponent<EffectHandler>().AddEffect(this);

            waterTiles = Game.world.GetAllSameConnectedTile(tile, GameHelper.TileType.Water);
            thunders = new List<GameObject>();
            for (int i = 0; i < waterTiles.Count; i++)
            {
                thunders.Add(Instantiate(thunderPregab, waterTiles[i].transform));
            }
            StartCoroutine(Effect());
            return true;
        }
        return false;
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
            World.indicator.RemoveEffect(this, tile);

            Destroy(this.gameObject);
        }
    }

    public override List<Tile> DrawIndicator(Tile tile)
    {
        List<Tile> indicatorTiles = Game.world.GetAllSameConnectedTile(tile, GameHelper.TileType.Water);
        for (int i = 0; i < indicatorTiles.Count; i++)
        {
            World.indicator.DrawDamage(indicatorTiles[i], damageType, damage);
        }
        return indicatorTiles;
    }
    public override GameHelper.EffectType Type()
    {
        return GameHelper.EffectType.Thunder;
    }
}
