using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public AudioClip mainTheme;

    public AudioSource audioSource;

    public void StartGame()
    {
        audioSource.clip = mainTheme;
        audioSource.Play();
    }

    public void StopGame()
    {
        audioSource.Stop();
    }
}
