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

    static int conSize;
    static int offSet;
    [MenuItem("Window/Builder")]
    public static void ShowWindow()
    {

        BuildWindow window = (BuildWindow)EditorWindow.GetWindow(typeof(BuildWindow));
        window.Show();
        conSize = 60;
        offSet = 4;
        f = GameObject.Find("World").GetComponent<Field>();
    }
    bool tchar = false;
    bool ttile = false;
    bool tcont = false;

    void OnGUI()
    {
        DrawMenuFancy();

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
   
    //On Progress
    public void DrawMenuFancy()
    {
        int maxContentsPerRow = ((int)position.width) / (conSize + offSet);
        EditorGUILayout.BeginScrollView(new Vector2(0,0));


        tchar = EditorGUILayout.Foldout(tchar, "Characters");
        if(tchar)
        {
            team = EditorGUILayout.IntField("team", team);
            DrawTexButtons(f.characterPrefabs, maxContentsPerRow,0);
        }
        ttile = EditorGUILayout.Foldout(ttile, "Tiles");
        if (ttile)
        {
            DrawTexButtons(f.tilePrefabs, maxContentsPerRow, 1);
        }
        tcont = EditorGUILayout.Foldout(tcont, "Contents");
        if (tcont)
        {
            DrawTexButtons(f.contentPrefabs, maxContentsPerRow, 2);
        }
        EditorGUILayout.EndScrollView();
    }
    public void DrawTexButtons(List<GameObject> gameobjects, int maxRowContent,int id,int currentElement = 0 )
    {
        EditorGUILayout.BeginHorizontal();
        for (int i = currentElement; i < ((gameobjects.Count > currentElement + maxRowContent)? currentElement + maxRowContent:gameobjects.Count); i++)
        {
                Texture2D tex = gameobjects[i].GetComponent<SpriteRenderer>().sprite.texture;
                if (prefabmodus == id && selectedPrefab == i)
                {
                    GUI.backgroundColor = new Color(32/255f,115/255f,249/255f);
                }
                else
                {
                    GUI.backgroundColor = Color.white;
                }
                if (GUILayout.Button(tex, GUILayout.Width(conSize), GUILayout.Height(conSize)))
                {
                    selectedPrefab = i;
                    prefabmodus = id;
                }
        }
        EditorGUILayout.EndHorizontal();
        if (gameobjects.Count > currentElement+ maxRowContent)
        {
            DrawTexButtons(gameobjects, maxRowContent, currentElement + maxRowContent);
        }
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
