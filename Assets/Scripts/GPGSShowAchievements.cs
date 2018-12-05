using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPGSShowAchievements : MonoBehaviour {

    public Button achievementsButton;
    public Text achievementsButtonText;

    public Button leaderboardButton;
    public Text leaderboardButtonsText;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void ShowLeaderboards()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated) {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        } else {

            leaderboardButtonsText.text = "Sing in first";


            StartCoroutine(leaderboardText(1f));

                        Debug.Log("Cannot show leaderboard: not authenticated");
        }
    }

    IEnumerator leaderboardText(float seconds)
    {

        yield return new WaitForSeconds(seconds);
        leaderboardButtonsText.text = "Highscores";


    }

    public void ShowAchievements()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated) {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        } else {
            achievementsButtonText.text = "Sing in first";


           StartCoroutine(achievementsText(1f));
                    

            Debug.Log("Cannot show Achievements, not logged in");
        }
    }

    IEnumerator achievementsText(float seconds)
    {
      
        yield return new WaitForSeconds(seconds);
        achievementsButtonText.text = "Achievements";


    }






}
