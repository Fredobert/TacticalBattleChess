using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : Ability
{
    private Field field;
    private Tile target;
    public float speed = 0.9f;
    public int damage = 5;
    public GameObject prefab;
    // Use this for initializatiosn
    void Start()
    {
        GameObject f = GameObject.Find("World");
        field = f.GetComponent<Field>();
    }


    public override void CastAbility(Character character, Tile target)
    {

        this.target = target;
        GameObject g = Instantiate(prefab);
        StartCoroutine(Animation(g));
    }

    public override List<Tile> possibleCasts(Character character, Tile from)
    {
        return field.allTiles;
    }


    IEnumerator Animation(GameObject g)
    {
        g.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -4);
        target.reset();
        yield return new WaitForSeconds(speed);


        if (target.GetCharacter() != null)
        {
            target.GetCharacter().DealDamage(damage);
        }
        Destroy(g);
    }





}
