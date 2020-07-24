using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] float loadTime = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip levelFinish;
    [SerializeField] AudioClip death;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem levelFinishParticles;
    [SerializeField] ParticleSystem deathParticles;
    Rigidbody rocket;
    AudioSource thruster;

    enum State { Alive, Dead, New };
    State state = State.Alive;


    void Start()
    {
        rocket = GetComponent<Rigidbody>();
        thruster = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (state == State.Alive)
            ProcessInput();

    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive||true) //to be removed
            return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":

                break;
            case "Finish":
                {
                    FinishSequence();
                    break;
                }
            default:
                {
                    DeathSequence();
                    break;
                }
        }
           
    }

    private void FinishSequence()
    {
        state = State.New;
        thruster.Stop();
        thruster.PlayOneShot(levelFinish);
        levelFinishParticles.Play();
        Invoke("LoadNextLevel", loadTime);
    }

    private void DeathSequence()
    {
        state = State.Dead;
        thruster.Stop();
        thruster.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadInitLevel", loadTime);
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
                thruster.PlayOneShot(mainEngine);
            mainEngineParticles.Play();

        }
        else
        {
            thruster.Stop();
            mainEngineParticles.Stop();
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

        }
        else if (Input.GetKey(KeyCode.D))
        {
            //rotates according to left thumb rule
            transform.Rotate(-(Vector3.forward)*w);

        }
        //resuming physical rotation
        rocket.freezeRotation = false;
    }
}
