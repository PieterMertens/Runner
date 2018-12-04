using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleManager : MonoBehaviour {

    [SerializeField]
    Toggle m_Toggle;
    [SerializeField]
    public Text m_Text;


    void Start()
    {
        Debug.Log("IN HEEERE");
        Debug.Log(PlayerPrefs.GetInt("music"));
        //Fetch the Toggle GameObject
        System.Diagnostics.Debug.WriteLine("TOGGLE NAME" + m_Toggle.name);
        if (m_Toggle.name.Contains("Sounds")) {
            if (!PlayerPrefs.HasKey("sounds")) PlayerPrefs.SetInt("sounds", 1);
            m_Toggle.isOn = PlayerPrefs.GetInt("sounds") == 0 ? false : true;
        }
        if (m_Toggle.name.Contains("Music")) {
            if (!PlayerPrefs.HasKey("music")) PlayerPrefs.SetInt("music", 1);
            m_Toggle.isOn = PlayerPrefs.GetInt("music") == 0 ? false: true;
        }
        //Add listener for when the state of the Toggle changes, to take action
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });

        //Initialise the Text to say the first state of the Toggle
        m_Text.text = "Enabled";
    }

    //Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle change)
    {
        Debug.Log(m_Toggle.name);
        if (!m_Toggle.isOn)
            m_Text.text = "Disabled";
        else
            m_Text.text = "Enabled";
        if (m_Toggle.name.Contains("Music"))
        {
            Debug.Log("toggling music");
            PlayerPrefs.SetInt("music", PlayerPrefs.GetInt("music") == 1 ? 0 : 1);
        }
        if (m_Toggle.name.Contains("Sounds"))
            PlayerPrefs.SetInt("sounds", PlayerPrefs.GetInt("sounds") == 1 ? 0 : 1);
        Debug.Log(PlayerPrefs.GetInt("music"));
    }
        // Update is called once per frame
        void Update () {

    }
}
