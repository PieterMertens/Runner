using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 moveVector;


    private float speedZ = 5f;
    private float speedX = 5f;
    private float speedY = 5f;

    private float currentSpeedZ = 0f;
    private float currentSpeedY = 0f;
    private float currentSpeedX = 0f;

    private float gravity = -10f;

    private int isMovingInY = 0;
    private int isMovingInX = 0;

    private int isMovingInYThreshold = 2;

    private int currentLane = 0;


    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();

        //Z
        currentSpeedZ = speedZ;
    }

    // Update is called once per frame
    void Update()
    {
        Swipe dir = SwipeManager.swipeDirection;


        //TODO implementeren van links en rechts "tappen" voor besturing
        //X
        float input = Input.GetAxisRaw("Horizontal");
        if (isMovingInX == 0) {
            if (dir == Swipe.Right|| dir==Swipe.TapRight) {
                currentSpeedX = speedX;
                isMovingInX += 1;
                currentLane += 1;
                StartCoroutine(stopMovingInX());

            } else if (dir == Swipe.Left|| dir==Swipe.TapLeft) {
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
                isMovingInY += 1;
                currentSpeedY = speedY;
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


}
