using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIUnit : MonoBehaviour
{
    public Character character;
    public AIPlayer aiPlayer;

    public void Init(AIPlayer player)
    {
        aiPlayer = player;
    }

    public abstract void Begin();




    public abstract void FinishedMoving();

    public abstract void FinishedAbility();

    public void Finish()
    {
        aiPlayer.UnitFinish();
    }


}
