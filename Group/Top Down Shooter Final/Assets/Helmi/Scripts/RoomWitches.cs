using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWitches : MonoBehaviour
{
    public GameObject curHouse;
    public GameObject[] witches;
    // Start is called before the first frame update
    void Start()
    {
        curHouse = gameObject;
        foreach(GameObject witch in witches)
        {
            SpriteRenderer witchRenderer = witch.GetComponent<SpriteRenderer>();
            EnemySpawner witchSpawner = witch.GetComponent<EnemySpawner>();
            witchRenderer.enabled = false;
            witchSpawner.enabled = false;
        }
    }
}
