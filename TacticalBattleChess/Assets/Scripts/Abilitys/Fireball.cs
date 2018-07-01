using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Ability {


    public GameObject prefab;
    public float speed = 0.2f;
    public int damage = 10;




    int directionOffset;
    PFelement from;
    public override void CastAbility(Character character, PFelement target)
    {
     directionOffset =   from.neighboors.IndexOf(target);
        if (directionOffset == -1)
        {
            return;
        }
        from = from.neighboors[directionOffset];
        GameObject g = Instantiate(prefab);
        StartCoroutine(Animation(g));
   
         
    }

    public override List<PFelement> possibleCasts(Character character, PFelement from)
    {
        this.from = from;
        return from.neighboors;
    }
 

    IEnumerator Animation(GameObject g)
    {
        while (from != null && from.walkable != false)
        {
            g.transform.position = new Vector3(from.transform.position.x, from.transform.position.y, -4);
            from = from.neighboors[directionOffset];
            yield return new WaitForSeconds(speed);
        }
        if (from.GetComponent<Tile>().character != null)
        {
            from.GetComponent<Tile>().character.GetComponent<Character>().DealDamage(damage);
        }
        Destroy(g);
    }

    // Use this for initialization
    void Start () {
    }
	
}
