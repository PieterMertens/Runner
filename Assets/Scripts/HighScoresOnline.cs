using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class HighScoresOnline : MonoBehaviour {


    private string secretKey = ""; // Edit this value and make sure it's the same as the one stored on the server
        private string addScoreURLsec = "https://infrunner.000webhostapp.com/db/update_user_score.php?"; //be sure to add a ? to your url
        private string highscoreURLsec = "https://infrunner.000webhostapp.com/db/ranglijst_top.php";
    private string addScoreURL = "http://infrunner.000webhostapp.com/db/update_user_score.php?"; //be sure to add a ? to your url
    private string highscoreURL = "http://infrunner.000webhostapp.com/db/ranglijst_top.php";



    public Text HighscoresText;

    void Start()
        {
            StartCoroutine(GetScores(highscoreURLsec));
        secretKey = PlayerPrefs.GetString("c");
    }


    IEnumerator Register(string name)
    {
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        string code = GetRandomString();
        Debug.Log("random string=" + code);

        //string hash = MD5Test.Md5Sum(name + code);

        string post_url = addScoreURL + "userName=" + WWW.EscapeURL(name) + "&code=" + WWW.EscapeURL(code);

        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null) {
            print("There was an error posting the high score: " + hs_post.error);
        } else {
            PlayerPrefs.SetString("c", code);
            string response = hs_post.text; // this is a GUIText that will display the scores in game.
            Debug.Log(response);
        }
    }





    // remember to use StartCoroutine when calling this function!
    IEnumerator PostScores(string name, int score)
        {
            //This connects to a server side php script that will add the name and score to a MySQL DB.
            // Supply it with a string representing the players name and the players score.
            //string hash = MD5Test.Md5Sum(name + score + secretKey);

            string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&code=" + PlayerPrefs.GetString("c");

            // Post the URL to the site and create a download object to get the result.
            WWW hs_post = new WWW(post_url);
            yield return hs_post; // Wait until the download is done

            if (hs_post.error != null) {
                print("There was an error posting the high score: " + hs_post.error);
            }
        }

        // Get the scores from the MySQL DB to display in a GUIText.
        // remember to use StartCoroutine when calling this function!
        IEnumerator GetScores(string url)
        {
        HighscoresText.text = "Loading Scores";
            WWW hs_get = new WWW(url);
            yield return hs_get;

            if (hs_get.error != null) {
            if (hs_get.error == "Unable to complete SSL connection") {
                Debug.Log("trying insec connection");
                StartCoroutine(GetScores(highscoreURL));
            }
            print("There was an error getting the high score: " + hs_get.error);
            HighscoresText.text = "Something went wrong! :(";
            } else {
            Debug.Log("--- header="+hs_get.responseHeaders);
            Debug.Log("--- text="+hs_get.text);
            HighscoresText.text = hs_get.text; // this is a GUIText that will display the scores in game.
            }
        }

    public string GetRandomString()
    {
        string path = Path.GetRandomFileName();
        path = path.Replace(".", ""); // Remove period.
        return path;
    }

}