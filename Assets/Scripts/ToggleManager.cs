using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleManager : MonoBehaviour {

    
    public Text ToggleText;


    void Start()
    {
        initButtons();
    }

    //Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle change)
    {
        
    }
    // Update is called once per frame
    void Update() {

    }

    public void togglePlayerPref(string s) {
        if (PlayerPrefs.HasKey(s))
        {
            int value = PlayerPrefs.GetInt(s);
            if (value == 1) PlayerPrefs.SetInt(s, 0);
            else PlayerPrefs.SetInt(s, 1);
        }
        else {
            PlayerPrefs.SetInt(s, 0);
        }
    }

    public void switchMusicToggle() {
        togglePlayerPref("music");
        if (PlayerPrefs.GetInt("music") == 1) ToggleText.text = "✔ Enabled";
        else ToggleText.text = "X Disabled"; 
        
        
    }

    public void initButtons() {
        if (!PlayerPrefs.HasKey("music")) PlayerPrefs.SetInt("music", 1);
        if(!PlayerPrefs.HasKey("sounds")) PlayerPrefs.SetInt("sounds", 1);
        if (this.name.Contains("Music")) {
            switchMusicToggle(); switchMusicToggle();

        }
        if (this.name.Contains("Sounds")) {
            switchSoundsToggle(); switchSoundsToggle();
        }
    }

    public void switchSoundsToggle() {
        togglePlayerPref("sounds");
        if (PlayerPrefs.GetInt("sounds") == 1) ToggleText.text = "✔ Enabled";
        else ToggleText.text = " X Disabled";
        
    }

}
