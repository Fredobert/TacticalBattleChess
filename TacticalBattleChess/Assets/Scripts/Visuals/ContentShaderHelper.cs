using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentShaderHelper : MonoBehaviour {

    private SpriteRenderer sr;
    public void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Standard()
    {
        SetMark(false);
        SetOutline(false);
    }
    public void Select()
    {
        SetMark(true);
        SetOutline(true);
        SetOutlineColor(Color.white);
        SetOutlineSize(0.02f);
    }
    public void Hover()
    {
        SetMark(false);
        SetOutline(true);
        SetOutlineColor(new Color(158, 255, 242));
        SetOutlineSize(0.03f);
    }
    //Help methods
    private void SetMark(bool active)
    {
        sr.material.SetFloat("_MarkActive", (active) ? 1f : 0f);
    }
    private void SetOutline(bool active)
    {
        sr.material.SetFloat("_OutlineActive", (active) ? 1f : 0f);
    }
    private void SetOutlineColor(Color color)
    {
        sr.material.SetColor("_OutlineColor", color);
    }
    private void SetOutlineSize(float size)
    {
        sr.material.SetFloat("_Outline", size);
    }
}
