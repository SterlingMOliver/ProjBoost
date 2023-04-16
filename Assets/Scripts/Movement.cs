using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 10f;
    [SerializeField] float mainRotate = 10f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem leftThrust;
    [SerializeField] ParticleSystem rightThrust;
    [SerializeField] ParticleSystem upThrust;

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

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
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
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!upThrust.isPlaying)
        {
            upThrust.Play();
        }
    }

    void StopThrust()
    {
        audioSource.Stop();
        upThrust.Stop();
    }
    void ProcessRotation()
    {
        StartRotate();

    }

    void StartRotate()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            RotateRight();
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            RotateLeft();
        else
            StopRotate();
    }

    private void RotateRight()
    {
        ApplyRotation(mainRotate);
        if (!rightThrust.isPlaying)
        {
            rightThrust.Play();
        }
    }
    private void RotateLeft()
    {
        ApplyRotation(-mainRotate);
        if (!leftThrust.isPlaying)
        {
            leftThrust.Play();
        }
    }

    private void StopRotate()
    {
        rightThrust.Stop();
        leftThrust.Stop();
    }
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate without Unity screwing it up
        transform.Rotate(Vector3.back * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //thawed
    }

}