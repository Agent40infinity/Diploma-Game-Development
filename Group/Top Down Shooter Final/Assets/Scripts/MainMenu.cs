using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject overlay;
    public GameObject mainMenu;

    public bool startPressed = false;

    public void Update()
    {
        overlay.SetActive(false);
    }

    public void PlayTheGame()
    {
        overlay.SetActive(true);
        mainMenu.SetActive(false);
        startPressed = true;
    }
    public void GameOver()
    {
        overlay.SetActive(false);
        mainMenu.SetActive(true);
    }
}
