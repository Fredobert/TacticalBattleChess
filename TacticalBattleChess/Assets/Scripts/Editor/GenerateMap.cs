using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(Game))]
public class GenerateMap : Editor
{
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();
        Game g = target as Game;

        EditorGUILayout.HelpBox("Building options", MessageType.Info);

    
        if (GUILayout.Button("Rebuild Map"))
        {
            //int grp = Undo.GetCurrentGroup();
            //Undo.RegisterFullObjectHierarchyUndo(GameObject.Find("Canvas"), "New World");
            //Undo.RegisterFullObjectHierarchyUndo(f.gameObject, "New World");
   
            UiHandler ui = g.GetComponent<UiHandler>();


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


            g.GenerateGame();
            //Undo.RegisterCreatedObjectUndo(GameObject.Find(f.parentname), "New World");
            EditorUtility.SetDirty(g);
            EditorUtility.SetDirty(g.GetComponent<Field>());
            EditorUtility.SetDirty(g.GetComponent<World>());
            //Undo.CollapseUndoOperations(grp);
        }
    }
}