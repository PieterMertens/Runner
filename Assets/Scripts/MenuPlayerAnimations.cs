using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerAnimations : MonoBehaviour
{

    public Animator animator;

    private float timeBetweenDances = 10;
    private float timeToDance;


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO transitions to new animation
        if (isTimeToDance()) {
            playRandom();


        }
    }

    private bool isTimeToDance()
    {
        if (timeToDance > 0) {
            timeToDance -= Time.deltaTime;
            return false;
        } else {
            timeToDance = timeBetweenDances;
            return true;
        }

    }

    void playRandom()
    {
        int i = Random.Range(0, 8);

        if (i == 0) {
            playHipHopDance();
        } else if (i == 1) {
            playBreakDanceFreeze();
        } else if (i == 2) {
            playDance();
        } else if (i == 3) {
            playHeadSpinning();
        } else if (i == 4) {
            playJazzDance();
        } else if (i == 5) {
            playSwingDance();
        } else if (i == 6) {
            playHipHopTutDance();
        } else if (i == 7) {
            playTwistDance();
        }

    }

    void playHipHopDance()
    {
        animator.Play("Bboy Hip Hop Move_noskin");
    }
    void playBreakDanceFreeze()
    {
        animator.Play("Breakdance Freezes_noskin");
    }
    void playDance()
    {
        animator.Play("Dancing_noskin");
    }
    void playHeadSpinning()
    {
        animator.Play("Head Spinning_noskin");
    }
    void playJazzDance()
    {
        animator.Play("Jazz Dancing2_noskin");
    }
    void playSwingDance()
    {
        animator.Play("Swing Dancing_noskin");
    }
    void playHipHopTutDance()
    {
        animator.Play("Tut Hip Hop Dance_noskin");
    }
    void playTwistDance()
    {
        animator.Play("Twist Dance_noskin");
    }

}