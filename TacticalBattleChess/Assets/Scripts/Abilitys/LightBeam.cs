using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeam : Ability
{
    private Field field;
    private PFelement target;
    public float speed = 0.9f;
    public int damage = 5;
    public GameObject prefab;
    // Use this for initializatiosn
    void Start()
    {
        GameObject f = GameObject.Find("World");
        field = f.GetComponent<Field>();
    }


    public override void CastAbility(Character character, PFelement target)
    {

        this.target = target;
        GameObject g = Instantiate(prefab);
        StartCoroutine(Animation(g));
    }

    public override List<PFelement> possibleCasts(Character character, PFelement from)
    {
        return field.allPfs;
    }


    IEnumerator Animation(GameObject g)
    {
        g.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -4);
        target.GetComponent<PFelement>().walkable = true;
        target.GetComponent<Tile>().refresh();
        yield return new WaitForSeconds(speed);


        if (target.GetComponent<Tile>().character != null)
        {
            target.GetComponent<Tile>().character.GetComponent<Character>().DealDamage(damage);
        }
        Destroy(g);
    }





}
