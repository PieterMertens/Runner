using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    private float score = 0.0f;

    public Text scoreText;

    private PlayerMovement playerMovement;

    private float scoreToNextLevel = 20f;
    private float multiplier = 1f;


	// Use this for initialization
	void Start () {
        playerMovement = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        
        //TODO score ook rekening houden met coins,...
        score = transform.position.z;
        scoreText.text = ((int)score).ToString();

        if (score > scoreToNextLevel) { LevelUp(); }
        
    }

    private void LevelUp() {
        scoreToNextLevel *= 2;
        if (multiplier < 5) { multiplier += 0.05f; } else { multiplier += 0.01f; }
        
        playerMovement.setSpeed(multiplier);
    }

}
