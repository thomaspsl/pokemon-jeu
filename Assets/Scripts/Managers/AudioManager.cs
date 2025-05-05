using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioClip mainTheme;
    public AudioSource audioSource;

    public void StartGame()
    {
        audioSource.clip = mainTheme;
        audioSource.Play();
    }
}
