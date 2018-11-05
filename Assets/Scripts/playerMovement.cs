using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private CharacterController controller;
    private Vector3 moveVector;


    public float speed = 3f;

    private float gravity = -10f;
    private float verticalVelocity = 0f;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        Swipe dir = SwipeManager.swipeDirection;
        moveVector = Vector3.zero;

        //X
        if (dir == Swipe.Right) {
            moveVector.x = 1;
        } else if (dir == Swipe.Left){
            moveVector.x = -1;
        }
        moveVector.x = Input.GetAxisRaw("Horizontal");


        //Y
        if (dir == Swipe.Up || dir == Swipe.Tap || Input.GetAxisRaw("Vertical") == 1)
        {
            print("--- Input.GetAxisRaw()" + Input.GetAxisRaw("Vertical").ToString());
            verticalVelocity = 7f;
        }
       

        if (!controller.isGrounded) {
            verticalVelocity += gravity * Time.deltaTime;
        }
        moveVector.y = verticalVelocity;

        //Z
        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);
	}

    public void setSpeed(float multiplier) {
        speed = speed * multiplier;
    }

}
