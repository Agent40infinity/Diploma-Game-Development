using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : MonoBehaviour
{
    public GameObject enemy;
    public float spawnRate = 5f;
    private float _nextSpawn = 0f;
    private float _randomX;
    private Vector2 _spawnPos;

    private void Update()
    {
        if(Time.time > _nextSpawn)
        {
            _nextSpawn = Time.time + spawnRate;
            _randomX = Random.Range(-5f, 5f);
            _spawnPos = new Vector2(_randomX, transform.position.y);
            Instantiate(enemy, _spawnPos, Quaternion.identity);
        }
    }
}
