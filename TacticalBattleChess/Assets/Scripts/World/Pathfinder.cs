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
    List<Tile> pq;
    //beta
    List<Tile> inRange;

    int dz = 0;
    int maxweight;
    public void generatePath(Tile start, int maxweight = 500)
    {
        this.start = start;
        this.maxweight = maxweight;
        pq = new List<Tile>();
        allwalkables = new List<Tile>();
        inRange = new List<Tile>();
        pq.Add(start);
        start.g = 0;
        pid++;
    }


    //lazy verison with List.sort better PRiorityQueue
    private Tile curElement;
    public bool next()
    {
        if (pq.Count == 0)
        {
            return true;
        }

        curElement = poll();
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
                if (!pq.Contains(n))
                {

                    //+1 maybe wrong in certain cases
                    n.g = curElement.g +1;
                    n.from = curElement;
                    Add(n);
                    continue;
                }
                if (curElement.g +1 > n.g)
                {
                    continue;
                }
                n.from = curElement;
                n.g = curElement.g + 1;
                //reanrange in pq poor performance
                pq.Remove(n);
                Add(n);

            }
        }

        if (dz > 10000)
        {
            //ERROR
            return true;
        }
        //could be buggy tetsing 
        return false;
    }
    //poor performance
    private void Add(Tile t)
    {
        if (pq.Count == 0)
        {
            pq.Add(t);
        }
        else
        {

            for (int i = 0; i < pq.Count; i++)
            {
                if (pq[i].g <= t.g)
                {
                    pq.Insert(i, t);
                    return;
                }
            }
        }
    }
    private Tile poll()
    {
        Tile z = pq[pq.Count - 1];
        allwalkables.Add(z);
        pq.RemoveAt(pq.Count - 1);
        return z;
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
