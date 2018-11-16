using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraMovement : MonoBehaviour
{


    private Transform lookAt;
    private int state;
    private float stateTime;

    // Use this for initialization
    void Start()
    {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        updateState();
        if (state == 0) { } else if (state == 1) {
            
            transform.Translate(Vector3.right * Time.deltaTime);
            transform.LookAt(lookAt);
        } else if (state == 2)
            
        transform.Translate(Vector3.right * Time.deltaTime);
        transform.LookAt(lookAt);
    }


    void updateState()
    {
        if (stateTime > 0) {
            stateTime -= Time.deltaTime;
        } else {
            state = Random.Range(0, 3);
            stateTime = Random.Range(2, 6);
        }

    }

}
