using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Field))]
public class GenerateMap : Editor
{
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();
        Field f = target as Field;

        EditorGUILayout.HelpBox("Building options", MessageType.Info);
        if (GUILayout.Button("Rebuild Map"))
        {
            UiHandler ui = f.GetComponent<UiHandler>();


            //ui.t1anchor.transform.DetachChildren();
            //ui.t0anchor.transform.DetachChildren();
            for (int i = 0; i < ui.charUIs.Count; i++)
            {
                if (ui.charUIs[i] != null)
                {
                    DestroyImmediate(ui.charUIs[i].gameObject);
                }
                
            }

            ui.charUIs = new List<CharUIElement>();



            f.GenerateMap();
            EditorUtility.SetDirty(f);
        }
    }
}