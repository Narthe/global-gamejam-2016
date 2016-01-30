using Assets.Scripts.Components;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathControllerComponent))]
public class PathBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PathControllerComponent myScript = (PathControllerComponent)target;
        if (GUILayout.Button("refreshPath"))
        {
            myScript.RefreshPath();
        }
    }
}