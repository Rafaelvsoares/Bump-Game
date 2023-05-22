using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustIntensity = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem jetBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;
    Rigidbody rb;
    AudioSource audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else 
        {
            StopThrusting();
        }
    }
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RotateRight();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateLeft();
        }
        else
        {
            StopRotation();
        }

    }

    void StopThrusting()
    {
        audioSource.Stop();
        jetBoosterParticles.Stop();
    }
    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustIntensity * Time.deltaTime);
        if (jetBoosterParticles.isStopped)
        {
            jetBoosterParticles.Play();
        }
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }
    
    void RotateLeft()
    {
        if(leftBoosterParticles.isStopped)
          {
              leftBoosterParticles.Play();
          }
          ApplyRotation(-rotationThrust);
    }
    void RotateRight()
    {
      if (rightBoosterParticles.isStopped)
      {
        rightBoosterParticles.Play();
      }
      ApplyRotation(rotationThrust);
    }
    void StopRotation()
    {
        leftBoosterParticles.Stop();
        rightBoosterParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}

