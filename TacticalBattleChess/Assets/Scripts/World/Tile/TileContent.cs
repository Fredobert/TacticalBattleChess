using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileContent : MonoBehaviour {

    protected Tile tile;
   
    protected bool walkable = true;
    protected Field field;
    public Character character;
    public Content content;
    // protected Field field;
    // Use this for initialization
    public Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    public virtual void Damage(int damage)
    {
        if (character != null)
        {
            character.DealDamage(damage);
        }
    }

    public virtual void Heal(int heal)
    {
        if (character != null)
        {
            character.Heal(heal);
        }
    }

    public virtual bool Walkable()
    {
        if (walkable && content != null)
        {
            return content.Walkable();
        }
        if (character != null)
        {
            return false;
        }
        return walkable;
    }
    public virtual Character GetCharacter()
    {
        return character;
    }

    //useless at the moment may be removed
    protected abstract void Init();
    public void AInit()
    {
        Init();
    }
    public void BaseInit(Tile tile, Field field)
    {
        this.field = field;
        this.tile = tile;
        Init();
    }


}
