using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Ability {


    public GameObject prefab;
    public float speed = 0.2f;
    public int damage = 2;
    public Tile effectTile;



    int directionOffset;
    Tile from;
    public override void CastAbility(Character character, Tile target)
    {
     directionOffset =   from.neighboors.IndexOf(target);
        if (directionOffset == -1)
        {
            return;
        }
        from = from.neighboors[directionOffset];
        GameObject g = Instantiate(prefab);
        g.transform.Rotate(0, 0, directionOffset * -90);
        StartCoroutine(Animation(g));
    }

    public override List<Tile> DrawIndicator(Tile tile)
    {
        List<Tile> indicatorTiles = new List<Tile>();
        Tile ftile = from;
        directionOffset = ftile.neighboors.IndexOf(tile);
        if (directionOffset == -1)
        {
            return indicatorTiles;
        }
        ftile = ftile.neighboors[directionOffset]; 
        while (ftile != null && ftile.Walkable())
        {
            World.indicator.DrawIndicator(ftile, GameHelper.AbilityType.Fire);
            indicatorTiles.Add(ftile);
            ftile = ftile.neighboors[directionOffset];
        }
        if (ftile != null && ftile.tileContent != null)
        {
            World.indicator.DrawDamage(ftile, GameHelper.AbilityType.Fire, damage);
            indicatorTiles.Add(ftile);
            effectTile = ftile;

        }
        return indicatorTiles;
    }

    public override void RemoveIndicator()
    {

        base.RemoveIndicator();
    }

    public override List<Tile> PossibleCasts(Character character, Tile from)
    {
        this.from = from;
        return from.neighboors;
    }
 

    IEnumerator Animation(GameObject g)
    {
        while (from != null && from.Walkable())
        {
            g.transform.position = new Vector3(from.transform.position.x, from.transform.position.y, -4);
            from = from.neighboors[directionOffset];
            yield return new WaitForSeconds(speed);
        }
        if (from != null && from.tileContent != null)
        {
            from.Effect(damage,GameHelper.AbilityType.Fire);
            World.effectSpawner.Spawn(GameHelper.EffectType.Burning, from);
        }
        Destroy(g);
        Finished();
    }

    // Use this for initialization
    void Start () {
        base.Init();
    }
	
}
