using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField]float mainThrust = 1000;
    [SerializeField]float rotationThrust = 100;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainThrustParticle;
    [SerializeField] ParticleSystem leftThrustParticle;
    [SerializeField] ParticleSystem rightThrustParticle;

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
        StartThrusting();
    }


    void ProcessRotation()
    {
        RotateLeft();
    }

    private void RotateLeft()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
            if (!rightThrustParticle.isPlaying)
            {
                rightThrustParticle.Play();
            }
        }

        else if (Input.GetKey(KeyCode.D))
        {

            ApplyRotation(-rotationThrust);
            if (!leftThrustParticle.isPlaying)
            {
                leftThrustParticle.Play();
            }
        }
        else
        {
            leftThrustParticle.Stop();
            rightThrustParticle.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;       //freezing rotation so that we can rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;      //unfreezing rotation so that the physics system can take over
        
    }
    private void StartThrusting()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!mainThrustParticle.isPlaying)
            {
                mainThrustParticle.Play();
            }
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            audioSource.Stop();
            mainThrustParticle.Stop();
        }
    }
}
