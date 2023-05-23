using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour

{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip successAudio;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;
    
    bool isTransitioning =  false;
    bool isCollision = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        AdminLoadLevel();
        DisableCollision();
    }

    void OnCollisionEnter(Collision other) 
    {
      if(isCollision) { return; }
      if(isTransitioning) { return; }

      switch (other.gameObject.tag)
      {
          case "Friendly":
              Debug.Log("This object is friendly");
              break;
          case "Finish":
              StartSuccessSequence();
              break;
          default:
              StartCrashSequence();
              break;
      }
      
    }

    void StartCrashSequence()
    {   
        isTransitioning = true;
        audioSource.Stop();
        crashParticles.Play();
        audioSource.PlayOneShot(crashAudio);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        successParticles.Play();
        audioSource.PlayOneShot(successAudio);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);

    }
    void ReloadLevel()
    {   
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {   
        int sceneLength = SceneManager.sceneCountInBuildSettings;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex == sceneLength -1)
        {
          SceneManager.LoadScene(0);
          return;
        }
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    void AdminLoadLevel()
    {
        if(Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }  
    }

    void DisableCollision()
    {
        // Change to key press so it is one single input as this is for real time input.
        if(Input.GetKey(KeyCode.C))
        {
            isCollision = !isCollision;
            Debug.Log(isCollision);
        }
    }
}

