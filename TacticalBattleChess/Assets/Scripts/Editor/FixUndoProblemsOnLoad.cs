using UnityEngine;
using UnityEditor;


[InitializeOnLoad]
public class FixUndoProblemsOnLoad{

    static FixUndoProblemsOnLoad()
    {
        Debug.Log("Up and running");
    }
}
