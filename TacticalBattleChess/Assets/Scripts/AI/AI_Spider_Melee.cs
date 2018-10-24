using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//prototype!
public class AI_Spider_Melee : AIUnit
{
    public enum States { Searching, Moving, ReachableMoving,MultipleMoves};
    public Character spider;
    public States state;
    Tile targetTile;
    public override void Begin()
    {
        //set before
        Debug.Log("Melee start");
        spider = GetComponent<Character>();
        state = States.Searching;
        GetBestTarget();
    }

    private void GetBestTarget()
    {

        List<Character> enemies = aiPlayer.GetEnemies();
        List<Tile> possTiles = new List<Tile>();
        for (int i = 0; i < enemies.Count; i++)
        {
            for (int j = 0; j < enemies[i].standingOn.neighboors.Count; j++)
            {
                //TODO get rid of this abomination
                if (enemies[i].standingOn.neighboors[j]!= null && !enemies[i].standingOn.neighboors[j].tilehelper.Dangerous && (enemies[i].standingOn.neighboors[j].Walkable()|| enemies[i].standingOn.neighboors[j].GetCharacter() != null && enemies[i].standingOn.neighboors[j].GetCharacter() == character))
                {
                    possTiles.Add(enemies[i].standingOn.neighboors[j]);
                }
            }
        }
        Debug.Log("Spider Melee: Possible Moves: " + possTiles.Count);
        int bestIndex = GetNearestTile(possTiles);
        targetTile = possTiles[bestIndex];
        if (bestIndex == -1)
        {
            Debug.Log("no Move Reachable");
            aiPlayer.UnitFinish();
            return;
        }

        List<Tile> path = World.pf.GetPath(possTiles[bestIndex], 2000);
        if (path.Count == 0)
        {
            Debug.Log("in position");
            state = States.ReachableMoving;
            FinishedMoving(); //quick hack
        }
        else if (path.Count > spider.movment)
        {
            Debug.Log("in position");
            state = States.MultipleMoves;
            aiPlayer.Move(path[path.Count- spider.movment ], spider);
        }
        else
        {
            Debug.Log("no Move Reachable");
            aiPlayer.Move(possTiles[bestIndex], character);
            state = States.ReachableMoving;
        }

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
            path = World.pf.GetPath(tiles[i], 2000);
            if (path != null && path.Count != 0 && path.Count < min)
            {
                min = path.Count;
                minIndex = i;
            }
        }
        return minIndex;
    }

    public override void FinishedAbility()
    {

    }

    public override void FinishedMoving()
    {
        if (state == States.ReachableMoving)
        {
            if (spider.abilitys[1].IsNotOnCd())
            {
                DoAbility(spider.abilitys[1], spider.standingOn, 1);
            }
            else
            {
                for (int i = 0; i < targetTile.neighboors.Count; i++)
                {
                    if (targetTile.neighboors[i] != null &&targetTile.neighboors[i].GetCharacter() != null && targetTile.neighboors[i].GetCharacter().team != aiPlayer.teamid)
                    {
                        DoAbility(spider.abilitys[0], targetTile.neighboors[i], 1);
                        break;
                    }
                }
               
            }
        }
        else
        {
            aiPlayer.UnitFinish();
        }
    }

    private void DoAbility(Ability ability, Tile tile, int turndelay)
    {
        aiPlayer.AddAbility(spider, ability, tile, turndelay);
        aiPlayer.UnitFinish();
    }

}
