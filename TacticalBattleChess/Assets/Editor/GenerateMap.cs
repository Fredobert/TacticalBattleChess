using UnityEngine;
using UnityEditor;


[CustomEditor (typeof (Field))]
public class GenerateMap : Editor {
    public int oldx;
    public int oldy;
    public float oldpad;
    public bool init = false;


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Field f = target as Field;
        if (!init)
        {
            oldx = f.xlength;
            oldy = f.ylength;
            oldpad = f.padding;
            init = true;
        }
        if (oldpad != f.padding || oldx != f.xlength || oldy != f.ylength)
        {
            f.GenerateMap();
            oldx = f.xlength;
            oldy = f.ylength;
            oldpad = f.padding;
        }
  
    }

}
