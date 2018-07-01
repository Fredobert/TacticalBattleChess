using UnityEngine;
using UnityEditor;

public class PrefabExplorer : EditorWindow
{
    //from:https://gist.github.com/sabresaurus/629e27791b5c0157f4c7426341f15bf1

    // MIT License
    // 
    // Copyright (c) 2017 Sabresaurus
    // 
    // Permission is hereby granted, free of charge, to any person obtaining a copy
    // of this software and associated documentation files (the "Software"), to deal
    // in the Software without restriction, including without limitation the rights
    // to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    // copies of the Software, and to permit persons to whom the Software is
    // furnished to do so, subject to the following conditions:
    // 
    // 	The above copyright notice and this permission notice shall be included in all
    // 	copies or substantial portions of the Software.
    // 
    // 	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    // 	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    // 	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    // 	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    // 	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    // 	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    // 	SOFTWARE.

    Vector2 scrollPosition = Vector2.zero;

    [MenuItem("Window/Prefab Explorer")]
    static void CreateAndShow()
    {
        EditorWindow window = EditorWindow.GetWindow<PrefabExplorer>("Prefab Explorer");

        window.Show();
    }

    private void OnSelectionChange()
    {
        Repaint();
    }

    void OnGUI()
    {
        GameObject prefabRoot = PrefabUtility.FindPrefabRoot(Selection.activeGameObject);
        if (prefabRoot != null)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            DrawName(prefabRoot.transform);
            RecurseChildren(prefabRoot.transform);
            EditorGUILayout.EndScrollView();
        }
    }

    void DrawName(Transform transform)
    {
        GUIStyle style = EditorStyles.label;
        if (transform == Selection.activeGameObject.transform)
        {
            style = new GUIStyle(style);
            style.normal.textColor = Color.blue;
        }

        Rect rect = EditorGUILayout.GetControlRect();
        rect = EditorGUI.IndentedRect(rect);

        if (GUI.Button(rect, transform.name, style))
        {
            Selection.activeTransform = transform;
        }
    }

    void RecurseChildren(Transform parent)
    {
        EditorGUI.indentLevel++;
        foreach (Transform childTransform in parent)
        {
            DrawName(childTransform);

            if (childTransform.childCount > 0)
                RecurseChildren(childTransform);
        }
        EditorGUI.indentLevel--;
    }
}