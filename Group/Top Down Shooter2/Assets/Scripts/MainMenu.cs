using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject overlay;
    public GameObject mainMenu;
    public GameObject[] witches;
    public void awake()
    {
        overlay.SetActive(false);
        foreach(GameObject witch in witches)
        {
            witch.SetActive(false);
        }
    }
    public void PlayTheGame()
    {
        overlay.SetActive(true);
        mainMenu.SetActive(false);

        foreach (GameObject witch in witches)
        {
            witch.SetActive(true);
        }
    }
    public void GameOver()
    {
        overlay.SetActive(false);
        mainMenu.SetActive(true);
    }
}
