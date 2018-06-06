using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Pathfinder : MonoBehaviour {
    //if target is not rechable this freezes
   // public Tile[,] field;
    public int pid = 0;
    public PFelement start;
  
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	}

    //version 1.0 Dijkstara because we want all multiple ways and only all possible outcomes within a certain raneg
    List<PFelement> pq;
    //beta
    List<PFelement> inRange;

    int dz = 0;
    int maxweight;
    public void generatePath(PFelement start, int maxweight = 500)
    {
        this.start = start;
        this.maxweight = maxweight;
        pq = new List<PFelement>();
        inRange = new List<PFelement>();
        pq.Add(start);
        start.g = 0;
        pid++;
    }


    //lazy verison with List.sort better PRiorityQueue
    private PFelement curElement;
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
         

        List<PFelement> neigh = curElement.neighboors;
        PFelement n;
        for (int i = 0; i < neigh.Count; i++)
        {
            n = neigh[i];
            if (n.pid != pid&&n.walkable)
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

        if (dz > 1000)
        {
            print("error");
            return true;
        }
        //could be buggy tetsing 
        return false;
    }
    //poor performance
    private void Add(PFelement t)
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
    private PFelement poll()
    {
        PFelement z = pq[pq.Count - 1];
        pq.RemoveAt(pq.Count - 1);
        return z;
    }
    public List<PFelement> GetinRange()
    {
        return inRange;
    }
    public List<PFelement> GetPath(PFelement goal)
    {
        List<PFelement> path = new List<PFelement>();
        PFelement zw = goal;
        
        while (!(zw.id == start.id))
        {
            //zw.mark();
            if (zw.from != null && zw.pid == pid)
            {
                path.Add(zw);
                zw = zw.from;
            }
            else
            {
                return new List<PFelement>();
            }


    
        }
        return path;
    }


}
