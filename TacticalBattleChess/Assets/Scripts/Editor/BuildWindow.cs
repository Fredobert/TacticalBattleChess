using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
public class BuildWindow : EditorWindow {

    static Field f;
    int selectedPrefab = -1;
    int prefabmodus = -1;
    int team;

    [MenuItem("Window/Builder")]
    public static void ShowWindow()
    {

        BuildWindow window = (BuildWindow)EditorWindow.GetWindow(typeof(BuildWindow));
        window.Show();

        f = GameObject.Find("World").GetComponent<Field>();
    }
    bool tchar = false;
    bool ttile = false;
    bool tcont = false;

    void OnGUI()
    {
     
        EditorGUILayout.BeginScrollView(Vector2.zero);
        GUIStyle style = EditorStyles.label;
        GUIStyle parentstyle = EditorStyles.largeLabel;
        parentstyle.normal.textColor = Color.red;
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
            prefabmodus = -1;
        }
        rect = EditorGUILayout.GetControlRect();
        rect = EditorGUI.IndentedRect(rect);
        //ugly but i dont know how better :(
        if (tchar)
        {
            if (GUI.Button(rect, "Chars", parentstyle))
            {
                tchar = false;

            }
        }
        else
        {
            if (GUI.Button(rect, "Chars", parentstyle))
            {
                tchar = true;
            }
        }
        if (tchar)
        {
            team = EditorGUILayout.IntField("team", team);
            GenerateContent(f.characterPrefabs, 0, style, ref rect);
        }
        rect = EditorGUILayout.GetControlRect();
        rect = EditorGUI.IndentedRect(rect);

        if (ttile)
        {
            if (GUI.Button(rect, "Tiles", parentstyle))
            {
                ttile = false;
            }
        }
        else
        {
            if (GUI.Button(rect, "Tiles", parentstyle))
            {
                ttile = true;
            }
        }
        if (ttile)
        {
            GenerateContent(f.tilePrefabs, 1, style, ref rect);
        }
        rect = EditorGUILayout.GetControlRect();
        rect = EditorGUI.IndentedRect(rect);

        if (tcont)
        {
            if (GUI.Button(rect, "Content", parentstyle))
            {
                tcont = false;
            }
        }
        else
        {
            if (GUI.Button(rect, "Content", parentstyle))
            {
                tcont = true;
            }
        }
        if (tcont)
        {
            GenerateContent(f.contentPrefabs, 2, style, ref rect);
        }
        rect = EditorGUILayout.GetControlRect();
        rect = EditorGUI.IndentedRect(rect);
 
        // tiles
        if (GUI.Button(rect, "Mark Scene Dirty", EditorStyles.miniButton))
        {
            EditorSceneManager.MarkAllScenesDirty();
        }
        EditorGUILayout.EndScrollView();

    }

    void OnSelectionChange()
    {

        Tile t;
        GameObject z;
        switch (prefabmodus)
        {
            case 0:
                t = GetSelectedTile();
                if (t != null)
                {
                    CharUIElement cue;
                    if (selectedPrefab == -1)
                    {
                        if (t.tileContent.GetCharacter() != null)
                        {


                            //Undo.RegisterCompleteObjectUndo(f.GetComponent<UiHandler>().gameObject, "New Char");
                            //Undo.RegisterCompleteObjectUndo(t.tileContent, "New Char");
                            Undo.RegisterFullObjectHierarchyUndo(GameObject.Find("Canvas"), "Remove Char");
                            Undo.DestroyObjectImmediate(f.GetComponent<UiHandler>().Get(t.tileContent.GetCharacter()).gameObject);
                            f.GetComponent<UiHandler>().ClearNones();
                            f.GetComponent<UiHandler>().OrderUI();
                            //cue = f.GetComponent<UiHandler>().RemoveUI(t.tileContent.GetCharacter());
                            //Undo.DestroyObjectImmediate(cue.gameObject);
                            //
                            Undo.RecordObject(f.GetComponent<UiHandler>(), "Remove Char");
                            Undo.DestroyObjectImmediate(t.tileContent.GetCharacter().gameObject);
                            //EditorUtility.SetDirty(f.GetComponent<UiHandler>());

                            //Todo Delete UI with undo flag
                        }
                    }
                    else
                    {
                        //Undo.RegisterCompleteObjectUndo(t.tileContent, "New Char");
                        Undo.RegisterCompleteObjectUndo(f.GetComponent<UiHandler>().gameObject, "New Char");

                        z = PrefabUtility.InstantiatePrefab(f.characterPrefabs[selectedPrefab]) as GameObject;
                        f.AddCharPrefab(z, t, team);
                        EditorUtility.SetDirty(z.GetComponent<Renderer>());
                        t.tileContent.type = GameHelper.TileType.Gras;
                        Undo.RegisterCreatedObjectUndo(z, "New Char");
                        cue = Instantiate(f.charuiprefab).GetComponent<CharUIElement>();
                        cue.character = z.GetComponent<Character>();
                        cue.Init();
                        f.GetComponent<UiHandler>().AddUI(cue);
                        Undo.RegisterCreatedObjectUndo(cue.gameObject, "New Char");
                        //has to be done or UiHandler charUis contains None refereces after play
                        //EditorUtility.SetDirty(f.GetComponent<UiHandler>());
                        Selection.objects = new Object[0];
                    }
                }
                break;
            case 1:
                t = GetSelectedTile();
                if (t != null)
                {
                    if (t.tileContent != null)
                    {
                        Undo.RegisterCompleteObjectUndo(t, "New TileContent");
                        Undo.DestroyObjectImmediate(t.tileContent.gameObject);
                    }
                    z = PrefabUtility.InstantiatePrefab(f.tilePrefabs[selectedPrefab]) as GameObject;
                    f.AddTileContent(z, t);
                    Undo.RegisterCreatedObjectUndo(z, "New TileContent");
                    Undo.RegisterCompleteObjectUndo(t, "New TileContent");
                    Selection.objects = new Object[0];
                }
                break;
            case 2:
                t = GetSelectedTile();
                if (t != null)
                {
                    if (t.tileContent.content!= null)
                    {
                        Undo.RegisterCompleteObjectUndo(t.tileContent, "New Content");
                        Undo.DestroyObjectImmediate(t.tileContent.content.gameObject);
                    }
                    z = PrefabUtility.InstantiatePrefab(f.contentPrefabs[selectedPrefab]) as GameObject;
                    f.AddContent(z, t);
                    Undo.RegisterCreatedObjectUndo(z, "New Content");
                    Undo.RegisterCompleteObjectUndo(t.tileContent, "New Content");
                    Selection.objects = new Object[0];
                }
                break;
        }
     
    }

    public void GenerateContent(List<GameObject> list,int id, GUIStyle style, ref Rect rect)
    {
        EditorGUI.indentLevel++;
        style = EditorStyles.label;
        if (-1 == selectedPrefab && prefabmodus == id)
        {
            style = new GUIStyle(style);
            style.normal.textColor = Color.blue;
        }
        rect = EditorGUILayout.GetControlRect();
        rect = EditorGUI.IndentedRect(rect);
        if (GUI.Button(rect, "none", style))
        {
            selectedPrefab = -1;
            prefabmodus = id;
        }
        for (int i = 0; i < list.Count; i++)
        {
            style = EditorStyles.label;
            if (i == selectedPrefab && prefabmodus == id)
            {
                style = new GUIStyle(style);
                style.normal.textColor = Color.blue;
            }
            rect = EditorGUILayout.GetControlRect();
            rect = EditorGUI.IndentedRect(rect);
            if (GUI.Button(rect, list[i].name, style))
            {
                selectedPrefab = i;
                prefabmodus = id;
            }
        }
        EditorGUI.indentLevel--;
    }

    public Tile GetSelectedTile()
    {
        if (Selection.activeTransform != null)
        {
            GameObject SelectedObject = Selection.activeTransform.gameObject;
            if (SelectedObject == null)
            {
                return null;
            }
            if (SelectedObject.GetComponent<Tile>() != null)
            {
                return SelectedObject.GetComponent<Tile>();
            }
            else if (SelectedObject.GetComponent<TileContent>() != null)
            {
                return SelectedObject.transform.parent.GetComponent<Tile>();
            }
            //return null when Character or Content Todo Replace it
        }

        return null;
    }



}
