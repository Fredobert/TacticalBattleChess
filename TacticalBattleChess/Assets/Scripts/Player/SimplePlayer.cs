using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimplePlayer : MonoBehaviour {

    public int teamid;
    public string teamname;
    public Material TeamCharacterMaterial;
    public List<Character> units = new List<Character>();

    public abstract void FinishSelecting(List<Tile> inRange);
    public abstract void FinishedAbility();
    public abstract void FinishedMoving();
    public abstract void TurnStart();
    public abstract void KillCharacter(Character character);
    public abstract void Finish();


}
