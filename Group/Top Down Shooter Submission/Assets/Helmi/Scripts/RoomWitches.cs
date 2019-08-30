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
        // Get the cur Room
        curHouse = gameObject;

        #region Trash
        #region First Try
        //foreach (GameObject witch in witches)
        //{
        //    SpriteRenderer witchRenderer = witch.GetComponent<SpriteRenderer>();
        //    EnemySpawner witchSpawner = witch.GetComponent<EnemySpawner>();
        //    witchRenderer.enabled = false;
        //    witchSpawner.enabled = false;
        //}
        #endregion

        #region Second Try
        //if (witches[0] != null)
        //{
        //    SpriteRenderer witchRenderer = witches[0].GetComponent<SpriteRenderer>();
        //    EnemySpawner witchSpawner = witches[0].GetComponent<EnemySpawner>();
        //    witchRenderer.enabled = false;
        //    witchSpawner.enabled = false;
        //}

        //if (witches[1] != null)
        //{
        //    SpriteRenderer witchRenderer = witches[1].GetComponent<SpriteRenderer>();
        //    EnemySpawner witchSpawner = witches[1].GetComponent<EnemySpawner>();
        //    witchRenderer.enabled = false;
        //    witchSpawner.enabled = false;
        //}
        #endregion
        #endregion

        for (int i = 0; i < witches.Length; i++)
        {
            SpriteRenderer witchRenderer = witches[i].GetComponent<SpriteRenderer>();
            EnemySpawner witchSpawner = witches[i].GetComponent<EnemySpawner>();
            // Set the sprite renderer of every witch in the room to false
            witchRenderer.enabled = false;
            // Set the enemy spawner of every witch in the room to false
            witchSpawner.enabled = false;
        }
    }
}
