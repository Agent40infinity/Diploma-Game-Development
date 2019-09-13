using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class MeshInspector : Editor
{
    private MeshStudy mesh;
    private Transform handleTransform;
    private Quaternion handleRotation;
    string triangleIdx;

    void OnSceneGUI()
    {
        EditMesh();
    }

    void EditMesh()
    {

    }

    private void ShowPoint(int index)
    {
        if (mesh.moveVertexPoint)
        {
            //draw dot
            //drag
        }
        else
        {
            //click
        }
    }


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        mesh = target as MeshStudy;

        // For testing Reset function
        if (mesh.isCloned)
        {
            if (GUILayout.Button("Test Edit"))
            {
                mesh.EditMesh();
            }
        }
    }


}
