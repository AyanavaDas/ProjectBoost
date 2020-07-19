﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rocket;
    AudioSource thruster;
 
    void Start()
    {
        rocket = GetComponent<Rigidbody>();
        thruster = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessInput();
        
    }

    private void ProcessInput()
    {
        Thrusting();

        Rotation();
    }

    private void Thrusting()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //relative force because we want to use local coordinates// 
            //vector3 up adds force in the y direction//
            rocket.AddRelativeForce(Vector3.up);
            if (!thruster.isPlaying)
                thruster.Play(0);

        }
    }

    private void Rotation()
    {
        //taking manual control of rotation
        rocket.freezeRotation = true; 
        if (Input.GetKey(KeyCode.A))
        {
            //vector3 forward is +z direction
            //rotates according to left thumb rule
            transform.Rotate(Vector3.forward);
            if (thruster.isPlaying)
                thruster.Stop();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //rotates according to left thumb rule
            transform.Rotate(-(Vector3.forward));
            if (thruster.isPlaying)
                thruster.Stop();
        }
        //resuming physical rotation
        rocket.freezeRotation = false;
    }
}
