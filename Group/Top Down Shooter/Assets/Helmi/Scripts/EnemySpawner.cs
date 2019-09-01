using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Space]
    [Header("Spawner Stuff")]
    public Transform[] spawnersPos;
    public GameObject enemyPrefab;
    public Transform enemyParent;
    public float firstSpawnTime;
    public float spawnDelay;
    [Space]
    [Header("Animation Stuff")]
    public Animator witchAnim;
    public Animator particleAnim;


    // Update is called once per frame
    void Update()
    {

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

            GameObject enemy = Instantiate(enemyPrefab, spawnersPos[0].position, Quaternion.identity, enemyParent);
            GameObject enemy1 = Instantiate(enemyPrefab, spawnersPos[1].position, Quaternion.identity, enemyParent);
            GameObject enemy2 = Instantiate(enemyPrefab, spawnersPos[2].position, Quaternion.identity, enemyParent);
            GameObject enemy3 = Instantiate(enemyPrefab, spawnersPos[3].position, Quaternion.identity, enemyParent);
        }
    }
}
