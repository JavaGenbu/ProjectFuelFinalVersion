using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    [SerializeField] float rocketPropulsion = 1000f;
    [SerializeField] float rotationSpeed = 250;
    [SerializeField] AudioClip mainEngine;

    //Visual Effects
    [SerializeField] ParticleSystem leftSideThrustParticles;
    [SerializeField] ParticleSystem rightSideThrustParticles;
    [SerializeField] ParticleSystem mainThrustParticles;
    
    Rigidbody rb;
    AudioSource audioSource;

    bool isAlive;



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

    void ProcessThrust(){
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    
    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * rocketPropulsion * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainThrustParticles.isPlaying)
        {
            mainThrustParticles.Play();
        }
    }

    private void StopThrusting()
    {
        mainThrustParticles.Stop();
        audioSource.Stop();
    }

    void ProcessRotation(){
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            rightSideThrustParticles.Stop();
            leftSideThrustParticles.Stop();
        }
    }


     private void RotateLeft()
    {
        ApplyRotation(rotationSpeed);
        if (!rightSideThrustParticles.isPlaying)
        {
            rightSideThrustParticles.Play();
        }
    }
    private void RotateRight()
    {
        ApplyRotation(-rotationSpeed);
        if (!leftSideThrustParticles.isPlaying)
        {
            leftSideThrustParticles.Play();
        }
    }

   

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //Freeze rotation so we can manually rotate (If this line doesnt exist, physics (Rigidbody) and force apllied create conflict and messes the controlls)
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreeze rotation so physics can take over (Rigidbody)
    }



    


}
