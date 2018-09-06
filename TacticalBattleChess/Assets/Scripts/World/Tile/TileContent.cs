using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileContent : MonoBehaviour {

    public Tile tile;
   
    protected bool walkable = true;
    protected Field field;
    public Character character;
    public Content content;
    public GameHelper.TileType type;
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

    public void DoEffect(int z, GameHelper.EffectType type)
    {
        if (content != null)
        {
            content.Effect(z, type);
        }
        if (character != null)
        {
            character.Effect(z, type);
        }
    }

    public virtual void Effect(int z, GameHelper.EffectType type)
    {
        DoEffect(z, type);
    }

    public virtual Character GetCharacter()
    {
        return character;
    }

    public void BaseInit(Tile tile, Field field)
    {
        this.field = field;
        this.tile = tile;
    }


}
