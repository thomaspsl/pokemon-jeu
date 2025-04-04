using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // public UiManager UiManager { get; private set; }
    // public AudioManager AudioManager { get; private set; }
    // public ScoreManager ScoreManager { get; private set; }
    // public TimeManager TimeManager { get; private set; }
    public PokemonManager PokemonManager { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        // UiManager = GetComponent<UiManager>();
        // AudioManager = GetComponent<AudioManager>();
        // ScoreManager = GetComponent<ScoreManager>();
        // TimeManager = GetComponent<TimeManager>();
        PokemonManager = GetComponent<PokemonManager>();

        StartGame();
    }

    // --

    public void StartGame()
    {
        // TimeManager.OnTimeUp += TimeUpHandler;

        // UiManager.StartGame();
        // AudioManager.StartGame();
        // ScoreManager.StartGame();
        // TimeManager.StartGame();
        PokemonManager.StartGame();
    }
    
    public void StopGame()
    {
        // TimeManager.OnTimeUp -= TimeUpHandler;

        // UiManager.StopGame();
        // AudioManager.StopGame();
        // ScoreManager.StopGame();
        // TimeManager.StopGame();
        PokemonManager.StopGame();
    }

    // private void TimeUpHandler()
    // {
    //     StopGame();
    // }
}
