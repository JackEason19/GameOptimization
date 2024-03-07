using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuScreen, settingsScreen;
    //public Button quit, settings, resume, restart;
    void Start()
    {
        mainMenuScreen.SetActive(true);
        settingsScreen.SetActive(false);
    }
    public void StartGame()
    {
        Background_Music background_Music = FindObjectOfType<Background_Music>();
        background_Music.Level1Music();
        SceneManager.LoadScene("Level1");
    }
    public void GoToSettings()
    {
        settingsScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
    }
    public void ReturnToMain()
    {
        mainMenuScreen.SetActive(true);
        settingsScreen.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
