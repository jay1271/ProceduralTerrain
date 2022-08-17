using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maprendere : MonoBehaviour
{
    public Renderer TexRenderer;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public void DrawTexture(Texture2D texture)
    {
        
        TexRenderer.sharedMaterial.mainTexture = texture;
        TexRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);

    }

    public void DrawMesh(MEshData mEshData, Texture2D texture)
    {
        meshFilter.sharedMesh = mEshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}
