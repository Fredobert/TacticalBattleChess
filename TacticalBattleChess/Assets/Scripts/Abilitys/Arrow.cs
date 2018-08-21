using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Ability { //NOT WORKING

    public GameObject prefab;
    public float speed = 0.2f;
    public int damage = 5;


    Field field;



    int directionOffset;
    Tile from;
    public override void CastAbility(Character character, Tile target)
    {
        GameObject g = Instantiate(prefab);
        StartCoroutine(Animation(g,target));
    }

    public override List<Tile> possibleCasts(Character character, Tile from)
    {
        return field.allTiles;
    }


    IEnumerator Animation(GameObject g,Tile target)
    {
        Vector3 vec;
        while (target.transform.position != g.transform.position) {
            vec = Vector3.MoveTowards(target.transform.position, g.transform.position, speed * Time.deltaTime);
            vec.z = g.transform.position.z;
            g.transform.position = vec;
            yield return null;
        }
        if (target.GetComponent<Tile>().GetCharacter() != null)
        {
            target.GetComponent<Tile>().GetCharacter().DealDamage(damage);
        }
        Destroy(g);
    }

    // Use this for initialization
    void Start()
    {
        base.Init();
        GameObject f = GameObject.Find("World");
        field = f.GetComponent<Field>();
    }
}
