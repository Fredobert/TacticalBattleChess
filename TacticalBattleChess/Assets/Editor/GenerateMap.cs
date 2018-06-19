using UnityEngine;
using UnityEditor;


[CustomEditor (typeof (Field))]
public class GenerateMap : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Field f = target as Field;
        f.GenerateMap();
    }

}
