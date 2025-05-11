using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class UiManager : MonoBehaviour
{
    [Header("Game Settings")]
    public GameObject gameUI;

    [Header("Gameplay Settings")]
    public GameObject gameplay;
    public GameObject gameplayUI;
    public Text score;
    public Text time;
    public Transform inventory;
    public GameObject slotPrefab;

    [Header("Endgame Settings")]
    public GameObject endgameUI;
    public Text finalScore;

    // Propriétés
    private GameManager Game { get; set; }

    /*
    * Function to wake managers 
    */
    private void Awake()
    {
        this.Game = GameManager.Instance;
    }

    public void isGameplayUI()
    {
        this.gameplayUI.SetActive(true);
    }

    /*
    * Function to update the HUD
    */
    private void Update()
    {
        if (this.gameplayUI.activeSelf) 
        {
            score.text = $"Score: <color=#feae34>{this.Game.ScoreManager.Score}</color>";
            time.text = $"{TimeSpan.FromSeconds(this.Game.TimeManager.Remaining):mm\\:ss}";
        }

        if (this.endgameUI.activeSelf) 
        {
            finalScore.text = $"Score: <color=#feae34>{this.Game.ScoreManager.Score}</color>";
        }
    }

    /*
    * Function to start the game
    */
    public void StartGame()
    {
        endgameUI.SetActive(false);
        gameplay.SetActive(false);
        gameplayUI.SetActive(false);
        gameUI.SetActive(true);
    }

    /*
    * Function to start the gameplay
    */
    public void StartGameplay()
    {
        endgameUI.SetActive(false);
        gameUI.SetActive(false);
        gameplay.SetActive(true);
        gameplayUI.SetActive(true);
    }
    
    /*
    * Function to stop the gameplay
    */
    public void StopGameplay()
    {
        gameUI.SetActive(false);
        gameplay.SetActive(false);
        gameplayUI.SetActive(false);
        endgameUI.SetActive(true);
    }
}
