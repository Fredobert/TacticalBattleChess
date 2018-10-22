using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class Ability : MonoBehaviour {

    public Sprite icon;
    public int cd = 2;
    public int currentCd = 0;

    public abstract List<Tile> PossibleCasts(Character character, Tile tile);


    public abstract void CastAbility(Character character,Tile tile);

    public abstract List<Tile> DrawIndicator(Tile tile);

    public virtual void RemoveIndicator()
    {
    }

    public virtual bool IsNotOnCd()
    {
        return cd <= 0;
    }

	public void Init () {
        EventManager.OnTurnEnd += TurnEnd;
	}

    public void Finished()
    {
        RemoveIndicator();
        Game.world.FinishedAbility();
    }
    //Turn End because UI fires with TurnStart and this needs to be called before
    public void TurnEnd(int id)
    {
        if (currentCd >0 && cd > 0)
        {
            currentCd--;
        }
    }
}
