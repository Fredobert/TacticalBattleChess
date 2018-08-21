using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Ability
{

    public float speed = 2f;
    public int damage = 5;


    Field field;



    int directionOffset;
    Tile from;
    public override void CastAbility(Character character, Tile target)
    {
        StartCoroutine(Animation(character.gameObject, target));

        character.standingOn.tileContent.character = null;
        target.tileContent.character = character;
        character.standingOn = target;
    }

    public override List<Tile> possibleCasts(Character character, Tile from)
    {
        List<Tile> possible = new List<Tile>();
        for (int i = 0; i < field.allTiles.Count; i++)
        {
            if (field.allTiles[i] != null && field.allTiles[i].Walkable())
            {
                possible.Add(field.allTiles[i]);
            }
            
        }
        return possible;
    }


    IEnumerator Animation(GameObject g, Tile target)
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
        base.Init();
        GameObject f = GameObject.Find("World");
        field = f.GetComponent<Field>();
    }
}
