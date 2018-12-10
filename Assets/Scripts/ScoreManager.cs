using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
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
        score = transform.position.z + coins;
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
        updateHighScore();
        updateCoinHighscore();
        updateTotalCoins();
        updateTotalMeterWalked();
        deathMenu.showDeathMenu(score);
        coins = 0;
    }

    public void updateHighScore() {
        if (PlayerPrefs.HasKey("highscore")) {
            int high = PlayerPrefs.GetInt("highscore");
            if (high < score) PlayerPrefs.SetInt("highscore", ((int)System.Math.Floor(score)));
            AnalyticsEvent.Custom(PlayerPrefs.GetString("id"), new Dictionary<string, object> { {"Score", score }, { "Highscore", high } });
            return;
        }
        PlayerPrefs.SetInt("highscore", (int)System.Math.Floor(score));
        AnalyticsEvent.Custom(PlayerPrefs.GetString("id"), new Dictionary<string, object> { { "Score", score }, { "Highscore", score } });

    }

    public void updateCoinHighscore() {
        if (PlayerPrefs.HasKey("coins_colleted_in_one_game"))
        {
            int high = PlayerPrefs.GetInt("coins_colleted_in_one_game");
            if (high < score) PlayerPrefs.SetInt("coins_colleted_in_one_game", ((int)System.Math.Floor(score)));
            return;
        }
        PlayerPrefs.SetInt("coins_colleted_in_one_game", (int)System.Math.Floor(score));

    }

    public void updateTotalCoins() {
        if (PlayerPrefs.HasKey("coins_collected_total"))
        {
            int coins_total = PlayerPrefs.GetInt("coins_collected_total");
            PlayerPrefs.SetInt("coins_collected_total", (coins + coins_total));
        }
        else {
            PlayerPrefs.SetInt("coins_collected_total", (coins));
        }
    }

    public void updateTotalMeterWalked() {
        if (PlayerPrefs.HasKey("total_meter_walked"))
        {
            int total_meter_walked = PlayerPrefs.GetInt("total_meter_walked");
            PlayerPrefs.SetInt("total_meter_walked", ((int) score + total_meter_walked));
        }
        else
        {
            PlayerPrefs.SetInt("total_meter_walked", (coins));
        }
    }

    public void Revive() {
        isDead = false;
    }

    public void collectCoin()
    {
        coins += 1;
    }


}
