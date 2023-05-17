using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustIntensity = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem jetBoosterParticles;
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
            rb.AddRelativeForce(Vector3.up * thrustIntensity * Time.deltaTime);
            jetBoosterParticles.Play();
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else {
            audioSource.Stop();
            jetBoosterParticles.Stop();
        }
    }

  
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
    {
      ApplyRotation(rotationThrust);
    }
    else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

  private void ApplyRotation(float rotationThisFrame)
  {
    rb.freezeRotation = true;
    transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    rb.freezeRotation = false;
  }
}

