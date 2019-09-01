using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int beenVisited = 0;
    public GameObject[] doors = new GameObject[4];
    //public Transform[] doorPos = new Transform[4];
    public GameObject curRoom = null;

    public void Start()
    {
        //for (int i = 0; i < doors.Length; i++)
        //{
        //    doorPos[i] = GetComponentInChildren<Transform>();
        //}
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (beenVisited < 2)
            {
                beenVisited = 1;
            }
            GameObject.Find("GameManager").GetComponent<GameManager>().curRoom = gameObject;
        }
    }
}
