using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //General:
    public float xOffset;
    public float yOffset;
    public int random;

    public int currentScore;
    public int highscore;

    public bool endGame = false;

    //Reference:
    public GameObject player;
    public GameObject spawnRoom;
    public GameObject[] roomPresets;
    public GameObject curRoom;

    public Text score;
    public Text finalScore;
    public Text health;
    public GameObject gameOver;

    public void Start()
    {
        player = GameObject.Find("Player");
        spawnRoom = GameObject.Find("Spawn");
        curRoom = spawnRoom;
        for (int i = 0; i < roomPresets.Length; i++)
        {
            roomPresets[i] = Resources.Load<GameObject>("Prefabs/Presets/Preset " + (i + 1));
            Debug.Log(roomPresets[i]);
        }

        score = GameObject.Find("Score").GetComponent<Text>();
        health = GameObject.Find("Health").GetComponent<Text>();
    }

    public void Update()
    {
        if (curRoom.GetComponent<Room>().beenVisited == 1)
        {
            for (int i = 0; i < curRoom.GetComponent<Room>().doors.Length; i++)
            {
                if (curRoom.GetComponent<Room>().doors[0].GetComponent<Door>().hasChecked == false)
                { xOffset = 0f; yOffset = -18f; }
                else if (curRoom.GetComponent<Room>().doors[1].GetComponent<Door>().hasChecked == false)
                { xOffset = -32f; yOffset = 0f; }
                else if (curRoom.GetComponent<Room>().doors[2].GetComponent<Door>().hasChecked == false)
                { xOffset = 0f; yOffset = 18f; }
                else if (curRoom.GetComponent<Room>().doors[3].GetComponent<Door>().hasChecked == false)
                { xOffset = 32f; yOffset = 0; }

                if (curRoom.GetComponent<Room>().doors[i].GetComponent<Door>().hasChecked == false)
                {
                    random = Random.Range(0, 4);
                    Instantiate(roomPresets[random], new Vector2(curRoom.transform.position.x + xOffset, curRoom.transform.position.y + yOffset), Quaternion.identity);
                    curRoom.GetComponent<Room>().doors[i].GetComponent<Door>().hasChecked = true;
                    Debug.Log(random);
                }
            }
            curRoom.GetComponent<Room>().beenVisited = 2;
        }

        if (endGame == false)
        {
            Health();
        }
    }

    #region Point System
    public void addScore(int points)
    {
        currentScore += points;
        score.text = "Score: " + currentScore;
    }
    #endregion

    #region Display Health
    public void Health()
    {
        health.text = "Health: " + (GameObject.Find("Player").GetComponent<Player>().health + 1);
    }
    #endregion

    #region End Game
    public void EndGame()
    {
        endGame = true;
        gameOver.SetActive(true);
        finalScore.text = "Final Score: " + currentScore;

        GameObject[] witches = GameObject.FindGameObjectsWithTag("Witch");
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        GameObject[] shielded = GameObject.FindGameObjectsWithTag("Shield Zombie");
        for (int i = 0; i < witches.Length; i++)
        {
            Destroy(witches[i]);
        }
        for (int i = 0; i < zombies.Length; i++)
        {
            Destroy(zombies[i]);
        }
        for (int i = 0; i < shielded.Length; i++)
        {
            Destroy(shielded[i]);
        }
    }
    #endregion
}
