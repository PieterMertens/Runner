using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void startGame() { 
    SceneManager.LoadScene("Game");
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void loadStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void loadSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
}
