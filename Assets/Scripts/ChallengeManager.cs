using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeManager : MonoBehaviour {

    [SerializeField]
    public Text text;
    // Use this for initialization

    private int meters_walked;
    private int highscore;
    private int coins_collected_total;
    private int coins_collected_one_game;

    private List<string> coin_challenges_text = new List<string> {"Collect 10 coins in total!",
                                                                   "Collect 100 coins in total!",
                                                                  "Collect 1000 coins in total",
                                                                  "Collect 10 000 coins in total"};

    private List<string> coin_challenges_text_one_game_text = new List<string> {"Collect 5 coins in one game!",
                                                                   "Collect 20 coins in one game!",
                                                                  "Collect 50 coins in one game!",
                                                                  "Collect 100 coins in one game!"};

    private List<string> total_meters_walked_challenge_text = new List<string> {"Walk 1000 meters in total!",
                                                                   "Walk 10 000 meters in total!",
                                                                  "Walk 100 000 meters in total!",
                                                                  "Walk 1 000 000 meters in total!"};

    private List<string>  highscore_challenge_text = new List<string> {"Get a highscore of 100+!",
                                                                   "Get a highscore of 300+!",
                                                                  "Get a highscore of 600+!",
                                                                  "Get a highscore of 1100!"};


    void Start () {
        updateStats();
        text.text = getAmountOfChallengeCompleted().ToString() + "/16 CHALLENGES COMPLETED.\n\n\n\n" +
            "NEXT CHALLENGE: \n\n" + getNextChallenge();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void updateStats()
    {
        meters_walked = PlayerPrefs.GetInt("total_meter_walked");
        highscore = PlayerPrefs.GetInt("highscore");
        coins_collected_one_game = PlayerPrefs.GetInt("coins_colleted_in_one_game");
        coins_collected_total = PlayerPrefs.GetInt("coins_collected_total");

    }

    public string getNextCoinChallenge() {
        if (coins_collected_one_game >= 50) return coin_challenges_text_one_game_text[3];
        if (coins_collected_one_game >= 20) return coin_challenges_text_one_game_text[2];
        if (coins_collected_one_game >= 5) return coin_challenges_text_one_game_text[1];
        else return coin_challenges_text_one_game_text[0];

    }

    public int getAmountOfChallengeCompleted() {
        int total= 0;
        if (coins_collected_one_game >= 5) total++;
        if (coins_collected_one_game >= 50) total++;
        if (coins_collected_one_game >= 20) total++;
        if (coins_collected_one_game >= 100) total++;
        if (coins_collected_total >= 1000) total++;
        if (coins_collected_total >= 100) total++;
        if (coins_collected_total >= 10) total++;
        if (coins_collected_total >= 10000) total++;
        if (highscore >= 100) total++;
        if (highscore >= 300) total++;
        if (highscore >= 600) total++;
        if (highscore >= 1100) total++;
        if (coins_collected_total >= 1000) total++;
        if (coins_collected_total >= 10000) total++;
        if (coins_collected_total >= 100000) total++;
        if (coins_collected_total >= 1000000) total++;
        return total;
    }

    public string getNextTotalCoinChallenge() {
        if (coins_collected_total >= 1000) return coin_challenges_text[3];
        if (coins_collected_total >= 100) return coin_challenges_text[2];
        if (coins_collected_total >= 10) return coin_challenges_text[1];
        else return coin_challenges_text[0];
    }

    public string getNextHighScoreChallenge() {
        if (highscore >= 600) return highscore_challenge_text[3];
        if (highscore >= 300) return highscore_challenge_text[2];
        if (highscore >= 100) return highscore_challenge_text[1];
        else return highscore_challenge_text[0];
    }

    public string getNextMetersWalkedChallenge() {
        if (meters_walked >= 100000) return total_meters_walked_challenge_text[3];
        if (meters_walked >= 10000) return total_meters_walked_challenge_text[2];
        if (meters_walked >= 1000) return total_meters_walked_challenge_text[1];
        else return total_meters_walked_challenge_text[0];
    }

    public String getNextChallenge() {
        float number = UnityEngine.Random.Range(0,4);
        if (number < 1) return getNextTotalCoinChallenge();
        if (number < 2) return getNextCoinChallenge();
        if (number < 3) return getNextHighScoreChallenge();
        else return getNextMetersWalkedChallenge();
    }


}
