using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(NetVisualizer))]
public class NetEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Save"))
        {

            (target as NetVisualizer).PrepareMesh();
            AssetDatabase.CreateAsset((target as NetVisualizer).GetComponent<MeshFilter>().sharedMesh, "Assets/mesh.asset");
        }
    }
}
