using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(HeartMesh))]
public class HeartMeshInspector : Editor
{
    private HeartMesh mesh;
    private Transform handleTransform;
    private Quaternion handleRotation;

    void OnSceneGUI()
    {
        mesh = target as HeartMesh;
        handleTransform = mesh.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

        // ShowHandles on Mesh
        if (mesh.isEditMode)
        {
            if (mesh.oVertices == null || mesh.normals.Length == 0)
            {
                mesh.Init();
            }
            for (int i = 0; i < mesh.oVertices.Length; i++)
            {
                ShowHandle(i);
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

        // unselected vertex
        if (!mesh.selectedIndices.Contains(index))
        {
            Handles.color = Color.blue;
            if (Handles.Button(point, handleRotation, mesh.pickSize, mesh.pickSize, Handles.DotHandleCap)) 
            {
                mesh.selectedIndices.Add(index); 
            }
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        mesh = target as HeartMesh;

        if (mesh.isEditMode || mesh.isMeshReady)
        {
            if (GUILayout.Button("Show Normals"))
            {
                Vector3[] verts = mesh.mVertices.Length == 0 ? mesh.oVertices : mesh.mVertices;
                Vector3[] normals = mesh.normals; Debug.Log(normals.Length);
                for (int i = 0; i < verts.Length; i++)
                {
                    Debug.DrawLine(handleTransform.TransformPoint(verts[i]), handleTransform.TransformPoint(normals[i]), Color.green, 4.0f, true);
                }
            }
        }

        if (GUILayout.Button("Clear Selected Vertices"))
        {
            mesh.ClearAllData();
        }

        if (!mesh.isEditMode && mesh.isMeshReady)
        {
            string path = "Assets/Prefabs/CustomHeart.prefab"; 

            if (GUILayout.Button("Save Mesh"))
            {
                mesh.isMeshReady = false;
                Object pfObj = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)); 
                Object pfRef = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
                GameObject gameObj = (GameObject)PrefabUtility.InstantiatePrefab(pfObj);
                Mesh pfMesh = (Mesh)AssetDatabase.LoadAssetAtPath(path, typeof(Mesh)); 
                if (!pfMesh)
                {
                    pfMesh = new Mesh();
                }
                else
                {
                    pfMesh.Clear();
                }
                pfMesh = mesh.SaveMesh(); 
                AssetDatabase.AddObjectToAsset(pfMesh, path);

                gameObj.GetComponentInChildren<MeshFilter>().mesh = pfMesh; 
                PrefabUtility.ReplacePrefab(gameObj, pfRef, ReplacePrefabOptions.Default);
                Object.DestroyImmediate(gameObj); 
            }
        }
    }


}
