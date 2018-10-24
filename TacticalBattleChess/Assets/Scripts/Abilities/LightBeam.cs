using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeam : Ability
{
    private Field field;
    private Tile target;
    public float speed = 0.9f;
    public int damage = 5;
    public GameObject prefab;
    // Use this for initializatiosn
    void Start()
    {
        base.Init();
        GameObject f = GameObject.Find("World");
        field = f.GetComponent<Field>();
    }


    public override void CastAbility(Character character, Tile target)
    {

        this.target = target;
        GameObject g = Instantiate(prefab);
        StartCoroutine(Animation(g));
    }

    public override List<Tile> PossibleCasts(Character character, Tile from)
    {
        return field.allTiles;
    }


    IEnumerator Animation(GameObject g)
    {
        g.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -4);
        yield return new WaitForSeconds(speed);


        if (target.GetCharacter() != null)
        {
            target.Effect(damage,GameHelper.AbilityType.Light);
        }
        Destroy(g);
        Finished();
    }

    public override List<Tile> DrawIndicator(Tile tile)
    {
        List<Tile> indicatorTiles = new List<Tile>();
        World.indicator.DrawDamage(tile, GameHelper.AbilityType.Light, damage);
        indicatorTiles.Add(tile);
        return indicatorTiles;
    }
}
