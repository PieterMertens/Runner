using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleManager : MonoBehaviour {

    
    public Text ToggleText;


    void Start()
    {
       
    }

    //Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle change)
    {
        
    }
    // Update is called once per frame
    void Update() {

    }

    public void togglePlayerPref(string s) {
        if (PlayerPrefs.HasKey(s)) {
            int value = PlayerPrefs.GetInt(s);
            if (value == 1) PlayerPrefs.SetInt(s, 0);
            else PlayerPrefs.SetInt(s, 1);
        }
    }

    public void switchMusicToggle() {
        if (PlayerPrefs.GetInt("music") == 0) ToggleText.text = "✔ Enabled";
        else ToggleText.text = "Disabled"; 
        togglePlayerPref("music");
        
    }

    public void switchSoundsToggle() {
        if (PlayerPrefs.GetInt("sounds") == 0) ToggleText.text = "✔ Enabled";
        else ToggleText.text = " X Disabled";
        togglePlayerPref("sounds");
    }

}
