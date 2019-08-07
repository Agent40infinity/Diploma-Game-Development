using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    public Transform attached;
    // Update is called once per frame
    void Update()
    {
        transform.position = attached.position;
    }
}
