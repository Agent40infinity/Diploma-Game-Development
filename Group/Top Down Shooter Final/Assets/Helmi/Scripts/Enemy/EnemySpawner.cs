using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Space]
    [Header("Spawner Stuff")]
    public GameObject[] shieldZombies;
    public Transform[] zombieSpawnersPos;
    public GameObject enemyPrefab;
    public float firstSpawnTime;
    public float spawnDelay;
    [Space]
    [Header("Animation Stuff")]
    public Animator witchAnim;
    public Animator particleAnim;


    private void Start()
    {
       foreach(GameObject shieldZombie in shieldZombies)
       {
            shieldZombie.SetActive(false);
       } 
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > 0f)
        {
            foreach (GameObject shieldZombie in shieldZombies)
            {
                shieldZombie.SetActive(true);
            }
        }

        if (Time.time > firstSpawnTime - 0.3f)
        {
            witchAnim.SetBool("IsAttack", true);
        }

        if (Time.time > firstSpawnTime - 0.2f)
        {
            particleAnim.SetBool("ParticleIsOn", true);
        }

        if (Time.time >= firstSpawnTime)
        {
            firstSpawnTime = Time.time + spawnDelay;
            witchAnim.SetBool("IsAttack", false);
            particleAnim.SetBool("ParticleIsOn", false);

            GameObject enemy = Instantiate(enemyPrefab, zombieSpawnersPos[0].position, Quaternion.identity);
            GameObject enemy1 = Instantiate(enemyPrefab, zombieSpawnersPos[1].position, Quaternion.identity);
            GameObject enemy2 = Instantiate(enemyPrefab, zombieSpawnersPos[2].position, Quaternion.identity);
            GameObject enemy3 = Instantiate(enemyPrefab, zombieSpawnersPos[3].position, Quaternion.identity);
        }

    }
}
