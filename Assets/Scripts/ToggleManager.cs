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
        //Fetch the Toggle GameObject
        System.Diagnostics.Debug.WriteLine("TOGGLE NAME" + m_Toggle.name);
        if (m_Toggle.name.Contains("Sounds")) {
            m_Toggle.isOn = SettingsData.getSounds();
        }
        if (m_Toggle.name.Contains("Music")) {

            m_Toggle.isOn = SettingsData.getMusic();
        }
        if (m_Toggle.name.Contains("Nightmode")) {
            m_Toggle.isOn = SettingsData.getNightmode();
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
        if (!m_Toggle.isOn)
            m_Text.text = "Disabled";
        else
            m_Text.text = "Enabled";
    }

    // Update is called once per frame
    void Update () {
		
	}
}
