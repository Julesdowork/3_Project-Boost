﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
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

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))    // can thrust while rotating
        {
            m_rigidbody.AddRelativeForce(Vector3.up);
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

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        } else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
        }

        m_rigidbody.freezeRotation = false;     // resume physics control over rotation
    }
}
