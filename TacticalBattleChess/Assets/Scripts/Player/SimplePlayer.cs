using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimplePlayer : MonoBehaviour {
    //NOT USED YET
    public int teamid;
    public string teamname;



    public abstract void FinishSelecting();
    public abstract void FinishCasting();
    public abstract void FinishMoving();
}
