using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Mapgenrator))]
public class MapgeneratorEditor : Editor {
    public override void OnInspectorGUI()
    {
        Mapgenrator mapgen = (Mapgenrator)target;

        if (DrawDefaultInspector())
        { if (mapgen.AutoUpdate)
            {
                mapgen.Generator();
            } 

        }

        if (GUILayout.Button("GeneratePerlin"))
        {
            mapgen.Generator();
        }
    }
}

