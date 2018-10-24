using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class A_Effect : MonoBehaviour {

    public abstract void Apply(Tile tile);

    public abstract List<Tile> DrawIndicator(Tile tile);


}
