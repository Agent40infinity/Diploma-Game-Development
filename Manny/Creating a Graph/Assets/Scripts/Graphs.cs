 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphs : MonoBehaviour
{
    #region Variables - Graph 
    [Range(10, 100)]
    public int resolution = 10; //Determines how many points will be created.
    Transform[] points; //Creates a array of points created from the Prefab.
    public Transform pointPrefab; //References the points Prefab.
    #endregion

    #region Variables - Mathematical Surfaces 
    public GraphFunctionName function; //Creates a reference to the GraphFunctionName enum script.
    const float pi = Mathf.PI; //Creates a reference for PI to shorten the amount of times Mathf is needed.
    public int type;

    static Vector3 SineFunction(float x, float z, float t) //Used to determine the values for what the appearance of a Single SineFunction.
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.z = z;
        return p;
    }

    static Vector3 Sine2DFunction(float x, float z, float t) //Used to determine the values for what the appearance of a Single SineFunction from a 2D plane.
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(pi * (z + t));
        p.y *= 0.5f;
        p.z = z;
        return p;

    }

    static Vector3 MultiSineFunction(float x, float z, float t) //Used to determine the values for what the appearance of a MultiSineFunction.
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(2f * pi * (x + 2f * t)) / 2f;
        p.y *= 2f / 3f;
        p.z = z;
        return p;
    }

    static Vector3 MultiSine2DFunction(float x, float z, float t) //Used to determine the values for what appearance of the MultiSineFunction from a 2D plane.
    {
        Vector3 p;
        p.x = x;
        p.y = 4f * Mathf.Sin(pi * (x + z + t / 2f));
        p.y += Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(2f * pi * (z + 2f * t)) * 0.5f;
        p.y *= 1f / 5.5f;
        p.z = z;
        return p;

        
    }

    static Vector3 Ripple(float x, float z, float t) //Used to determine the values for the position of each instantiated object to create a Ripple effect. Animated Ripple Effect.
    {
        Vector3 p;
        float d = Mathf.Sqrt(x * x + z * z);
        p.x = x;
        p.y = Mathf.Sin(pi * (4f * d - t));
        p.y /= 1f + 10f * d;
        p.z = z;
        return p;
    }

    static Vector3 Cylinder(float u, float v, float t) //Used to determine the values for the position of each instantiated object to create a Cylinder like shape. Cylinder like appearance.
    {
        Vector3 p;
        float r = 0.8f + Mathf.Sin(pi * (6f * u + 2f * v + t)) * 0.2f;
        p.x = r * Mathf.Sin(pi * u);
        p.y = v;
        p.z = r * Mathf.Cos(pi * u);
        return p;
    }

    static Vector3 Sphere(float u, float v, float t) //Used to determine the values for the position of each instantiated object to create a Sphere like shape. Creates a spherical like shape.
    {
        Vector3 p;
        float r = 0.8f + Mathf.Sin(pi * (6f * u + t)) * 0.1f;
        r += Mathf.Sin(pi * (4f * v + t)) * 0.1f;
        float s = r * Mathf.Cos(pi * 0.5f * v);
        p.x = s * Mathf.Sin(pi * u);
        p.y = r * Mathf.Sin(pi * 0.5f * v);
        p.z = s * Mathf.Cos(pi * u);
        return p;
    }

    static Vector3 Torus(float u, float v, float t) //Used to determine the values for the position of each instantiated object to create a torus like shape. Any shape takes the form of a Torus.
    {
        Debug.Log(u + " " + v + " " + t);
        Vector3 p;
        float r1 = 0.65f + Mathf.Sin(pi * (6f * u + t)) * 0.1f;
        float r2 = 0.2f + Mathf.Sin(pi * (4f * v + t)) * 0.05f;
        float s = r2 * Mathf.Cos(pi *  v) + r1;
        p.x = s * Mathf.Sin(pi * u);
        p.y = r2 * Mathf.Sin(pi * v);
        p.z = s * Mathf.Cos(pi * u);
        return p;
    }

    static Vector3 RandomObject(float u, float v, float t) //Randomly Created Object.
    {
        Vector3 p;
        float r1 = Random.Range(0, 1f) + Mathf.Sin(pi * (Random.Range(0, 1f) * u + t)) * Random.Range(0, 1f); 
        float r2 = Random.Range(0, 1f) + Mathf.Sin(pi * (Random.Range(0, 1f) * v + t)) * Random.Range(0, 1f); 
        float s = r2 * Mathf.Cos(pi * v) + r1;
        p.x = s * Mathf.Sin(pi * u) * Random.Range(0, 2f);
        p.y = r2 * Mathf.Sin(pi * v) * Random.Range(0, 2f);
        p.z = s * Mathf.Cos(pi * u) * Random.Range(0, 2f);
        return p;
    }

    static GraphFunction[] functions = //Creates a reference to each type of shape being created to allow for it's generation.
    {
        SineFunction, Sine2DFunction, MultiSineFunction, MultiSine2DFunction, Ripple, Cylinder, Sphere, Torus, RandomObject
    };
    #endregion

    #region Creating a Graph
    public void Awake() //Sets up how many points will be created and where their original position will lie.
    {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one / 5f;
        points = new Transform[resolution * resolution];

        for (int i = 0; i < points.Length; i++) //Checks how many points there are in total and instantiates a new point based on the Length. It then saves the Transform of the point into an array for furture use.
        {
            Transform point = Instantiate(pointPrefab);
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    public void Update() //Used to update the position of each point based upon the resolution. It then checks and applies the pattern in which the points will move to (the graph functions: sphere, torus, cylinder, etc).
    {
        float t = Time.time;
        GraphFunction f = functions[(int)function];

        float step = 2f / resolution;
        for (int i = 0, z = 0; z < resolution; z++)
        {
            float v = (z + 0.5f) * step - 1f;
            for (int x = 0; x < resolution; x++, i++)
            {
                float u = (x + 0.5f) * step - 1f;
                points[i].localPosition = f(u, v, t);
            }
        }

        //CheckForShape();
    }
    #endregion

    //#region Extension Work
    //public void CheckForShape()
    //{
    //    switch (function)
    //    {
    //        case GraphFunctionName.Ripple:
    //            type = 4;
    //            break;
    //        case GraphFunctionName.Cylinder:
    //            type = 6;
    //            break;
    //        case GraphFunctionName.Sphere:
    //            type = 3;
    //            break;
    //        case GraphFunctionName.Torus:
    //            type = 5;
    //            break;
    //        default:
    //            type = 1;
    //            break;
    //    }
    //}
    //#endregion
}
