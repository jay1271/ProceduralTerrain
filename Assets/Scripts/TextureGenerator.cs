using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator
{
    public static Texture2D TextureOfcolourMap(Color[] colourMap, int Width, int Height)
    {
        Texture2D texture = new Texture2D(Width, Height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colourMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeighMap(float[,] heightMap)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        Color[] colourmap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colourmap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);


            }
        }
        return TextureOfcolourMap(colourmap, width, height);

    }
}

