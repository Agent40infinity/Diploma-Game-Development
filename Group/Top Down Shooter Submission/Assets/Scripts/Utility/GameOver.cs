using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void GameReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
