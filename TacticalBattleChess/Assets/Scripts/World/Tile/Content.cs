using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Content : MonoBehaviour
{

    public bool walkable = true;

    public virtual bool Walkable()
    {
        return walkable;
    }
}
