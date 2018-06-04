using UnityEditor;
using UnityEngine;
public class MyTools : MonoBehaviour
{
    // Add a menu item to create custom GameObjects.
    // Priority 1 ensures it is grouped with the other menu items of the same kind
    // and propagated to the hierarchy dropdown and hierarch context menus.
    [MenuItem("GameObject/MyTools/GridField", false, 10)]
    static void CreateCustomGameObject(MenuCommand menuCommand)
    {

        GameObject b = Resources.Load("field") as GameObject;
        b.name = "World";
        Field f = b.GetComponent<Field>();
        b =  Instantiate(b);
        b.name = "World";
        f.parent = b;
        f.Create();
    
        // Create a custom game object

        // Ensure it gets reparented if this was a context click (otherwise does nothing)
        GameObjectUtility.SetParentAndAlign(b, menuCommand.context as GameObject);
        // Register the creation in the undo system Check if all are registed
        Undo.RegisterCreatedObjectUndo(b, "Create " + b.name);
        Selection.activeObject = b;

    }
}