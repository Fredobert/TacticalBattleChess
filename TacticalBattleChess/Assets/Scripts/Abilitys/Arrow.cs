using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Ability { //NOT WORKING

    public GameObject prefab;
    public float speed = 0.2f;
    public int damage = 5;


    Field field;



    int directionOffset;
    PFelement from;
    public override void CastAbility(Character character, PFelement target)
    {
        GameObject g = Instantiate(prefab);
        StartCoroutine(Animation(g,target));
    }

    public override List<PFelement> possibleCasts(Character character, PFelement from)
    {
        return field.allPfs;
    }


    IEnumerator Animation(GameObject g,PFelement target)
    {
        Vector3 vec;
        while (target.transform.position != g.transform.position) {
            vec = Vector3.MoveTowards(target.transform.position, g.transform.position, speed * Time.deltaTime);
            vec.z = g.transform.position.z;
            g.transform.position = vec;
            yield return null;
        }
        if (target.GetComponent<Tile>().character != null)
        {
            target.GetComponent<Tile>().character.GetComponent<Character>().DealDamage(damage);
        }
        Destroy(g);
    }

    // Use this for initialization
    void Start()
    {
        GameObject f = GameObject.Find("World");
        field = f.GetComponent<Field>();
    }
}
