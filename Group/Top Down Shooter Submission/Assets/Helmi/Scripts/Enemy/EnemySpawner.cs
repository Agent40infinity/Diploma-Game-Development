using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Space]
    [Header("Spawner Stuff")]
    public GameObject[] shieldZombies; // Reference to all Shield Zombies
    public Transform[] zombieSpawnersPos; // Reference to Zombie spawn pos
    public GameObject enemyPrefab;  // Reference to Zombie
    public float firstSpawnTime = 3f; // Default
    public float spawnDelay = 4f; // Default
    [Space]
    [Header("Animation Stuff")]
    public Animator witchAnim;
    public Animator particleAnim;


    private void Start()
    {
       // Set every Shield Zombie inside shieldZombies array
       foreach(GameObject shieldZombie in shieldZombies)
       {
            // Set off to every shield zombie
            shieldZombie.SetActive(false);
       } 
    }

    // Update is called once per frame
    void Update()
    {
        // When time is more than 0
        if (Time.time > 0f)
        {
            for (int i = 0; i < shieldZombies.Length; i++)
            {
                if(shieldZombies[i] != null)
                {
                    shieldZombies[i].SetActive(true);
                }

                else
                {
                    shieldZombies[i] = null;
                }
            }

            #region Trash
            // >> Code that I tried to use at first << 

            //foreach (GameObject shieldZombie in shieldZombies)
            //{
            //    shieldZombie.SetActive(true);
            //}

            #region Set Up Shield Zombie
            #region First Zombie Slot
            //// When the first zombie slot exists
            //if (shieldZombies[0] != null)
            //{
            //    // Set the first zombie slot on
            //    shieldZombies[0].SetActive(true);
            //}

            //// When the first zombie slot does not exists
            //else if (shieldZombies[0] == null)
            //{
            //    // Makes it none
            //    shieldZombies[0] = null;
            //}
            #endregion

            #region Second Zombie Slot
            //// When the second zombie slot exists
            //if (shieldZombies[1] != null)
            //{
            //    // Set the second zombie slot on
            //    shieldZombies[1].SetActive(true);
            //}

            //// When the second zombie slot does not exists
            //else if (shieldZombies[1] == null)
            //{
            //    // Makes it none
            //    shieldZombies[1] = null;
            //}
            #endregion

            #region Third Zombie Slot
            //// When the third zombie slot exists
            //if (shieldZombies[2] != null)
            //{
            //    // Set the third zombie slot on
            //    shieldZombies[2].SetActive(true);
            //}

            //// When the third zombie slot does not exists
            //else if (shieldZombies[2] == null)
            //{
            //    // Makes it none
            //    shieldZombies[2] = null;
            //}
            #endregion

            #region Fourth Zombie Slot
            //// When the fourth zombie slot exists
            //if (shieldZombies[3] != null)
            //{
            //    // Set the fourth zombie slot on
            //    shieldZombies[3].SetActive(true);
            //}

            //// When the fourth zombie slot does not exists
            //else if (shieldZombies[3] == null)
            //{
            //    // Makes it none
            //    shieldZombies[3] = null;
            //}
            #endregion
            #endregion
            #endregion
        }

        if (Time.time > firstSpawnTime - 0.3f)
        {
            // Set Attack animation on
            witchAnim.SetBool("IsAttack", true);
        }

        if (Time.time > firstSpawnTime - 0.2f)
        {
            // Set Particle animation on
            particleAnim.SetBool("ParticleIsOn", true);
        }

        // When time is more than 3 seconds (default is set to 3 seconds)
        if (Time.time >= firstSpawnTime)
        {
            // Set first spawn time to the current time + spawnDelay (it is set to this so that it can loop again and again)
            firstSpawnTime = Time.time + spawnDelay;
            // Set attack animation to off
            witchAnim.SetBool("IsAttack", false);
            // Set particle animation to off
            particleAnim.SetBool("ParticleIsOn", false);

            // Instantiate first enemy
            GameObject enemy = Instantiate(enemyPrefab, zombieSpawnersPos[0].position, Quaternion.identity);
            // Instantiate second enemy
            GameObject enemy1 = Instantiate(enemyPrefab, zombieSpawnersPos[1].position, Quaternion.identity);
            // Instantiate third enemy
            GameObject enemy2 = Instantiate(enemyPrefab, zombieSpawnersPos[2].position, Quaternion.identity);
            // Instantiate fourth enemy
            GameObject enemy3 = Instantiate(enemyPrefab, zombieSpawnersPos[3].position, Quaternion.identity);
        }
    }
}
