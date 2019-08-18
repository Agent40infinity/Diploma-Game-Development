using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int beenVisited = 0;
    public Transform[] doors;

    public void Start()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i] = GetComponentInChildren<Transform>();
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            beenVisited = 1;
            GetComponent<GameManager>().curRoom = gameObject;
            Debug.Log(GetComponent<GameManager>().curRoom);
        }
    }
}
