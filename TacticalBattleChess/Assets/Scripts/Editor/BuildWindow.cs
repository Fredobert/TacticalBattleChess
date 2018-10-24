
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildWindow : EditorWindow {

    World world;
    int selectedPrefab = -1;
    int prefabmodus = -1;
    int team;
    Texture2D deltex;
    int conSize;
    int offSet;
    Color selectedColor = new Color(32 / 255f, 115 / 255f, 249 / 255f);

    [MenuItem("Window/Builder")]
    public static void ShowWindow()
    {
        BuildWindow window = (BuildWindow)EditorWindow.GetWindow(typeof(BuildWindow));
        window.minSize = new Vector2(300, 150);
        window.Show();
    }

    public void OnEnable()
    {
        conSize = 60;
        offSet = 4;
        world = GameObject.Find("World").GetComponent<World>();
        deltex = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Textures/Other/Delete.png", typeof(Texture2D));
    }

    void OnGUI()
    {
        DrawMenu();

    }

    bool tchar = false;
    bool ttile = false;
    bool tcont = false;
    public void DrawMenu()
    {
        int maxContentsPerRow = ((int)position.width) / (conSize + offSet);
     
        EditorGUILayout.BeginScrollView(new Vector2(0, 0),GUIStyle.none,GUI.skin.verticalScrollbar);


        tchar = EditorGUILayout.Foldout(tchar, "Characters");
        if (tchar)
        {
            team = EditorGUILayout.IntField("team", team);

            DrawContent(world.characterPrefabs,true, maxContentsPerRow, 0);
        }
        ttile = EditorGUILayout.Foldout(ttile, "Tiles");
        if (ttile)
        {
            DrawContent(world.tilePrefabs,false, maxContentsPerRow, 1);
        }
        tcont = EditorGUILayout.Foldout(tcont, "Contents");
        if (tcont)
        {
            DrawContent(world.contentPrefabs,true, maxContentsPerRow, 2);
        }
        GUI.backgroundColor = Color.green;
        EditorGUILayout.HelpBox("Create selected Content by selecting a Tile in Scene", MessageType.Info);
        EditorGUILayout.EndScrollView();
    }

    public void DrawContent(List<GameObject> gameobjects,bool deleteOpt, int maxRowContent, int id)
    {
        count = 0;
        AddButton(new Texture2D(0, 0), "none", maxRowContent, -2, -2);
        if (deleteOpt)
        {
           AddButton(deltex, "Delete", maxRowContent, id, -1);
        }
        for (int i = 0; i < gameobjects.Count; i++)
        {
            Texture2D tex = gameobjects[i].GetComponent<SpriteRenderer>().sprite.texture;
            AddButton(tex, gameobjects[i].name, maxRowContent, id, i);
        }
        if (count % maxRowContent != 0)
        {
            EditorGUILayout.EndHorizontal();
        }

    }

    //Help Method for DrawContent
    int count = 0;
    private void AddButton(Texture2D tex, string text, int maxRowContent, int id, int value)
    {
        if (count % maxRowContent == 0)
        {
            EditorGUILayout.BeginHorizontal();
        }
        DrawButton(tex, text, (prefabmodus == id && selectedPrefab == value) ? selectedColor : Color.white, "", id, value);
        count++;
        if (count % maxRowContent == 0)
        {
            EditorGUILayout.EndHorizontal();
        }
    }

    public void DrawButton(Texture2D tex, string text, Color backGroundColor, string name, int id, int index)
    {
        GUI.backgroundColor = backGroundColor;
        GUIContent gc = new GUIContent();
        gc.text = text;
        gc.image = tex;
        //better create UI skin asset and load it
        GUI.skin.button.imagePosition = ImagePosition.ImageAbove;
        GUI.skin.button.alignment = TextAnchor.LowerCenter;
        GUI.skin.button.fontSize = 8;
        if (GUILayout.Button(gc, GUILayout.Width(conSize), GUILayout.Height(conSize)))
        {
            selectedPrefab = index;
            prefabmodus = id;
        }
    }

    void OnSelectionChange()
    {
        Tile t = GetSelectedTile();
        if (t == null || EditorApplication.isPlaying )
        {
            return;
        }
        switch (prefabmodus)
        {
            case 0:
                if (selectedPrefab == -1)
                {
                    if (t.tileContent.GetCharacter() != null)
                    {
                        RemoveCharacter(t);
                    }
                }
                else
                {
                    if (t.tileContent.GetCharacter() != null)
                    {
                        RemoveCharacter(t);
                    }
                    AddCharacter(t, world.characterPrefabs[selectedPrefab] as GameObject);
                }
                break;
            case 1:
                AddTileContent(t, world.tilePrefabs[selectedPrefab] as GameObject);
                break;
            case 2:
                if (selectedPrefab == -1)
                {
                    if (t.tileContent.content != null)
                    {
                        RemoveContent(t);
                    }
                }
                else
                {
                    if (t.tileContent.content != null)
                    {
                        RemoveContent(t);
                    }
                    AddContent(t, world.contentPrefabs[selectedPrefab] as GameObject);
                }
                break;
        }
    }

    //Add Remove Section
    private void RemoveCharacter(Tile t)
    {
        int grp = Undo.GetCurrentGroup();
        //weird RegisterObect not enough UiHandler.charuis will not get updated but with RegisterCompleteObjectUndo it works
        Undo.RegisterCompleteObjectUndo(world.GetComponent<UiHandler>().gameObject, "New Char");
        //Same here
        Undo.RegisterCompleteObjectUndo(t.tileContent, "Remove Char");

        //save position of Ui
        Undo.RegisterFullObjectHierarchyUndo(GameObject.Find("Canvas"), "Remove Char");
        CharUIElement cue = world.GetComponent<UiHandler>().Get(t.tileContent.GetCharacter());
        Undo.DestroyObjectImmediate(cue.gameObject);
        world.GetComponent<UiHandler>().ClearNones();
        world.GetComponent<UiHandler>().OrderUI();

        Undo.DestroyObjectImmediate(t.tileContent.GetCharacter().gameObject);
        t.tileContent.character = null;

        //has to be done or UiHandler charUis contains None refereces after play
        EditorUtility.SetDirty(world.GetComponent<UiHandler>());
        Undo.CollapseUndoOperations(grp);
    }
    private void AddCharacter(Tile t,GameObject prefab)
    {
        int grp = Undo.GetCurrentGroup();
        Undo.RecordObject(world.GetComponent<UiHandler>(), "New Char");
        Undo.RecordObject(t.tileContent, "New Char");

        GameObject z = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        world.AddCharPrefab(z, t, team);
        t.tileContent.type = GameHelper.TileType.Gras;
        Undo.RegisterCreatedObjectUndo(z, "New Char");

        CharUIElement cue = Instantiate(world.charuiprefab).GetComponent<CharUIElement>();
        cue.character = z.GetComponent<Character>();
        cue.Init();
        world.GetComponent<UiHandler>().AddUI(cue);
        Undo.RegisterCreatedObjectUndo(cue.gameObject, "New Char");

        //has to be done or UiHandler charUis contains None refereces after play
        EditorUtility.SetDirty(world.GetComponent<UiHandler>());
        Undo.CollapseUndoOperations(grp);
    }

    private void RemoveContent(Tile t)
    {
        Undo.RegisterCompleteObjectUndo(t.tileContent, "New Content");
        Undo.DestroyObjectImmediate(t.tileContent.content.gameObject);
    }

    private void AddContent(Tile t, GameObject prefab)
    {
        GameObject z = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        world.AddContent(z, t);
        Undo.RegisterCreatedObjectUndo(z, "New Content");
        Undo.RegisterCompleteObjectUndo(t.tileContent, "New Content");
    }

    private void AddTileContent(Tile t, GameObject prefab)
    {
        Character character = null;
        Content content = null;    

        int grp = Undo.GetCurrentGroup();
        if (t.tileContent != null)
        {
            character = t.tileContent.character;
            content = t.tileContent.content;
            Undo.RegisterCompleteObjectUndo(t, "New TileContent");
            Undo.DestroyObjectImmediate(t.tileContent.gameObject);
        }
        GameObject z = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        world.AddTileContent(z, t);
        t.tileContent.character = character;
        t.tileContent.content = content;
        Undo.RegisterCreatedObjectUndo(z, "New TileContent");
        Undo.RegisterCompleteObjectUndo(t, "New TileContent");

        Undo.CollapseUndoOperations(grp);
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
