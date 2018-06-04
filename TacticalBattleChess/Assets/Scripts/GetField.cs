using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetField : MonoBehaviour
{
    private static  Field f;

    public static Field getField()
    {
        if(f== null)
        {
            f = new Field();
        }
        return f;
    }
    private void Start()
    {
        
    }
}