using UnityEditor;
using UnityEngine;

public class SpriteLoadHelper : AssetPostprocessor
{
    //For every imported sprite, alpha = 0 set the blue value to the distance to next pixel where a != 0
    //needed for Outlines in Combined Shader

    //Maybe change to float 
    public float r = 0.5f;
    public float g = 0.5f;
    public float b = 0.01f;
    public int depth = 16;

    void OnPostprocessTexture(Texture2D texture)
    {
        Debug.Log("SpriteLoadHelper: Imported a new Sprite");
        Color z;
        for (int k = 0; k < depth; k++)
        {
            for (int i = 0; i < texture.width; i++)
            {
                for (int j = 0; j < texture.height; j++)
                {
                    z = texture.GetPixel(i, j);
                    if (z.a == 0 && (z.r != r || z.g != g))
                    {
                        SetPixel(texture, i, j, k);
                    }
                }
            }
            texture.Apply();
        }
    }

    private void SetPixel(Texture2D texture, int x, int y, int d)
    {
        float min = 100.0f; 
        int[] z = { 0, 1, 1, 0, 0, -1, -1, 0 };
        for (int i = 0; i < z.Length; i += 2)
        {
            float k = CheckPixel(texture, x + z[i], y + z[i + 1]);
            if (k != -1 && k <= d * 10)
            {
                min = Mathf.Min(k, min);
            }
        }
        if (min + b < 1.0)
        {
            texture.SetPixel(x, y, new Color(r, g, (min + b),0));
        }
    }
    private float CheckPixel(Texture2D texture, int x, int y)
    {
        if (x < texture.width && x >= 0 && y < texture.height && y >= 0)
        {
            if (texture.GetPixel(x, y).a != 0)
            {
                return 0;
            }
            else if (Mathf.Abs(texture.GetPixel(x, y).r - r) < depth/10 && Mathf.Abs(texture.GetPixel(x, y).g - g) < depth / 10)
            {
                return (texture.GetPixel(x,y)).b;
            }
        }
        return -1;
    }



}
