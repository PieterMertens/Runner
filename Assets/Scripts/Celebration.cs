using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celebration : MonoBehaviour {

    public ParticleSystem particleSystem;
    private int currentMilestone = 0;
    private int milestoneDistance = 250;

    private PlayerAnimator playerAnimator;
    private PlayerMovement playerMovement;

    // Use this for initialization
    void Start()
    {
        currentMilestone += milestoneDistance;
        playerAnimator = GetComponent<PlayerAnimator>();
        playerMovement = GetComponent<PlayerMovement>();
        particleSystem.loop = false;
    }
	
	// Update is called once per frame
	void Update () {

       
        if (checkForMilestone()) {
            particleSystem.Play();
        }
		
	}


    private bool checkForMilestone() {
        if ((int) transform.position.z % milestoneDistance == 0 && transform.position.z >= currentMilestone) {
            currentMilestone += milestoneDistance;
            playerMovement.slowDownFor(2);
            playerAnimator.danceAnimation();

            return true;
        }
        return false;
    }


}
