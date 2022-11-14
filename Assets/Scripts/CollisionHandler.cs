using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField]float levelLoadDelay = 2f; //Time between levels

    //Audio
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip finishSound;

    //Effects

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem finishParticles;

    AudioSource audioSource;
    //ParticleSystem particleSystem;
    bool isTransitioning = false;


    void Start(){
        audioSource = GetComponent<AudioSource>();
        //particleSystem = GetComponent<ParticleSystem>();

        
    }

    void Update(){
        ActivateDebugKeys();
    }


    void OnCollisionEnter(Collision other) {

        if (!isTransitioning){
            switch(other.gameObject.tag){
            case "Finish": Debug.Log("Has llegado al final"); 
            StartSuccessSequence();
            break;
            case "Obstacle": Debug.Log("Has tocado un osbtaculo"); 
            StartCrashSequence();
            break;
            default: break;
            }
        }

        
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        finishParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(finishSound); //Audio play when final platform reached (picked in unity)


        GetComponent<Movement>().enabled = false;
        Invoke ("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence(){
        isTransitioning = true;
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound); //Audio play when crash (picked in unity)
        GetComponent<Movement>().enabled = false;
        Invoke ("realoadLevel", levelLoadDelay + 0.5f);
    }

    void realoadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ActivateDebugKeys(){
        if (Input.GetKey(KeyCode.L)){
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;    //Devuelve la escena actual
            int nextSceneIndex = currentSceneIndex + 1;                         //Devuelve la siguiente escena 
            
            if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
            
            }
            SceneManager.LoadScene(nextSceneIndex);
        }
        else if (Input.GetKey(KeyCode.C)){
            //rb.collider.enabled = false;
        }
    }

    

}
