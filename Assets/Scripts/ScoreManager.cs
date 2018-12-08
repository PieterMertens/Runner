using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    private float score = 0.0f;
    int coins = 0;

    public Text scoreText;
    

    private PlayerMovement playerMovement;
    public DeathMenu deathMenu;

    private float scoreToNextLevel = 20f;
    private float multiplier = 1f;

    private bool isDead = false;

    private GameObject tileManager;
    private TileManager manager;

	// Use this for initialization
	void Start () {
        playerMovement = GetComponent<PlayerMovement>();
        tileManager = GameObject.Find("TileManager");
        manager = tileManager.GetComponent<TileManager>();        
	}
	
	// Update is called once per frame
	void Update () {

        if (isDead) {
            return;
        }
        
        //TODO score ook rekening houden met coins,...
        score = transform.position.z;
        scoreText.text = ((int)score).ToString();

        if (score > scoreToNextLevel) { LevelUp(); }
        
    }

    private void LevelUp() {
        scoreToNextLevel +=scoreToNextLevel+ 15;
        if (multiplier < 3) { multiplier += 0.03f; } else { multiplier += 0.001f; }
        
        playerMovement.setSpeed(multiplier);
        manager.updateObstaclesSpacing();
    }

    public void Die() {
        isDead = true;
        deathMenu.showDeathMenu(score);
        updateHighScore();
    }

    public void updateHighScore() {
        if (PlayerPrefs.HasKey("highscore")) {
            if (PlayerPrefs.GetInt("highscore") < score) PlayerPrefs.SetInt("highscore", ((int) score));
        }
        PlayerPrefs.SetInt("highscore", (int)score);
        
    }

    public void Revive() {
        isDead = false;
    }

    public void collectCoin()
    {
        coins += 1;
    }


}
