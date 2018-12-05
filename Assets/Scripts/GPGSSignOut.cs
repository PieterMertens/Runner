using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPGSSignOut : MonoBehaviour {

    public Text signInButtonText;


    // Use this for initialization
    void Start() {

        if (PlayGamesPlatform.Instance.localUser.authenticated) {
            signInButtonText.text = "Sign out from Google Play Services";
        } else {
        
            signInButtonText.text = "Sign in on Google Play Services";
            
        }

    }

    // Update is called once per frame
    void Update() {

    }

    public void SignInCallback(bool success)
    {
        if (success) {
            Debug.Log("################## INFIRUNNER Signed in!");

            // Change sign-in button text
            signInButtonText.text = Social.localUser.userName+ " Sign out";

            // Show the user's name
            //authStatus.text = "Signed in as: " + Social.localUser.userName;
        } else {
            Debug.Log("################## INFIRUNNER Sign-in failed...");

            // Show failure message
            signInButtonText.text = "Sign in on Google Play Services";
            //authStatus.text = "Sign-in failed";
        }
    }

    public void SignIn()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated) {
            // Sign in with Play Game Services, showing the consent dialog
            // by setting the second parameter to isSilent=false.
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
        } else {
            // Sign out of play games
            PlayGamesPlatform.Instance.SignOut();

            // Reset UI
            signInButtonText.text = "Sign in on Google Play Services";
            //authStatus.text = "";
        }
    }
}
