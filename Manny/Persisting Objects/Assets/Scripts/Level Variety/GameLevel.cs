using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField]
    SpawnZone spawnZone;

    public void Start()
    {
        Game.Instance.SpawnZoneOfLevel = spawnZone;
    }
}
