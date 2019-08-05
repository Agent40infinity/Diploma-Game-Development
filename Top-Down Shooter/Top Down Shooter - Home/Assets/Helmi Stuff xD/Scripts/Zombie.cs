using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float stoppingDistance;

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }
        else
        {
            speed = 0;  
        }

    }
}
