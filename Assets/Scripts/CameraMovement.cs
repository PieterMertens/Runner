using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private Transform lookAt;
    //private Vector3 startOffset;
    private Vector3 moveVector;

    //private float transition = 0.0f;
    //private float animationDuration = 2.0f;
    //private Quaternion targetRotation = Quaternion.Euler(0,0,0);
    private Vector3 cameraOffset = new Vector3(0,3,-5);

    //private float targetDistance = 5f;

	// Use this for initialization
	void Start () {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        //startOffset = transform.position - lookAt.position;

        //print("--- targetrot" + targetRotation.ToString());
	}
	
	// Update is called once per frame
	void Update () {
        moveVector = lookAt.position + cameraOffset;

        //X
        moveVector.x = 0;

        //Y
        moveVector.y = Mathf.Clamp(moveVector.y,-4,10);

        //TODO camera laten draaien in begin (voorkant zien met gezicht)
        /*
        if (transition > 1f) {
            transform.position = moveVector;
        } else {
            moveVector = lookAt.position;
            transform.RotateAround(lookAt.position, lookAt.up, 180 / animationDuration * Time.deltaTime  );
            //Vector3 delta = transform.position - lookAt.position;
            //transform.position = transform.position + delta.normalized * targetDistance;


            transform.position = Vector3.Lerp(transform.position , transform.position + cameraOffset, transition);
            transition += Time.deltaTime / animationDuration;
           

        }
        */

        transform.position = moveVector;
        transform.rotation = Quaternion.Euler(0,0,0);
    		
	}
}
