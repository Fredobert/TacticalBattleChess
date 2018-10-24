using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//prototype!
public class TileContentShaderHelper : MonoBehaviour {

    private SpriteRenderer sr;
    public Color FadeColor;
    public int prev = 0;
    public int curr = 0;
    public void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        FadeColor = new Color(158/255.0f, 1.0f, 238/255.0f);
    }
    //states
   public void Standard()
   {
        if (curr == 0)
        {
            return;
        }
        SetMark(false);
        SetRange(false);
        SetOutline(false);
        SetSelect(false);
        prev = 0;
        curr = 0;
   }
    public void Hover()
    {
        if (curr == 1)
        {
            return;
        }
        SetMark(false);
        SetOutline(false);
        SetRange(true);
        SetRangeColor(FadeColor);
        sr.material.SetFloat("_RangeTexFade", 0.584f);
        SetSelect(false);
        prev = curr;
        curr = 1;
    }
    public void Select()
    {
        if (curr == 2)
        {
            return;
        }
        SetMark(false);
        SetRange(true);
        SetRangeColor(Color.white);
        SetOutline(true);
        SetOutlineColor(Color.white);
        SetSelect(true);
        SetSelectColor(Color.white);
        prev = curr;
        curr = 2;
    }
    public void Range()
    {
        if (curr == 3)
        {
            return;
        }
        SetMark(false);
        SetOutline(false);
        SetRange(true);
        SetRangeColor(FadeColor);
        SetSelect(false);
        prev = curr;
        curr = 3;
    }
    public void Path()
    {
        if (curr == 4)
        {
            return;
        }
        SetMark(true);
        SetMarkColor(Color.black);
        SetOutline(true);
        SetOutlineColor(Color.black);
        SetSelect(false);
        SetRange(false);
        prev = curr;
        curr = 4;
    }
    public void Ability()
    {
        if (curr == 5)
        {
            return;
        }
        SetMark(false);
        //SetMarkColor(Color.red);
        SetOutline(true);
        SetOutlineColor(Color.red);
        SetSelect(false);
        SetRange(false);
        prev = curr;
        curr = 5;
    }


    public void AbilityIndicator(Color MarkColor, Color OutlineColor)
    {
        SetMark(true);
        SetMarkColor(MarkColor);
        SetOutline(true);
        SetOutlineColor(OutlineColor);
        SetSelect(false);
        SetRange(false);
        prev = curr;
        if (curr == 5 || curr == 7)
        {
            curr = 7;
        }
        else
        {
            curr = 6;
        }
       
    }

    //Temporary  solution
    public void Undo()
    {
 
        switch (prev)
        {
            case 0:
                Standard();
                break;
            case 1:
                //Hover();
                Standard();
                break;
            case 2:
                Select();
                break;
            case 3:
                Range();
                break;
            case 4:
                Path();
                break;
            case 5:
                Ability();
                break;
            case 6:
                //Ability();
                Standard();
                break;
            case 7:
                //Standard();
                Ability();
                break;
        }
    }
    //Help methods
    private void SetMark(bool active)
    {
        sr.material.SetFloat("_MarkActive", (active)?1f:0f);
    }
    private void SetRange(bool active)
    {
        sr.material.SetFloat("_RangeActive", (active) ? 1f : 0f);
    }
    private void SetOutline(bool active)
    {
        sr.material.SetFloat("_OutlineActive", (active) ? 1f : 0f);
    }
    private void SetSelect(bool active)
    {
        sr.material.SetFloat("_SelectActive", (active) ? 1f : 0f);
    }
    private void SetMarkColor(Color color)
    {
        sr.material.SetColor("_MarkColor", color);
    }
    private void SetRangeColor(Color color)
    {
        sr.material.SetColor("_RangeColor", color);
    }
    private void SetOutlineColor(Color color)
    {
        sr.material.SetColor("_OutlineColor", color);
    }
    private void SetSelectColor(Color color)
    {
        sr.material.SetColor("_SelectColor", color);
    }
}


