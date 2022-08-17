using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MEshGenerator
{

    public static MEshData GenerateTerrainMesh(float[,] heightMap)
    {
        int Width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (Width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        MEshData meshData = new MEshData(Width, height);
        int vertexIndex = 0;

        for (int y = 0; y<height; y++)
        {
            for (int x = 0; x< Width; x++)
            {
                meshData.vertices[vertexIndex] = new Vector3(topLeftX+x,heightMap[x,y], topLeftZ-y);
                meshData.uvs[vertexIndex] = new Vector2(x / (float)Width, y / (float)height);
                if(x<Width -1 && y < height - 1)
                {
                    meshData.AddTriangles(vertexIndex, vertexIndex + Width + 1, vertexIndex + Width);
                    meshData.AddTriangles(vertexIndex + Width + 1, vertexIndex, vertexIndex + 1);
                }
                vertexIndex++;
            }
        }
        return meshData;
    }
}

[System.Serializable]
public class MEshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;
    int trianleIndex;

    public MEshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshWidth* meshHeight];
        uvs = new Vector2[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
        
    }

    public void AddTriangles(int a, int b , int c)
    {
        triangles[trianleIndex] = a;
        triangles[trianleIndex + 1] = b;
        triangles[trianleIndex+2] = c;
        trianleIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}