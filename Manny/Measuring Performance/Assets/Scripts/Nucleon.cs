using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Nucleon : MonoBehaviour
{
    //General:
    public float attractionForce;

    //References:
    Rigidbody body;

    public void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        body.AddForce(transform.localPosition * -attractionForce);
    }
}
