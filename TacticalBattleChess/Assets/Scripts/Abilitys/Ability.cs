using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class Ability : MonoBehaviour {

    public Sprite icon;
    public int cd = 2;
    public int currentCd = 0;

    public abstract List<Tile> possibleCasts(Character character, Tile tile);


    public abstract void CastAbility(Character character,Tile tile);


	public void Init () {
        EventManager.OnTurnEnd += TurnEnd;
	}

    public void Finished()
    {
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
