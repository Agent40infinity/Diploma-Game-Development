using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchManager : MonoBehaviour
{
    public GameObject currentRoom;
    private GameObject initialRoom;
    public RoomWitches roomWitch;
    public GameObject[] witchesInTheRoom;
    public GameObject[] rooms;
    //public int witchRoomNum;
    //public GameObject[] witches;
    public GameManager gm;
    public MainMenu main;

    private EnemySpawner[] enemySpawners;

    private void Start()
    {
        gm = GetComponent<GameManager>();
        rooms = GameObject.FindGameObjectsWithTag("Room");
        initialRoom = gm.curRoom;
    }

    void Update()
    {
        currentRoom = gm.curRoom;
        rooms = GameObject.FindGameObjectsWithTag("Room");
        StartCoroutine(WhichRoom(currentRoom));

        #region Trash
        #region First Try
        //witches = GameObject.FindGameObjectsWithTag("Witch");

        //if (!main.startPressed)
        //{
        //    foreach (GameObject witch in witches)
        //    {
        //        EnemySpawner enemySpawner = witch.GetComponent<EnemySpawner>();
        //        enemySpawner.zombieSpawned = false;
        //    }
        //}

        //if (main.startPressed)
        //{

        //    foreach (GameObject witch in witches)
        //    {
        //        EnemySpawner enemySpawner = witch.GetComponent<EnemySpawner>();
        //        enemySpawner.zombieSpawned = true;
        //    }

        //}
        #endregion

        #region Second Try
        //GameObject preset1 = GameObject.Find("Preset 1(Clone)");
        //GameObject preset2 = GameObject.Find("Preset 2(Clone)");
        //GameObject preset3 = GameObject.Find("Preset 3(Clone)");
        //GameObject preset4 = GameObject.Find("Preset 4(Clone)");

        //if (currentRoom == preset1)
        //{
        //    roomWitch = preset1.GetComponent<WitchInTheHouse>();

        //    foreach (GameObject witch in roomWitch.witches)
        //    {
        //        witch.SetActive(true);
        //        witch.GetComponent<EnemySpawner>().zombieSpawned = true;
        //    }
        //}

        //if (currentRoom == preset2)
        //{
        //    roomWitch = preset2.GetComponent<WitchInTheHouse>();

        //    foreach (GameObject witch in roomWitch.witches)
        //    {
        //        witch.SetActive(true);
        //        witch.GetComponent<EnemySpawner>().zombieSpawned = true;
        //    }
        //}

        //if (currentRoom == preset3)
        //{
        //    roomWitch = preset3.GetComponent<WitchInTheHouse>();

        //    foreach (GameObject witch in roomWitch.witches)
        //    {
        //        witch.SetActive(true);
        //        witch.GetComponent<EnemySpawner>().zombieSpawned = true;
        //    }
        //}

        //if (currentRoom == preset4)
        //{
        //    roomWitch = preset4.GetComponent<WitchInTheHouse>();

        //    foreach (GameObject witch in roomWitch.witches)
        //    {
        //        witch.SetActive(true);
        //        witch.GetComponent<EnemySpawner>().zombieSpawned = true;
        //    }
        //}
        #endregion
        #endregion
    }

    IEnumerator WhichRoom(GameObject room)
    {
        roomWitch = room.GetComponent<RoomWitches>();

        if (room == initialRoom)
        { }
        else
        {
            witchesInTheRoom = roomWitch.witches;
            foreach (GameObject witch in witchesInTheRoom)
            {
                SpriteRenderer witchRenderer = witch.GetComponent<SpriteRenderer>();
                EnemySpawner witchSpawner = witch.GetComponent<EnemySpawner>();
                witchRenderer.enabled = true;
                witchSpawner.enabled = true;
            }
        }

        yield return new WaitForEndOfFrame();
    }
}
