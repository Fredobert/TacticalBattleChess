using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileHelper : MonoBehaviour {

    public Tile tile;
    public TextMeshPro textmp;

    public int damage;
    public bool textShown;

    public bool Dangerous = false;

    public void Range()
    {
        if (tile.tileContent != null)
        {
            tile.tileContent.GetComponent<TileContentShaderHelper>().Range();
            if (tile.tileContent.content != null)
            {
                tile.tileContent.content.GetComponent<ContentShaderHelper>().Standard();
            }
        }
        if (tile.GetCharacter() != null)
        {
            tile.GetCharacter().GetComponent<CharacterShaderHelper>().Standard();
        }
    }
    public void Select()
    {
        if (tile.tileContent != null)
        {
            tile.tileContent.GetComponent<TileContentShaderHelper>().Select();
            if (tile.tileContent.content != null)
            {
                tile.tileContent.content.GetComponent<ContentShaderHelper>().Standard();
            }
        }
        if (tile.GetCharacter() != null)
        {
            tile.GetCharacter().GetComponent<CharacterShaderHelper>().Select();
        }
    }
   
    public void Standard()
    {
        Dangerous = false;
        HideText();
        if (tile.tileContent != null)
        {
            tile.tileContent.GetComponent<TileContentShaderHelper>().Standard();
            if (tile.tileContent.content != null)
            {
                tile.tileContent.content.GetComponent<ContentShaderHelper>().Standard();
            }
        }
        if (tile.GetCharacter() != null)
        {
            tile.GetCharacter().GetComponent<CharacterShaderHelper>().Standard();
        }
    }

    public void Hover()
    {
        if (tile.tileContent != null)
        {
            tile.tileContent.GetComponent<TileContentShaderHelper>().Hover();
            if (tile.tileContent.content != null)
            {
                tile.tileContent.content.GetComponent<ContentShaderHelper>().Hover();
            }
        }
        if (tile.GetCharacter() != null)
        {
            tile.GetCharacter().GetComponent<CharacterShaderHelper>().Hover();
        }
    }

    public void Path()
    {
        if (tile.tileContent != null)
        {
            tile.tileContent.GetComponent<TileContentShaderHelper>().Path();
            if (tile.tileContent.content != null)
            {
                tile.tileContent.content.GetComponent<ContentShaderHelper>().Standard();
            }
        }
        if (tile.GetCharacter() != null)
        {
            tile.GetCharacter().GetComponent<CharacterShaderHelper>().Select();
        }
    }
    public void Ability()
    {
        if (tile.tileContent != null)
        {
            tile.tileContent.GetComponent<TileContentShaderHelper>().Ability();
            if (tile.tileContent.content != null)
            {
                tile.tileContent.content.GetComponent<ContentShaderHelper>().Standard();
            }
        }
        if (tile.GetCharacter() != null)
        {
            tile.GetCharacter().GetComponent<CharacterShaderHelper>().Select();
        }
    }

    public void Undo()
    {
        Dangerous = false;
        HideText();
        if (tile.tileContent != null)
        {
            tile.tileContent.GetComponent<TileContentShaderHelper>().Undo();
            if (tile.tileContent.content != null)
            {
                tile.tileContent.content.GetComponent<ContentShaderHelper>().Standard();
            }
        }
        if (tile.GetCharacter() != null)
        {
            tile.GetCharacter().GetComponent<CharacterShaderHelper>().Standard();
        }
    }


    public void AbilityDefault(int damage, Color color)
    {
        tile.tileContent.GetComponent<TileContentShaderHelper>().AbilityIndicator(color, color);
        SetText(damage, color);
        Dangerous = true;
    }

    public void AbilityTraverse(Color color)
    {
        tile.tileContent.GetComponent<TileContentShaderHelper>().AbilityIndicator(color, color);
        Dangerous = true;
    }

    public void SetText(int damage, Color color)
    {
        textmp.color = color;
        SetText(damage);
    }
    public void SetText(int damageText)
    {
        damage += damageText;
        textShown = true;
        textmp.text = (damage < 0)?"+"+(damage*-1):damage+ "";
    }
    public void HideText()
    {
        damage = 0;
        textShown = false;
        textmp.text = "";
    }
}
