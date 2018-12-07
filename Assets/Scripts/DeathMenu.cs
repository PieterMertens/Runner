using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

    public Text scoreText;
    public Text highscoreText;
    public Image backgroundImg;
    private float transitionTime = 0.0f;

    public bool isShown;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isShown) {
            return;
        }

        transitionTime += Time.deltaTime;
        backgroundImg.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0,0,0,1), transitionTime);

		
	}

    public void showDeathMenu(float score) {
        isShown = true;
        gameObject.SetActive(true);
        scoreText.text = "Score:" + ((int) score).ToString();
        if (PlayerPrefs.HasKey("highscore"))
            highscoreText.text = "Highscore: " + ((int) PlayerPrefs.GetInt("highscore")).ToString();
        else
            highscoreText.text = "Highscore: " + ((int) score).ToString();
    }

    public void restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void goToMenu() {
        //TODO maak menuscene
        SceneManager.LoadScene("MainMenu");
    }

}
