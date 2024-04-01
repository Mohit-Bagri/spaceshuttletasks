using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Officetriggerzone : MonoBehaviour
{
    public AudioClip officeTriggerClip; // Renamed to officeTriggerClip

    private AudioSource audioSource;
    private bool hasPlayed;

    void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

        // Make sure the AudioClip is assigned and the AudioSource is set to play on awake
        if (officeTriggerClip != null && audioSource != null)
        {
            audioSource.clip = officeTriggerClip; // Assigning the officeTriggerClip
            audioSource.playOnAwake = false;
        }
        else
        {
            Debug.LogWarning("AudioClip or AudioSource is not assigned!");
        }

        // Initialize the flag to false
        hasPlayed = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is tagged as "Player" and the audio hasn't been played yet
        if (other.CompareTag("Player") && !hasPlayed)
        {
            // Check if audioSource and officeTriggerClip are assigned
            if (audioSource != null && officeTriggerClip != null)
            {
                // Play the audio clip
                audioSource.Play();

                // Set the flag to true to indicate that the audio has been played
                hasPlayed = true;
            }
            else
            {
                Debug.LogWarning("AudioClip or AudioSource is not assigned!");
            }
        }
    }
}
