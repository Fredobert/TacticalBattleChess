using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Pathfinder : MonoBehaviour {
    //if target is not rechable this freezes
    public Tile[,] field;
    public int pid = 0;
    public int sx;
    public int sy;
  
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	}

    //version 1.0 Dijkstara because we want all multiple ways and only all possible outcomes within a certain raneg
    List<Tile> pq;
    int dz = 0;
    public void generatePath(int x, int y, int maxweight)
    {
        sx = x;
        sy = y;
        pq = new List<Tile>();
        pq.Add(field[x, y]);
        field[x, y].g = 0;
        pid++;
    }


    //lazy verison with List.sort better PRiorityQueue
    private Tile curTile;
    public bool next()
    {
        if (pq.Count == 0)
        {
            return true;
        }

        curTile = poll();
        //curTile.g = 1;
        curTile.pid = pid;
        if (curTile.g <=2)
        {
            curTile.closed();
        }
         

        List<Tile> neigh = GetNeighbors(curTile.x , curTile.y);
        Tile n;
        for (int i = 0; i < neigh.Count; i++)
        {
            n = neigh[i];
            if (n.pid != pid)
            {
                n.g =1;
                n.pid = pid;
                if (!pq.Contains(n))
                {

                    //+1 maybe wrong in certain cases
                    n.g = curTile.g +1;
                    n.from = curTile;
                    Add(n);
                    continue;
                }
                if (curTile.g +1 > n.g)
                {
                    continue;
                }
                n.from = curTile;
                n.g = curTile.g + 1;
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
        pq.RemoveAt(pq.Count - 1);
        return z;
    }

    //only vor non diagonal
    public List<Tile> GetNeighbors(int x, int y)
    {
        int[] z = { x + 1, y ,  x-1,y,  x,y+1,  x,y-1  };
        List<Tile> n = new List<Tile>();

        int zx;
        int zy;

        for (int i = 0; i < z.Length; i+=2)
        {
            zx = z[i];
            zy = z[i + 1];
            if (zx > -1 && zx < field.GetLength(0) && zy > -1 && zy < field.GetLength(1))
            {
                if (field[zx,zy].walkable)
                {

                    n.Add(field[zx, zy]);

                 
                }
            }
        }
        return n;
    }
    public List<Tile> GetPath(int gx,int gy)
    {
        List<Tile> path = new List<Tile>();
        Tile zw = field[gx, gy];
        
        while (!(zw.x == sx && zw.y == sy))
        {
            //zw.mark();
            if (zw.from != null && zw.pid == pid)
            {
                path.Add(zw);
                zw = zw.from;
            }
            else
            {
                return new List<Tile>();
            }


    
        }
        return path;
    }


}
