using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderHelper : MonoBehaviour {

    public Color color;
    public Color teamColor;

    private SpriteRenderer sr;
    public void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void DrawTeamOutline()
    {
        Material mat = sr.material;
        mat.SetFloat("_RangeActive", 1.0f);
    }
    // Rebuild Sprite


}
