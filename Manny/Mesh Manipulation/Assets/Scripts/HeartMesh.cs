using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeartMesh : MonoBehaviour
{
    Mesh oMesh;
    Mesh cMesh;
    MeshFilter oFilter;

    [HideInInspector]
    public int targetIndex;

    [HideInInspector]
    public Vector3 targetVertex;

    [HideInInspector]
    public Vector3[] oVertices;

    [HideInInspector]
    public Vector3[] mVertices;

    [HideInInspector]
    public Vector3[] normals;

    [HideInInspector]
    public bool isMeshReady = false;
    public bool isEditMode = true;
    public bool showTransformHandle = true;
    public List<int> selectedIndices = new List<int>();
    public float pickSize = 0.01f;

    public float radiusofeffect = 0.3f;
    public float pullvalue = 0.3f; 
    public float duration = 1.2f; 
    int currentIndex = 0; 
    bool isAnimate = false;
    float starttime = 0f;
    float runtime = 0f;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        oFilter = GetComponent<MeshFilter>();
        isMeshReady = false;
        currentIndex = 0;

        if (isEditMode)
        {
            oMesh = oFilter.sharedMesh;
            cMesh = new Mesh();
            cMesh.name = "clone";
            cMesh.vertices = oMesh.vertices;
            cMesh.triangles = oMesh.triangles;
            cMesh.normals = oMesh.normals;
            oFilter.mesh = cMesh;

            oVertices = cMesh.vertices;
            normals = cMesh.normals;
            Debug.Log("Init & Cloned");
        }
        else
        {
            oMesh = oFilter.mesh;
            oVertices = oMesh.vertices;
            normals = oMesh.normals;
            mVertices = new Vector3[oVertices.Length];
            for (int i = 0; i < oVertices.Length; i++)
            {
                mVertices[i] = oVertices[i];
            }
            StartDisplacement();
        }

    }

    public void StartDisplacement()
    {
        targetVertex = oVertices[selectedIndices[currentIndex]]; 
        starttime = Time.time;
        isAnimate = true;
    }

    public void FixedUpdate()
    {
        if (!isAnimate)
        {
            return;
        }

        runtime = Time.time - starttime; 

        if (runtime < duration) 
        {
            Vector3 targetVertexPos = oFilter.transform.InverseTransformPoint(targetVertex);
            DisplaceVertices(targetVertexPos, pullvalue, radiusofeffect);
        }
        else
        {
            currentIndex++;
            if (currentIndex < selectedIndices.Count)
            {
                StartDisplacement();
            }
            else 
            {
                oMesh = GetComponent<MeshFilter>().mesh;
                isAnimate = false;
                isMeshReady = true;
            }
        }
    }

    void DisplaceVertices(Vector3 targetVertexPos, float force, float radius)
    {
        Vector3 currentVertexPos = Vector3.zero;
        float sqrRadius = radius * radius; 

        for (int i = 0; i < mVertices.Length; i++) 
        {
            currentVertexPos = mVertices[i];
            float sqrMagnitute = (currentVertexPos - targetVertexPos).sqrMagnitude; 
            if (sqrMagnitute > sqrRadius)
            {
                continue; 
            }
            float distance = Mathf.Sqrt(sqrMagnitute); 
            float falloff = GaussFalloff(distance, radius);
            Vector3 translate = (currentVertexPos * force) * falloff; 
            translate.z = 0f;
            Quaternion rotation = Quaternion.Euler(translate);
            Matrix4x4 m = Matrix4x4.TRS(translate, rotation, Vector3.one);
            mVertices[i] = m.MultiplyPoint3x4(currentVertexPos);
        }
        oMesh.vertices = mVertices; 
        oMesh.RecalculateNormals();
    }

    public void ClearAllData()
    {
        selectedIndices = new List<int>();
        targetIndex = 0;
        targetVertex = Vector3.zero;
    }

    public Mesh SaveMesh()
    {
        Mesh nMesh = new Mesh();
        nMesh.name = "HeartMesh";
        nMesh.vertices = oMesh.vertices;
        nMesh.triangles = oMesh.triangles;
        nMesh.normals = oMesh.normals;

        return nMesh;
    }

    #region HELPER FUNCTIONS

    static float LinearFalloff(float dist, float inRadius)
    {
        return Mathf.Clamp01(0.5f + (dist / inRadius) * 0.5f);
    }

    static float GaussFalloff(float dist, float inRadius)
    {
        return Mathf.Clamp01(Mathf.Pow(360, -Mathf.Pow(dist / inRadius, 2.5f) - 0.01f));
    }

    static float NeedleFalloff(float dist, float inRadius)
    {
        return -(dist * dist) / (inRadius * inRadius) + 1.0f;
    }

    #endregion
}
