using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapheight,int seed, float scale, int Octaves, float Persistance, float Lacunarity, Vector2 offset)
    {
        float[,] noiseMap = new float[mapWidth, mapheight];
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[Octaves];
        for (int i = 0; i<Octaves ; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x ;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }
        if (scale <= 0)
        {
            scale = 0.0001f;
        }
        float maxNoiseheight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapheight / 2f;
        for (int y = 0; y < mapheight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseheight = 0;

                for (int i = 0; i < Octaves; i++)
                {
                    float sampleX = (x-halfWidth )/ scale*frequency + octaveOffsets[i].x;
                    float sampleY = (y-halfHeight)  / scale*frequency + octaveOffsets[i].y; 

                    
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY)*2-1;
                    noiseheight += perlinValue * amplitude;

                    amplitude *= Persistance;
                    frequency *= Lacunarity;
                }
                if (noiseheight > maxNoiseheight)
                {
                    maxNoiseheight = noiseheight;
                }
                else if(noiseheight < minNoiseHeight)
                {
                    minNoiseHeight = noiseheight;
                }
                noiseMap[x, y] = noiseheight;
            }
        
        }

        for (int y = 0; y < mapheight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseheight, noiseMap[x, y]);
            }
        }
                return  noiseMap;
    }
}