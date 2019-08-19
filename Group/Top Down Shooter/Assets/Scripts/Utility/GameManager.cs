using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //General:
    public float xOffset;
    public float yOffset;
    public int random;

    //Reference:
    public GameObject player;
    public GameObject spawnRoom;
    public GameObject[] roomPresets;
    public GameObject curRoom;

    public void Start()
    {
        player = GameObject.Find("Player");
        spawnRoom = GameObject.Find("Spawn");
        curRoom = spawnRoom;
        for (int i = 0; i < roomPresets.Length; i++)
        {
            roomPresets[i] = Resources.Load<GameObject>("Prefabs/Presets/Preset " + (i + 1));
            Debug.Log(roomPresets[i]);
        }
    }

    public void Update()
    {
        if (curRoom.GetComponent<Room>().beenVisited == 1)
        {
            for (int i = 0; i < curRoom.GetComponent<Room>().doors.Length; i++)
            {
                if (curRoom.GetComponent<Room>().doors[0])
                { xOffset = 0f; yOffset = -16.5f; }
                else if (curRoom.GetComponent<Room>().doors[1])
                { xOffset = -16.5f; yOffset = 0f; }
                else if (curRoom.GetComponent<Room>().doors[2])
                { xOffset = 0f; yOffset = 16.5f; }
                else if (curRoom.GetComponent<Room>().doors[3])
                { xOffset = 16.5f; yOffset = 0; }

                random = Random.Range(-1, 4);
                Instantiate(roomPresets[random], new Vector2(curRoom.transform.position.x + xOffset, curRoom.transform.position.y + yOffset), Quaternion.identity);
                Debug.Log(random);
            }
            curRoom.GetComponent<Room>().beenVisited = 2;
        }
    }
}
