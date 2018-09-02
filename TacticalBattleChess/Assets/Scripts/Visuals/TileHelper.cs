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
        }

    }
    public void Mark()
    {
        if (tile.tileContent != null)
        {
            tile.tileContent.GetComponent<TileContentShaderHelper>().Mark();
            if (tile.tileContent.content != null)
            {
                tile.tileContent.content.GetComponent<ContentShaderHelper>().Mark();
            }
        }
        if (tile.GetCharacter() != null)
        {
            tile.GetCharacter().GetComponent<CharacterShaderHelper>().Mark();
        }
    }
    public void Unmark()
    {
        if (tile.tileContent != null)
        {
            tile.tileContent.GetComponent<TileContentShaderHelper>().UnMark();
            if (tile.tileContent.content != null)
            {
                tile.tileContent.content.GetComponent<ContentShaderHelper>().UnMark();
            }
        }
        if (tile.GetCharacter() != null)
        {
            tile.GetCharacter().GetComponent<CharacterShaderHelper>().UnMark();
        }
    }
    public void ResetAll()
    {
        if (tile.tileContent != null)
        {
            tile.tileContent.GetComponent<TileContentShaderHelper>().ResetAll();
            if (tile.tileContent.content != null)
            {
                tile.tileContent.content.GetComponent<ContentShaderHelper>().ResetAll();
            }
        }
        if (tile.GetCharacter() != null)
        {
            tile.GetCharacter().GetComponent<CharacterShaderHelper>().ResetAll();
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
    public void Select()
    {
        if (tile.GetCharacter() != null)
        {
            tile.GetCharacter().GetComponent<CharacterShaderHelper>().Range();
        }
    }
    public void UnHover()
    {
        if (tile.tileContent != null)
        {
            tile.tileContent.GetComponent<TileContentShaderHelper>().UnHover();
            if (tile.tileContent.content != null)
            {
                tile.tileContent.content.GetComponent<ContentShaderHelper>().UnHover();
            }
        }
        if (tile.GetCharacter() != null)
        {
            tile.GetCharacter().GetComponent<CharacterShaderHelper>().UnHover();
        }
    }
}
