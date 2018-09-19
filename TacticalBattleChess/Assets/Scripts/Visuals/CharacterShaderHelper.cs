using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShaderHelper : MonoBehaviour {
    private SpriteRenderer sr;
    public void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void SetTeamColor(Color color)
    {
        //current Workaround
        //Material temp = new Material(GetComponent<SpriteRenderer>().sharedMaterial);
        //temp.SetColor("_TeamColorOutline", color);
        //GetComponent<SpriteRenderer>().sharedMaterial = temp;
        //GetComponent<Renderer>().sharedMaterial.SetColor("_TeamColorOutline", color);
    }
    public void Mark()
    {
        sr.material.SetFloat("_MarkActive", 1f);
    }
    public void Range()
    {
        sr.material.SetFloat("_Outline", 0.03f);
        sr.material.SetFloat("_OutlineActive", 1f);
    }
    public void Hover()
    {
        sr.material.SetFloat("_Outline", 0.02f);
        sr.material.SetFloat("_OutlineActive", 1f);
    }
    public void UnMark()
    {
        sr.material.SetFloat("_MarkActive", 0f);
    }
    public void UnRange()
    {
        sr.material.SetFloat("_OutlineActive", 0f);
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
