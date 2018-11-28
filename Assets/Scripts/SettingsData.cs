using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsData : MonoBehaviour {

    private static bool sounds = true;
    private static bool music = true;
    private static bool nightmode = false;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public static bool getSounds() {
        return sounds;
    }

    public static bool getMusic() {
        return music;
    }

    public static bool getNightmode() {
        return nightmode;
    }

    public static void toggleSounds() {
        sounds = !sounds;
    }

    public static void toggleMusic() {
        music = !music;
    }

    public static void toggleNightmode() {
        nightmode = !nightmode;
    }


}
