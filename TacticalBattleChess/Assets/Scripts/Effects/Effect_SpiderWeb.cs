using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_SpiderWeb : A_Effect
{

    public int turns = 2;
    public int movementDec = -10;


    private Tile tile;
    private Character character = null;
    private bool active = false;

    public override void Apply(Tile tile)
    {
        if (tile.tileContent != null)
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
}
