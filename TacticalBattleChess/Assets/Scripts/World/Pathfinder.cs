using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class Pathfinder  {
    //if target is not rechable this freezes
   // public Tile[,] field;
    public int pid = 0;
    public Tile start;
    private List<Tile> allwalkables;


    //version 1.0 Dijkstara because we want all multiple ways and only all possible outcomes within a certain raneg
    PriorityQueue<Tile> pq;
    //beta
    List<Tile> inRange;

    int dz = 0;
    int maxweight;
    public void generatePath(Tile start, int maxweight = 500)
    {
        this.start = start;
        this.maxweight = maxweight;
        pq = new PriorityQueue<Tile>(200);
        allwalkables = new List<Tile>();
        inRange = new List<Tile>();
        pq.Add(start,0);
        start.g = 0;
        pid++;
    }


    //lazy verison with List.sort better PRiorityQueue
    private Tile curElement;
    public bool next()
    {
        if (pq.IsEmpty())
        {
            return true;
        }

        curElement = pq.Poll();
        //curElement.g = 1;
        curElement.pid = pid;
        //beta
        if (curElement.g <=maxweight)
        {
            inRange.Add(curElement);
        }
         

        List<Tile> neigh = curElement.neighboors;
        Tile n;
        for (int i = 0; i < neigh.Count; i++)
        {
            n = neigh[i];
            if (n!= null &&n.pid != pid&&n.Walkable())
            {
                n.g =1;
                n.pid = pid;
                int pos = pq.Contains(n, n.g);
                if (pos == -1)
                {
                    //+1 maybe wrong in certain cases
                    n.g = curElement.g +1;
                    n.from = curElement;
                    pq.Add(n,n.g);
                    continue;
                }
                if (curElement.g +1 > n.g)
                {
                    continue;
                }
                n.from = curElement;
                n.g = curElement.g + 1;
                //reanrange in pq poor performance
                pq.DecreasePrio(pos, n.g);

            }
        }

        //remove before build
        if (dz > 10000)
        {
            Debug.Log("Error in pathfinder!");
            //ERROR
            return true;
        }
        //could be buggy tetsing 
        return false;
    }

    public List<Tile> GetinRange()
    {
        return inRange;
    }

    public List<Tile> GetAllWalkables()
    {
        return allwalkables;
    }

    public List<Tile> GetPath(Tile goal, int range)
    {
        List<Tile> path = new List<Tile>();
        Tile zw = goal;
        int z = 0;
        while (!(zw.id == start.id))
        {                                               
            //zw.mark();
            if (zw.from != null && zw.pid == pid && z < range)
            {
                path.Add(zw);
                zw = zw.from;
                z++;
            }
            else
            {
                return new List<Tile>();
            }
        }
        return path;
    }


}
