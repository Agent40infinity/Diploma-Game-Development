using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(CustomHeart))]
public class CustomHeartInspector : Editor
{
    private CustomHeart mesh;
    private Transform handleTransform;
    private Quaternion handleRotation;

    private int selectedIndex = -1;
    private int assignIndex;

    void OnSceneGUI()
    {
        mesh = target as CustomHeart;
        handleTransform = mesh.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

        if (mesh.oVertices == null) 
        {
            mesh.Init();
        }

        // Add Points
        if (mesh.editType.ToString() == "AddIndices")
        {
            for (int i = 0; i < mesh.oVertices.Length; i++)
            {
                ShowHandle(i);
            }
        } // Remove Points
        else if (mesh.editType.ToString() == "RemoveIndices")
        {
            for (int i = 0; i < mesh.oVertices.Length; i++)
            {
                ShowSelected(i);
            }
        }

        // Show/ Hide Transform Tool
        if (mesh.showTransformHandle)
        {
            Tools.current = Tool.Move;
        } 
        else
        {
            Tools.current = Tool.None;
        }
    }

    void ShowHandle(int index)
    {
        Vector3 point = handleTransform.TransformPoint(mesh.oVertices[index]);
        
        // Unselected vertex
        if (!mesh.selectedIndices.Contains(index))
        {
            Handles.color = Color.blue;
            if (Handles.Button(point, handleRotation, mesh.pickSize, mesh.pickSize, Handles.DotHandleCap))
            {
                selectedIndex = index;
            }
            
            if (selectedIndex == index)
            {
                if (assignIndex != index)
                {
                    assignIndex = mesh.targetIndex = index;
                    mesh.selectedIndices.Add(index);
                }
            }
        }
    }

    void ShowSelected(int index)
    {
        Vector3 point = handleTransform.TransformPoint(mesh.oVertices[index]);

        // Unselected vertex
        if (mesh.selectedIndices.Contains(index))
        {
            Handles.color = Color.red;
            if (Handles.Button(point, handleRotation, mesh.pickSize, mesh.pickSize, Handles.DotHandleCap))
            {
                selectedIndex = index;
            }
            
            if (selectedIndex == index)
            {
                if (assignIndex != index)
                {
                    assignIndex = mesh.targetIndex = index;
                    mesh.selectedIndices.Remove(index);
                }
            }
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        mesh = target as CustomHeart;

        if (GUILayout.Button("Clear Selected Vertices"))
        {
            Debug.Log("Editor Reset");
            assignIndex = 0;
            selectedIndex = -1;
            mesh.ClearAllData();
        }

        if (mesh.editType.ToString() != "None" && !Application.isPlaying)
        {
            if (GUILayout.Button("Show Normals"))
            {
                mesh.ShowNormals();
            }
        }
    }
}
