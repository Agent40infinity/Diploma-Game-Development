using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FurnitureSpawner : MonoBehaviour
{
    public List<GameObject> furniturePrefabs = new List<GameObject>();
    public Transform spawnPoint;
    public float timeBetweenSpawns = 4f;

    private float spawnCounter;

    void Start()
    {
        SpawnRandomFurniture();
    }

    void Update()
    {
        spawnCounter += Time.deltaTime;

        if (spawnCounter >= timeBetweenSpawns)
        {
            SpawnRandomFurniture();
            spawnCounter = 0f;
        }
    }

    private void SpawnRandomFurniture()
    {
        if (furniturePrefabs.Count > 0)
        {
            int randomIndex = Random.Range(0, furniturePrefabs.Count);
            GameObject newFurniture = Instantiate(furniturePrefabs[randomIndex], transform);
            newFurniture.transform.position = spawnPoint.position;
            newFurniture.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        }
    }
}
