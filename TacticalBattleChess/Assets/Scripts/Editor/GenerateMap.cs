using UnityEngine;
using UnityEditor;


[CustomEditor (typeof (Field))]
public class GenerateMap : Editor {



    public override void OnInspectorGUI()
    {
  

        DrawDefaultInspector();
        Field f = target as Field; 
        
        EditorGUILayout.HelpBox("Editor Section", MessageType.Info);
        if(GUILayout.Button("Create Characters"))
        {
            string[] options = new string[f.characterPrefabs.Count];
            int selected = 0;
            for (int i = 0; i < f.characterPrefabs.Count; i++)
            {
                options[i] = f.characterPrefabs[i].name;
            }
            selected =  EditorGUILayout.Popup("Label", selected,options);
        }



        if (GUILayout.Button("Rebuild Map"))
        {
            f.GenerateMap();
            EditorUtility.SetDirty(f);
        }
    }

}
