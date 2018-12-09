using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class MenuManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (!PlayerPrefs.HasKey("id"))
        {
            PlayerPrefs.SetString("id", System.DateTime.Now.ToString("yyyyMMddHHmmssfff"));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startGame()
    {
        SceneManager.LoadScene("Game");
        AnalyticsEvent.ScreenVisit("Game");
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        AnalyticsEvent.ScreenVisit("MainMenu");
    }

    public void loadStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
        AnalyticsEvent.ScreenVisit("StartMenu");
    }

    public void loadSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
        AnalyticsEvent.ScreenVisit("SettingsMenu");
    }
    public void loadHighscoresMenu()
    {
        SceneManager.LoadScene("Highscores");
        AnalyticsEvent.ScreenVisit("Highscores");
    }
}
