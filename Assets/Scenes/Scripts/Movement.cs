using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;

    Rigidbody rocket;
    AudioSource thruster;
    enum State{ Alive,Dead, New };
    State state = State.Alive;

 
    void Start()
    {
        rocket = GetComponent<Rigidbody>();
        thruster = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(state== State.Alive)
            ProcessInput();
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
            return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":

                break;
            case "Finish":
                {
                    state = State.New;
                    Invoke("LoadNextLevel", 1f);
                    break;
                }
            default:
                {
                    state = State.Dead;
                    Invoke("LoadInitLevel", 1f);
                    break;
                }
        }
           
    }

    private void LoadInitLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);

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
            float v = Time.deltaTime * mainThrust;
            rocket.AddRelativeForce(Vector3.up*v);
            if (!thruster.isPlaying)
                thruster.Play(0);

        }
    }

    private void Rotation()
    {
        //taking manual control of rotation
        rocket.freezeRotation = true;
        float w = Time.deltaTime * rotationThrust;
        if (Input.GetKey(KeyCode.A))
        {
            //vector3 forward is +z direction
            //rotates according to left thumb rule
            transform.Rotate(Vector3.forward*w);
           /* if (thruster.isPlaying)
                thruster.Stop();
            */
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //rotates according to left thumb rule
            transform.Rotate(-(Vector3.forward)*w);
            /*if (thruster.isPlaying)
                thruster.Stop();*/
        }
        //resuming physical rotation
        rocket.freezeRotation = false;
    }
}
