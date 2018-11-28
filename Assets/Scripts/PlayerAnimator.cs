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
        int randomIndex = Random.Range(0, 10);
        
        if (randomIndex == 0) {
            animator.Play("Backflip_noskin");
        } else if (randomIndex == 1) {
            animator.Play("Dancing Running Man_noskin");
        } else if (randomIndex == 2) {
            rotateAndBack(2.5f);
            animator.Play("Moonwalk_noskin");
        }else if (randomIndex == 3) {
            animator.Play("Samba Dancing_noskin");
        } else if (randomIndex == 4) {
            animator.Play("Shuffling_noskin");
        } else if (randomIndex == 5) {
            animator.Play("Cross Jumps Rotation_noskin");
        } else if (randomIndex == 6) {
            animator.Play("Drunk Run Forward_noskin");
        } else if (randomIndex == 7) {
            rotateAndBack(2.4f);
            animator.Play("Jog Backward_noskin");
        } else if (randomIndex == 8) {
            animator.Play("Injured Run_noskin");
        } else if (randomIndex == 9) {
            animator.Play("Running Crawl_noskin");
        }
    }

    private void rotateAndBack(float seconds)
    {
        transform.Rotate(Vector3.up, 180);
        StartCoroutine(rotateBackNum(seconds));
    }

    IEnumerator rotateBackNum(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        transform.Rotate(Vector3.up, 180);

    }



}
