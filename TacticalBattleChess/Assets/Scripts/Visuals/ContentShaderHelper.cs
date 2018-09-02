using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShaderHelper : MonoBehaviour {

    private SpriteRenderer sr;
    public void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Mark()
    {
        sr.material.SetFloat("_MarkActive", 1f);
    }
    public void Hover()
    {
        sr.material.SetFloat("_OutlineActive", 1f);
    }
    public void UnMark()
    {
        sr.material.SetFloat("_MarkActive", 0f);
    }
    public void UnHover()
    {
        sr.material.SetFloat("_OutlineActive", 0f);
    }
    public void ResetAll()
    {
        UnMark(); 
    }
}
