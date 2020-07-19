using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rocket;
 
    void Start()
    {
        rocket = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessInput();
        
    }

    private void ProcessInput()
    {

        if(Input.GetKey(KeyCode.Space))
        {
            //relative force because we want to use local coordinates// 
            //vector3 up adds force in the y direction//
            rocket.AddRelativeForce(Vector3.up);
        }
        //can thrust and rotate
        if(Input.GetKey(KeyCode.A))
        {
            //vector3 forward is +z direction
            //rotates according to left thumb rule
            transform.Rotate(Vector3.forward)
        }
        else if(Input.GetKey(KeyCode.D))
        {
            //rotates according to left thumb rule
            transform.Rotate(-(Vector3.forward));
        }
    }
}
