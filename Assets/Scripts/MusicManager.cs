using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {


    AudioSource m_MyAudioSource;
    // Use this for initialization
    void Start () {
        m_MyAudioSource = GetComponent<AudioSource>();
        if (!PlayerPrefs.HasKey("music")) PlayerPrefs.SetInt("music", 1);
        if (PlayerPrefs.GetInt("music") == 1)
        {
            if (!m_MyAudioSource.isPlaying) m_MyAudioSource.Play(0);
            m_MyAudioSource.mute = false;
        }
        else
        {
            m_MyAudioSource.Stop();
            m_MyAudioSource.mute = true;
        }
            
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
