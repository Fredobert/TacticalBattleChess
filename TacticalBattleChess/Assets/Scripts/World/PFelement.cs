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
}
