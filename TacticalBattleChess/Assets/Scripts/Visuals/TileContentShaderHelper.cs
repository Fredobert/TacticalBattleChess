using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileContentShaderHelper : MonoBehaviour {

    private SpriteRenderer sr;
    public void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Mark()
    {
        sr.material.SetFloat("_MarkActive", 1f);
    }
    public void Range()
    {
        sr.material.SetFloat("_RangeActive", 1f);
    }
    public void Hover()
    {
        sr.material.SetFloat("_OutlineActive", 1f);
    }
    public void UnMark()
    {
        sr.material.SetFloat("_MarkActive", 0f);
    }
    public void UnRange()
    {
        sr.material.SetFloat("_RangeActive", 0f);
    }
    public void UnHover()
    {
        sr.material.SetFloat("_OutlineActive", 0f);
    }
    public void ResetAll()
    {
        UnMark();
        UnRange();
    }
}
