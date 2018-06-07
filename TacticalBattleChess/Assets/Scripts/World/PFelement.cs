using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFelement : MonoBehaviour {

    //for pathfinding
    public PFelement from;
    public int pid = 0;
    public int g = 1;
    public List<PFelement> neighboors;
    public bool walkable;
    public string id;
    //maybe
    public int x;
    public int y;

    //0 N     1 E     2 S      3 W ...
    public PFelement Go(int direction)
    {
        int d = direction % 4;
        return (neighboors[d].walkable) ? neighboors[d] : null;
    }

}
