using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Propriétés
    public static GameManager Instance { get; private set; }
    public AudioManager AudioManager { get; private set; }
    public PokemonManager PokemonManager { get; private set; }
    public UiManager UiManager { get; private set; }
    public ScoreManager ScoreManager { get; private set; }
    public TimeManager TimeManager { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        AudioManager = GetComponent<AudioManager>();
        PokemonManager = GetComponent<PokemonManager>();
        UiManager = GetComponent<UiManager>();
        ScoreManager = GetComponent<ScoreManager>();
        TimeManager = GetComponent<TimeManager>();

        StartGame();
    }

    public void StartGame()
    {
        TimeManager.OnTimeUp += TimeUpHandler;

        AudioManager.StartGame();
        PokemonManager.StartGame();
        TimeManager.StartGame();
    }

    private void TimeUpHandler()
    {
        SceneManager.LoadScene("EndScene");
    }
}
