using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHelper : MonoBehaviour {

    Tile tile;
	// Use this for initialization
	void Start () {
        tile = GetComponent<Tile>();      
	}

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



}
