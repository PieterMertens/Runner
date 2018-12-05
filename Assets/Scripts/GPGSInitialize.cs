using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPGSInitialize : MonoBehaviour {

	// Use this for initialization
	void Start () {

        // Create client configuration
        PlayGamesClientConfiguration config = new
            PlayGamesClientConfiguration.Builder()
            .Build();

        // Enable debugging output (recommended)
        PlayGamesPlatform.DebugLogEnabled = true;

        // Initialize and activate the platform
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        SignIn();

        // Try silent sign-in (second parameter is isSilent)
        PlayGamesPlatform.Instance.Authenticate(SignInCallback, true);


    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void SignInCallback(bool success)
    {
        if (success) {
            Debug.Log("################## INFIRUNNER Signed in!");

            // Change sign-in button text
            //signInButtonText.text = "Sign out";

            // Show the user's name
            //authStatus.text = "Signed in as: " + Social.localUser.userName;
        } else {
            Debug.Log("################## INFIRUNNER Sign-in failed...");

            // Show failure message
            //signInButtonText.text = "Sign in";
            //authStatus.text = "Sign-in failed";
        }
    }

    public void SignIn()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated) {
            // Sign in with Play Game Services, showing the consent dialog
            // by setting the second parameter to isSilent=false.
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
        } /*else {
            // Sign out of play games
            PlayGamesPlatform.Instance.SignOut();

            // Reset UI
            //signInButtonText.text = "Sign In";
            //authStatus.text = "";
        }*/
    }

}
