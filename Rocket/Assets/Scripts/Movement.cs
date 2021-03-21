using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustSpeed = 100f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] AudioClip ThrustSound;
    [SerializeField] ParticleSystem ThrustParticles;
    [SerializeField] ParticleSystem LeftParticles;
    [SerializeField] ParticleSystem RightParticles;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    
    void ProcessThrust()
    {
        if (Input.GetKey("space"))
        {
            StartThrust();
        }
        else
        {
            StopThrust();
        }
    }

    void StartThrust()
    {
        if (!ThrustParticles.isPlaying)
        {
            ThrustParticles.Play();
        }

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(ThrustSound);
        }

        rb.AddRelativeForce(0, thrustSpeed * Time.deltaTime, 0);
    }

    void StopThrust()
    {
        audioSource.Stop();
        ThrustParticles.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey("a"))
        {
            StartRotationLeft(rotationSpeed);
        } 
        else if (Input.GetKey("d"))
        {
            StartRotationRight(rotationSpeed);
        }
        else
        {
            StopRotation();
        }
    }

    void StartRotationLeft(float rotationThisFrame)
    {
        ApplyRotation(rotationThisFrame);

        if (!LeftParticles.isPlaying)
        {
            LeftParticles.Play();
        }
    }

    void StartRotationRight(float rotationThisFrame)
    {
        ApplyRotation(-rotationThisFrame);

        if (!RightParticles.isPlaying)
        {
            RightParticles.Play();
        }
    }

    void StopRotation()
    {
        LeftParticles.Stop();
        RightParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(0, 0, rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
