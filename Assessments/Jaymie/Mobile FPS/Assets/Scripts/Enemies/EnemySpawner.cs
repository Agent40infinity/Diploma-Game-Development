using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*--------------------------------------------------------------------------
 * Script Created by: Aiden Nathan.
 *------------------------------------------------------------------------*/

public class EnemySpawner : MonoBehaviour
{
    #region Variables
    //General:
    public bool enemiesSpawning = true; //Checks whether or not the enemies are spawning

    //References:
    public GameObject enemyParent; //Reference for the enemy prefab to allow for the enemies to spawn
    public RoomData[] roomParents; //Array of RoomData's to store all spawner locations based upon the room
    public List<Transform> activeSpawners = new List<Transform>(); //Creates a list to reference the active spawners 

    public struct RoomData //RoomData is used to store the room parent information and the spawnpoints attached to each room
    {
        public GameObject room; //Used to reference the room parent gameObject
        public Transform[] spawnPoints; //Array of Transforms to reference each spawnpoint within the room
    }
    #endregion

    #region General
    public void Start()
    {
        enemyParent = Resources.Load("Prefabs/Enemy") as GameObject; //Loads the enemy prefab from file
        enemiesSpawning = true;

        roomParents = new RoomData[GameObject.FindGameObjectsWithTag("SpawnRooms").Length]; //Sets the length of RoomParents to the length of returned array of GameObjects with the tag "SpawnRooms"
        for (int i = 0; i < roomParents.Length; i++) //Loop used to set up the RoomData values per entry in the array
        {
            roomParents[i].room = GameObject.FindGameObjectsWithTag("SpawnRooms")[i]; //Sets the room parent
            roomParents[i].spawnPoints = roomParents[i].room.GetComponentsInChildren<Transform>(); //Gathers and stores a list of all the room's spawnpoints into an array
            for (int x = 0; x < roomParents[i].spawnPoints.Length; x++)
            {
                Debug.Log(roomParents[i].spawnPoints[x]);
            }
        }

        UnlockRoom(0); //Calls upon UnlcokRoom to add the spawn room data to activeSpawners
    }

    public void Update()
    {
        if (enemiesSpawning) //Checks whether or not enemies are needed to be spawned
        {
            for (int i = 0; i < activeSpawners.Count; i++) //Used to cycle through spawnpoints
            {
                StartCoroutine(Spawn(i)); //Spawns an enemy
                if (i == activeSpawners.Count - 1)
                {
                    enemiesSpawning = false;
                    StartCoroutine(Cooldown());
                }
            }
        }
    }
    #endregion

    #region Add Spawners
    public void UnlockRoom(int index) //Used to unlock a room to add the spawners to the activeSpawner List
    {
        for (int i = 1; i < roomParents[index].spawnPoints.Length; i++) //For all spawners within the room
        {
            activeSpawners.Add(roomParents[index].spawnPoints[i]); //Adds each spawner to the activeSpawner List
        }
    }
    #endregion

    #region Spawn Enemy
    IEnumerator Spawn(int spawnIndex)
    {
        yield return new WaitForEndOfFrame(); //Determines the amount of time between each set spawn
        GameObject enemy = Instantiate(enemyParent, activeSpawners[spawnIndex].position, Quaternion.identity, activeSpawners[spawnIndex]); //Instantiates a new enemy based on the spawnpoint arry's index
    }
    #endregion

    #region Cooldown
    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(3f);
        enemiesSpawning = true;
    }
    #endregion
}
