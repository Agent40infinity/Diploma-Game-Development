using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool beenVisited = false;
    public GameObject[] doors;

    public void Start()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            //doors[i] = 
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            beenVisited = true;
        }
    }
}
