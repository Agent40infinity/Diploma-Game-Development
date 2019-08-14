using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //General:
    public float xOffset;
    public float yOffset;

    //Reference:
    public GameObject player;
    public GameObject spawnRoom;
    public GameObject[] roomPresets;
    public GameObject curRoom;

    public void Start()
    {
        player = GameObject.Find("Player");
        spawnRoom = GameObject.Find("Spawn");
        for (int i = 0; i < roomPresets.Length; i++)
        {
            roomPresets[i] = Resources.Load<GameObject>("Prefabs/Towers/Preset " + i);
        }
    }

    public void Update()
    {


        if (curRoom.GetComponent<Room>().beenVisited != true)
        {
            for (int i = 0; i < curRoom.GetComponent<Room>().doors.Length; i++)
            {
                if (curRoom.GetComponent<Room>().doors[0])
                { xOffset = 0f; yOffset = -20f; }
                else if (curRoom.GetComponent<Room>().doors[1])
                { xOffset = -20f; yOffset = 0f; }
                else if (curRoom.GetComponent<Room>().doors[2])
                { xOffset = 0f; yOffset = 20f; }
                else if (curRoom.GetComponent<Room>().doors[3])
                { xOffset = 20f; yOffset = 0; }

                Instantiate(roomPresets[Random.Range(0, 4)], new Vector2(curRoom.transform.position.x + xOffset, curRoom.transform.position.y + yOffset), Quaternion.identity);
            }
            
        }
    }
}
