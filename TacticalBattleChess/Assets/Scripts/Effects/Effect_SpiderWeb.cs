using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_SpiderWeb : A_Effect
{

    public int maxturns = 2;
    public int turns = 2;
    public int movementDec = -10;


    private Tile tile;
    private Character character = null;
    private bool active = false;

    public override bool Apply(Tile tile)
    {
        if (tile.tileContent != null)
        {
            EffectHandler handler = tile.tileContent.GetComponent<EffectHandler>();
            A_Effect effect =  handler.GetFirst(GameHelper.EffectType.SpiderWeb);
            if (effect != null)
            {
                effect.AddDuration(1.0f);
                Destroy(this.gameObject);
                return false;
            }
            else
            {
                if (tile.tileContent != null && tile.GetCharacter() != null)
                {

                    character = tile.GetCharacter();
                    character.movment += movementDec;
                }
                this.tile = tile;
                active = true;
                EventManager.OnTurnEnd += TurnEnd;
                tile.tileContent.GetComponent<EffectHandler>().AddEffect(this);
            }
        }
        return true;
    }

    public override void AddDuration(float duration)
    {
        turns +=(int)(maxturns * duration);
    }

    public void WalkOver(Character character)
    {
        if (this.character != null)
        {
            this.character.movment -= movementDec;
            this.character = null;
        }
        else 
        {
            this.character = character;
            this.character.movment += movementDec;
        }
    }


    public void TurnEnd(int id)
    {
      
        turns--;
        if (turns == 0)
        {
            Kill();
        }
    }

    //not tested
    public void Kill()
    {
        if (active)
        {
            if (tile.tileContent != null && tile.GetCharacter() != null)
            {
                this.character.movment -= movementDec;
            }
            EventManager.OnTurnEnd -= TurnEnd;
            tile.tileContent.GetComponent<EffectHandler>().RemoveEffect(this);
            Destroy(this.gameObject);
        }
        
    }

    public override List<Tile> DrawIndicator(Tile tile)
    {
        List<Tile> indicatorTiles = new List<Tile>();
        return indicatorTiles;
    }

    public override GameHelper.EffectType Type()
    {
        return GameHelper.EffectType.SpiderWeb;
    }
}
