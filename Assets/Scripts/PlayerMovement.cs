﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerMovement : MonoBehaviour
{

    public GameObject coinManager;
    public CoinManager coinScript;
    public ScoreManager scoreManager;

    private CharacterController controller;
    private PlayerAnimator playerAnimator;
    private Vector3 moveVector;



    private float speedZ = 8f;
    private float speedX = 8f;
    private float speedY = 4f;

    private float currentSpeedZ = 0f;
    private float currentSpeedY = 0f;
    private float currentSpeedX = 0f;

    private float gravity = -12f;

    private int isMovingInY = 0;
    private int isMovingInX = 0;

    private int isMovingInYThreshold = 1;

    private int currentLane = 0;


    private bool isDead = false;
    private float deathDepthThreshold = -10f;

    private float lastCoinHit = 0f;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponent<PlayerAnimator>();
        coinScript = coinManager.GetComponent<CoinManager>();
        scoreManager = GetComponent<ScoreManager>();
        //Z
        currentSpeedZ = speedZ;
        AnalyticsEvent.FirstInteraction();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) {
            return;
        }


        Swipe dir = SwipeManager.swipeDirection;
        checkFalling();




        //TODO implementeren van links en rechts "tappen" voor besturing
        //X
        float input = Input.GetAxisRaw("Horizontal");
        if (isMovingInX == 0) {
            if (dir == Swipe.Right || dir == Swipe.TapRight) {
                currentSpeedX = speedX;
                isMovingInX += 1;
                currentLane += 1;
                StartCoroutine(stopMovingInX());

            } else if (dir == Swipe.Left || dir == Swipe.TapLeft) {
                currentSpeedX = -speedX;
                isMovingInX += 1;
                currentLane -= 1;
                StartCoroutine(stopMovingInX());

            } else if (input != 0) {
                currentSpeedX = input * speedX;
                isMovingInX += 1;
                currentLane += (int)input;
                StartCoroutine(stopMovingInX());
            }
        }
        //


        //Y
        if (dir == Swipe.Up || dir == Swipe.TapMiddle || Input.GetAxisRaw("Vertical") == 1) {
            if (isMovingInY < isMovingInYThreshold) {
                //FIXME werkt  ng maar een keer als threshold hoger staat, ook animatie fixen bij verandering
                //voorlopig goed zo
                isMovingInY += 1;
                currentSpeedY = speedY;
            }
            if (isMovingInY == 1) {
                playerAnimator.jumpAnimation();
            }
        }

        if (controller.isGrounded) {
            isMovingInY = 0;
        } else {
            currentSpeedY += gravity * Time.deltaTime;
        }
        //

        controller.Move(new Vector3(currentSpeedX, currentSpeedY, currentSpeedZ) * Time.deltaTime);
    }

    public void setSpeed(float multiplier)
    {
        currentSpeedZ = currentSpeedZ * multiplier;
    }


    IEnumerator stopMovingInX()
    {
        yield return new WaitForSeconds(1 / Mathf.Abs(currentSpeedX));
        transform.position = new Vector3((int)transform.position.x, transform.position.y, transform.position.z);
        currentSpeedX = 0;
        isMovingInX = 0;
    }

    public void slowDownFor(float seconds)
    {
        StartCoroutine(slowDownForNum(seconds));
    }

    IEnumerator slowDownForNum(float seconds)
    {
        currentSpeedZ = currentSpeedZ / 3;
        yield return new WaitForSeconds(seconds);
        currentSpeedZ = currentSpeedZ * 3;

    }

    private void checkFalling()
    {
        if (controller.transform.position.y < deathDepthThreshold) {
            Die("Fall");
        }
    }



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Obstacle" && hit.point.z > transform.position.z + controller.radius) {
            Die("Hit");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Coin")
        {
            GameObject coin = other.gameObject;
            float pos = coin.transform.position.z;
            if (pos != lastCoinHit)
            {
                coinScript.deleteCoin(coin);
                scoreManager.collectCoin();
                lastCoinHit = pos;
            }
        }
    }

    //TODO reden van dood meegeven -> gevallen, gebotst tegen iets...
    private void Die(string reason)
    {
        isDead = true;
        playerAnimator.idleAnimation();
        //TODO stop animatie
        GetComponent<ScoreManager>().Die();
        AnalyticsEvent.Custom("Death", new Dictionary<string, object> { { "Cause", reason } });

    }
}
