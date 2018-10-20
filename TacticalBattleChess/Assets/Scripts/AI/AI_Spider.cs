using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Spider : AIUnit
{
    public enum States { Searching,Moving,ReachableMoving}
    public States state;

    public Character spider;

    public override void Begin()
    {

        //set before
        spider = GetComponent<Character>();
        state = States.Searching;
        GetBestTarget();
    }

    public override void FinishedAbility()
    {
      //Do Nothing
      //Do Nothing
    }

    public override void FinishedMoving()
    {
        if (state == States.ReachableMoving)
        {
            DoAbility(spider.abilitys[0], spider.standingOn.Go(1));
        }
    }


    private void GetBestTarget()
    {
        //asumption! Calculater later via x and y
        int DirectionReverse = 3;
        List<Character> enemies = aiPlayer.GetEnemies();
        List<Tile> possTiles = new List<Tile>();
        for (int i = 0; i < enemies.Count; i++)
        {
            Tile t = enemies[i].standingOn.Go(DirectionReverse);
            while (t != null && t.Walkable()&& !t.tilehelper.Dangerous)
            {
                possTiles.Add(t);
                t = t.Go(DirectionReverse);
            }
        }
        Debug.Log("Spider: Possible Moves: " + possTiles.Count);
        int bestIndex = GetNearestTile(possTiles);
        if (bestIndex == -1)
        {
            aiPlayer.UnitFinish();
            Debug.Log("Spider: No Move found");
            return;
        }
        // List<Tile> path = World.pf.GetPath(possTiles[bestIndex], 2000);
        if (World.pf.GetPath(possTiles[bestIndex], 2000).Count == 0)
        {
            Debug.Log("in position");
        }
        else
        {
            aiPlayer.Move(possTiles[bestIndex], character);
        }
        state = States.ReachableMoving;
    }

    private int GetNearestTile(List<Tile> tiles)
    {
        int minIndex = -1;
        int min = 2000;
        List<Tile> path;
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i] == spider.standingOn)
            {
                return i;
            }
            path =  World.pf.GetPath(tiles[i], 2000);
            if (path != null && path.Count != 0 && path.Count<min)
            {
                min = path.Count;
                minIndex = i;
            }
        }
        return minIndex;
    }
    private void DoAbility(Ability ability, Tile tile)
    {
        aiPlayer.AddAbility(spider, ability, tile,1);
        aiPlayer.UnitFinish();
    } 

}
