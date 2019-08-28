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
                if (curRoom.GetComponent<Room>().doors[0].GetComponent<Door>().hasChecked == false)
                { xOffset = 0f; yOffset = -18f; }
                else if (curRoom.GetComponent<Room>().doors[1].GetComponent<Door>().hasChecked == false)
                { xOffset = -32f; yOffset = 0f; }
                else if (curRoom.GetComponent<Room>().doors[2].GetComponent<Door>().hasChecked == false)
                { xOffset = 0f; yOffset = 18f; }
                else if (curRoom.GetComponent<Room>().doors[3].GetComponent<Door>().hasChecked == false)
                { xOffset = 32f; yOffset = 0; }

                if (curRoom.GetComponent<Room>().doors[i].GetComponent<Door>().hasChecked == false)
                {
                    random = Random.Range(0, 4);
                    Instantiate(roomPresets[random], new Vector2(curRoom.transform.position.x + xOffset, curRoom.transform.position.y + yOffset), Quaternion.identity);
                    curRoom.GetComponent<Room>().doors[i].GetComponent<Door>().hasChecked = true;
                    Debug.Log(random);
                }
            }
            curRoom.GetComponent<Room>().beenVisited = 2;
        }
    }
}
