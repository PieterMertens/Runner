using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    public Animator animator;

	// Use this for initialization
	void Start () {

        animator = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {


		
	}

    public void jumpAnimation() {
        animator.Play("Jump");
    }

    public void idleAnimation() {
        animator.Play("Idle");
    }

    public void danceAnimation()
    {
        int randomIndex = Random.Range(0, 3);

        if (randomIndex == 0) {
            animator.Play("Backflip_noskin");
        } else if (randomIndex == 1) {
            animator.Play("Dancing Running Man_noskin");
        } else if (randomIndex == 2) {
            animator.Play("Front Flip_noskin");
        } else if (randomIndex == 3) {
            animator.Play("Dancing Running Man_noskin");
        } else if (randomIndex == 4) {
            animator.Play("Dancing Running Man_noskin");
        } else if (randomIndex == 5) {
            animator.Play("Dancing Running Man_noskin");
        } else if (randomIndex == 6) {
            animator.Play("Dancing Running Man_noskin");
        } else if (randomIndex == 7) {
            animator.Play("Dancing Running Man_noskin");
        }

    }

    

}
