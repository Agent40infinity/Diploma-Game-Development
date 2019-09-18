using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphs : MonoBehaviour
{
    #region Variables - Graph 
    [Range(10, 100)]
    public int resolution = 10;
    Transform[] points;
    public Transform pointPrefab;
    #endregion

    #region Variables - Mathematical Surfaces 
    public GraphFunctionName function;
    const float pi = Mathf.PI;
    public int type;

    static Vector3 SineFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.z = z;
        return p;
    }

    static Vector3 Sine2DFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(pi * (z + t));
        p.y *= 0.5f;
        p.z = z;
        return p;

    }

    static Vector3 MultiSineFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = Mathf.Sin(pi * (x + t));
        p.y += Mathf.Sin(2f * pi * (x + 2f * t)) / 2f;
        p.y *= 2f / 3f;
        p.z = z;
        return p;
    }

    static Vector3 MultiSine2DFunction(float x, float z, float t)
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

    static Vector3 Ripple(float x, float z, float t) //Used to determine the values for the position of each instantiated object to create a Ripple effect.
    {
        Vector3 p;
        float d = Mathf.Sqrt(x * x + z * z);
        p.x = x;
        p.y = Mathf.Sin(pi * (4f * d - t));
        p.y /= 1f + 10f * d;
        p.z = z;
        return p;
    }

    static Vector3 Cylinder(float u, float v, float t) //Used to determine the values for the position of each instantiated object to create a Cylinder like shape.
    {
        Vector3 p;
        float r = 0.8f + Mathf.Sin(pi * (6f * u + 2f * v + t)) * 0.2f;
        p.x = r * Mathf.Sin(pi * u);
        p.y = v;
        p.z = r * Mathf.Cos(pi * u);
        return p;
    }

    static Vector3 Sphere(float u, float v, float t) //Used to determine the values for the position of each instantiated object to create a Sphere like shape.
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

    static Vector3 Torus(float u, float v, float t) //Used to determine the values for the position of each instantiated object to create a torus like shape.
    {
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

    static GraphFunction[] functions =
    {
        SineFunction, Sine2DFunction, MultiSineFunction, MultiSine2DFunction, Ripple, Cylinder, Sphere, Torus, RandomObject
    };
    #endregion

    #region Creating a Graph
    public void Awake()
    {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one / 5f;
        points = new Transform[resolution * resolution];

        for (int i = 0; i < points.Length; i++)
        {
            Transform point = Instantiate(pointPrefab);
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    public void Update()
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
