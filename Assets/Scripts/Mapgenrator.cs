using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapgenrator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColourMap, drawMesh};
    public DrawMode drawMode;
    public int mapheight;
    public int mapwidth;
    public float noiseScale;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;


    public int Seed;
    public Vector2 Offfset;
    public bool AutoUpdate;

    public TerrainType[] regions;
    public void Generator()
    {
        float[,] NoiseMap = Noise.GenerateNoiseMap(mapwidth, mapheight,Seed, noiseScale, octaves, persistance, lacunarity, Offfset) ;
        Color[] colourMap = new Color[mapwidth * mapheight];
        for(int y = 0; y<mapheight; y++)
        {
            for (int x = 0; x<mapwidth; x++)
            {
                float currentHeight = NoiseMap[x, y];
                for(int i = 0; i<regions.Length; i++)
                {
                    if(currentHeight<= regions[i].height)
                    {
                        colourMap[y * mapwidth + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }

        Maprendere display = FindObjectOfType<Maprendere>();
        if(drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeighMap(NoiseMap));
        }
        else if( drawMode== DrawMode.ColourMap )
        {
            display.DrawTexture(TextureGenerator.TextureOfcolourMap(colourMap, mapwidth, mapheight));
        }
        else if(drawMode == DrawMode.drawMesh)
        {
            display.DrawMesh(MEshGenerator.GenerateTerrainMesh(NoiseMap), TextureGenerator.TextureOfcolourMap(colourMap, mapwidth, mapheight));
        }
      
    }

    private void OnValidate()
    {
        if (mapwidth < 1)
        {
            mapwidth = 1;
        }
        if(mapheight < 1)
        {
            mapheight = 1;
        }
        if(lacunarity < 1)
        {
            lacunarity = 1;
        }
        if(octaves < 0)
        {
            octaves = 0;
        }

    }

    
}
[System.Serializable]
public struct TerrainType
{
    public float height;
    public Color colour;
    public string name;
}
