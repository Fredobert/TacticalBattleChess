using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class BuildWindow : EditorWindow {

    static Field f;
    int selectedPrefab = -1;
    int team;

    [MenuItem("Window/Builder")]
    public static void ShowWindow()
    {
        GetWindow<BuildWindow>("Builder");
        f = GameObject.Find("World").GetComponent<Field>();
    }

    void OnGUI()
    {
        team  = EditorGUILayout.IntField("team",team);
        Vector2 t = new Vector2 ( 0f, 0f );
        EditorGUILayout.BeginScrollView(Vector2.zero);
        GUIStyle style = EditorStyles.label;
        if (selectedPrefab == -1)
        {
            style = new GUIStyle(style);
            style.normal.textColor = Color.blue;
        }
        Rect rect = EditorGUILayout.GetControlRect();
        rect = EditorGUI.IndentedRect(rect);
        if (GUI.Button(rect, "none", style))
        {
            selectedPrefab = -1;
        }

        for (int i = 0; i < f.characterPrefabs.Count; i++)
        {
            style = EditorStyles.label;
            if (i == selectedPrefab)
            {
                style = new GUIStyle(style);
                style.normal.textColor = Color.blue;
            }
            rect = EditorGUILayout.GetControlRect();
            rect = EditorGUI.IndentedRect(rect);
            if (GUI.Button(rect,f.characterPrefabs[i].name,style))
            {
                selectedPrefab = i;
            }
        }
        EditorGUILayout.EndScrollView();
    }

    void OnSelectionChange()
    {
      GameObject z = PrefabUtility.InstantiatePrefab(f.characterPrefabs[selectedPrefab]) as GameObject;
        GameObject SelectedObject = Selection.activeTransform.gameObject;
        f.AddCharPrefab(z, SelectedObject.GetComponent<Tile>(),team);
    }





}
