using UnityEngine;
using UnityEditor;

/// <summary>
/// FitColliderToMeshes script by @BlackDragonBE
/// This helpder script allows you to automatically create a box collider shape that envelops all of the child meshes.
/// 
/// Usage:
/// 1. Select parent GameObject(s) with mesh children.
/// 2. Add a Box Collider.
/// 3. Select Tools > Fit collider to children in the top menu.
/// </summary>
public class FitColliderToMeshes : MonoBehaviour
{
    [MenuItem("Tools/Fit collider to children")]
    static void FitToChildren()
    {
        foreach (GameObject parentObject in Selection.gameObjects)
        {
            if (!(parentObject.GetComponent<Collider>() is BoxCollider))
            {
                continue;
            }

            bool hasBounds = false;
            Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

            for (int i = 0; i < parentObject.transform.childCount; ++i)
            {
                Renderer childRenderer = parentObject.transform.GetChild(i).GetComponent<Renderer>();
                if (childRenderer != null)
                {
                    if (hasBounds)
                    {
                        bounds.Encapsulate(childRenderer.bounds);
                    }
                    else
                    {
                        bounds = childRenderer.bounds;
                        hasBounds = true;
                    }
                }
            }

            BoxCollider collider = (BoxCollider)parentObject.GetComponent<Collider>();
            collider.center = bounds.center - parentObject.transform.position;
            collider.size = bounds.size;
        }
    }

}