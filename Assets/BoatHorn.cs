using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatHorn : MonoBehaviour
{
    public AudioSource hornSource;   // assign horn AudioSource in inspector
    public float hornInterval = 30f; // time between auto horn blasts

    private float timer;

    void Start()
    {
        timer = hornInterval;
    }

    void Update()
    {
        // Countdown for auto horn
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            PlayHorn();
            timer = hornInterval; // reset timer
        }

        // Manual horn with G key
        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayHorn();
            timer = hornInterval; // reset timer so it doesn't overlap immediately
        }
    }

    void PlayHorn()
    {
        if (hornSource != null && !hornSource.isPlaying)
        {
            hornSource.Play();
        }
    }
}

