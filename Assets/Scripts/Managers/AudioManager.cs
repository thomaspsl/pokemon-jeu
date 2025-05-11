using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip gameTheme;

    /*
    * Start game music
    */
    public void StartGame()
    {
        audioSource.clip = gameTheme;
        audioSource.Play();
    }

    /*
    * Function to play a sound effect
    */
    public static void Play(AudioSource audioSource, AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {        
            audioSource.PlayOneShot(clip);
        }
    }
}
