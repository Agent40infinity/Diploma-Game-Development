using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    #region Variables
    //General


    //References
    public GameObject main;
    public GameObject settings;
    public AudioMixer masterMixer; //Creates reference for the menu music
    Resolution[] resolutions; //Creates reference   for all resolutions within Unity
    public Dropdown resolutionDropdown; //Creates reference for the resolution dropdown 
    #endregion

    #region General
    public void Start()
    {
        resolutions = Screen.resolutions;
        if (resolutionDropdown.options.Count > 0)
        {
            resolutionDropdown.ClearOptions();
        }
        int currentResolutionIndex = 0;
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++) //Load possible resolutions into list
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) //Makes sure the resolution is correctly applied
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        main.SetActive(true);
        settings.SetActive(false);
    }
    #endregion

    #region Main Menu
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings(bool toggle)
    {
        if (toggle)
        {
            settings.SetActive(true);
        }
        else
        {
            settings.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
    #endregion

    #region Settings
    public void ChangeResolution(int resIndex) //Trigger for changing and applying resolution based on list
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void ChangeVolume(float volume) //Trigger for changing volume of game's master channel
    {
        masterMixer.SetFloat("Master", volume);
    }

    public void ToggleFullscreen(bool isFullscreen) //Trigger for applying fullscreen
    {
        Screen.fullScreen = isFullscreen;
    }

    public void Back()
    {
        Settings(false);
    }
    #endregion

}
