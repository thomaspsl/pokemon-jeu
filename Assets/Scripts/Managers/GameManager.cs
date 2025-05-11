using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Propriétés
    public static GameManager Instance { get; private set; }
    public AudioManager AudioManager { get; private set; }
    public PlayerManager PlayerManager { get; private set; }
    public PokeballManager PokeballManager { get; private set; }
    public PokemonManager PokemonManager { get; private set; }
    public ScoreManager ScoreManager { get; private set; }
    public TimeManager TimeManager { get; private set; }
    public UiManager UiManager { get; private set; }

    /*
    * Function to wake managers 
    */
    private void Awake()
    {
        if (Instance == null) Instance = this; else Destroy(gameObject);

        this.AudioManager = GetComponent<AudioManager>();
        this.PlayerManager = GetComponent<PlayerManager>();
        this.PokeballManager = GetComponent<PokeballManager>();
        this.PokemonManager = GetComponent<PokemonManager>();
        this.ScoreManager = GetComponent<ScoreManager>();
        this.TimeManager = GetComponent<TimeManager>();
        this.UiManager = GetComponent<UiManager>();

        this.StartGame();
    }

    /*
    * Function to start the game
    */
    public void StartGame()
    {
        this.AudioManager.StartGame();
        this.UiManager.StartGame();
    }

    /*
    * Function to start the gameplay
    */
    public void StartGameplay()
    {
        this.TimeManager.OnTimeUp += this.TimeUpHandler;

        this.UiManager.StartGameplay();
        this.PlayerManager.StartGameplay();
        this.PokeballManager.StartGameplay();
        this.PokemonManager.StartGameplay();
        this.ScoreManager.StartGameplay();
        this.TimeManager.StartGameplay();
    }

    /*
    * Function to handle the time up event
    */
    private void TimeUpHandler()
    {
       this.StopGameplay();
    }

    /*
    * Function to handle the game over event
    */
    public void StopGameplay()
    {
        this.TimeManager.OnTimeUp -= this.TimeUpHandler;

        this.UiManager.StopGameplay();
        this.PokemonManager.StopGameplay();
        this.TimeManager.StopGameplay();
    }

    /*
    * Function to stop the game
    */
    public void StopGame()
    {
        Debug.Log("Stop Game");
        Application.Quit();
    }
}
