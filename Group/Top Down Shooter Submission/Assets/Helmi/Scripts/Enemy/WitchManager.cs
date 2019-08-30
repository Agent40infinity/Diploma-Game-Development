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
        
        // When the player is still in the menu room
        if (room == initialRoom)
        { }
        else
        {
            witchesInTheRoom = roomWitch.witches;

            #region Trash
            #region First Try
            //foreach (GameObject witch in witchesInTheRoom)
            //{
            //    SpriteRenderer witchRenderer = witch.GetComponent<SpriteRenderer>();
            //    EnemySpawner witchSpawner = witch.GetComponent<EnemySpawner>();
            //    witchRenderer.enabled = true;
            //    witchSpawner.enabled = true;
            //}
            #endregion

            #region Second Try
            //if(witchesInTheRoom[0] != null)
            //{
            //    SpriteRenderer witchRenderer = witchesInTheRoom[0].GetComponent<SpriteRenderer>();
            //    EnemySpawner witchSpawner = witchesInTheRoom[0].GetComponent<EnemySpawner>();
            //    witchRenderer.enabled = true;
            //    witchSpawner.enabled = true;
            //}

            //if (witchesInTheRoom[1] != null)
            //{
            //    SpriteRenderer witchRenderer = witchesInTheRoom[1].GetComponent<SpriteRenderer>();
            //    EnemySpawner witchSpawner = witchesInTheRoom[1].GetComponent<EnemySpawner>();
            //    witchRenderer.enabled = true;
            //    witchSpawner.enabled = true;
            //}
            #endregion
            #endregion

            for (int i = 0; i < witchesInTheRoom.Length; i++)
            {
                // When the witches exists
                if(witchesInTheRoom[i] != null)
                {
                    SpriteRenderer witchRenderer = witchesInTheRoom[i].GetComponent<SpriteRenderer>();
                    EnemySpawner witchSpawner = witchesInTheRoom[i].GetComponent<EnemySpawner>();
                    // Set the sprite renderer of every witch in the room to true
                    witchRenderer.enabled = true;
                    // Set the enemy spawner of every witch in the room to true
                    witchSpawner.enabled = true;
                }

                // When none
                else
                {
                    witchesInTheRoom[i] = null;
                }
            }
        }

        yield return new WaitForEndOfFrame();
    }
}
