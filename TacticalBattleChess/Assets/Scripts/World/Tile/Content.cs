using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Content : MonoBehaviour
{
    public GameHelper.ContentType type;
    public bool walkable = true;
    public Tile tile;




    public virtual bool Walkable()
    {
        return walkable;
    }

    public virtual void Effect(int z, GameHelper.EffectType type)
    {

    }

    public void Init(Tile tile)
    {
        this.tile = tile;
    }

    public void Remove()
    {
        tile.tileContent.content = null;
        gameObject.SetActive(false);
    }
}
