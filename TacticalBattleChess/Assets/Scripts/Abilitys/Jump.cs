using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Ability
{

    public float speed = 2f;
    public int damage = 5;


    Field field;



    int directionOffset;
    PFelement from;
    public override void CastAbility(Character character, PFelement target)
    {
        StartCoroutine(Animation(character.gameObject, target));

        character.standingOn.GetComponent<PFelement>().walkable = true;
        character.standingOn.GetComponent<Tile>().character = null;
        target.GetComponent<PFelement>().walkable = false;
        target.GetComponent<Tile>().character = character.gameObject;
        character.standingOn = target.gameObject;
    }

    public override List<PFelement> possibleCasts(Character character, PFelement from)
    {
        List<PFelement> possible = new List<PFelement>();
        for (int i = 0; i < field.allPfs.Count; i++)
        {
            if (field.allPfs[i] != null && field.allPfs[i].walkable)
            {
                possible.Add(field.allPfs[i]);
            }
            
        }
        return possible;
    }


    IEnumerator Animation(GameObject g, PFelement target)
    {
        Vector3 goal = new Vector3(target.transform.position.x, target.transform.position.y, g.transform.position.z);
        Vector3 vec;
        while (g.transform.position != goal)
        {
            vec = Vector3.MoveTowards(g.transform.position, goal, speed * Time.deltaTime);
            vec.z = g.transform.position.z;
            g.transform.position = vec;
            yield return null;
        }
    }

    // Use this for initialization
    void Start()
    {
        GameObject f = GameObject.Find("World");
        field = f.GetComponent<Field>();
    }
}
