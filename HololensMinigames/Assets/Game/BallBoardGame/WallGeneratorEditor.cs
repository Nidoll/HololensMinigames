using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


#if UNITY_EDITOR
[CustomEditor(typeof(WallGenerator))]
public class WallGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WallGenerator wallG = (WallGenerator) target;

        DrawDefaultInspector();
        if(GUILayout.Button("Generate Vertical")){
            wallG.GenerateWallsVertical();
        }

        if(GUILayout.Button("Generate Horizontal")){
            wallG.GenerateWallsHorizontal();
        }

        if(GUILayout.Button("Generate Nodes")){
            wallG.GenerateNodes();
        }

        if(GUILayout.Button("Test Nodes")){
            wallG.TestNodes();
        }

        if(GUILayout.Button("Generate Maze")){
            wallG.TraverseNodes();
        }

        if(GUILayout.Button("Reset Walls")){
            wallG.ResetWalls();
        }
    }
}

#endif
