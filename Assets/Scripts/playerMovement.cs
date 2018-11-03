using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

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
        moveVector = Vector3.zero;

        //X
        moveVector.x = Input.GetAxisRaw("Horizontal");

        //Y
        if (!controller.isGrounded) {
            verticalVelocity += gravity * Time.deltaTime;
        }
        moveVector.y = verticalVelocity;

        //Z
        moveVector.z = speed;

        controller.Move(moveVector * Time.deltaTime);
	}
}
