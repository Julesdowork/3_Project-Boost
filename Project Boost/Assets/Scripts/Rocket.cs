using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 300f;

    Rigidbody m_rigidbody;
    AudioSource m_audioSource;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with friendly");
                break;
            case "Fuel":
                Debug.Log("Fueled up");
                break;
            default:
                Debug.Log("You're dead");
                break;
        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))    // can thrust while rotating
        {
            m_rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!m_audioSource.isPlaying)   // so it doesn't layer on top of each other
            {
                m_audioSource.Play();
            }
        } else {
            m_audioSource.Stop();
        }
    }

    private void Rotate()
    {
        m_rigidbody.freezeRotation = true;      // take manual control of rotation
        
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        } else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        m_rigidbody.freezeRotation = false;     // resume physics control over rotation
    }


}
