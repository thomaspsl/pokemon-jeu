using UnityEngine.UI;
using UnityEngine;
using System;

public class UiManager : MonoBehaviour
{
    private GameManager _gm;

    [Header("Ui Settings")]
    public Text score;
    public Text time;

    private void Awake()
    {
        _gm = GameManager.Instance;
    }

    void Update()
    {   
        score.text = $"Score: <color=#feae34>{_gm.ScoreManager.Score}</color>";
        time.text = $"{TimeSpan.FromSeconds(_gm.TimeManager.Remaining):mm\\:ss}";
    }

    // public void StartGame()
    // {
    //     SceneManager.LoadScene("GameScene");
    // }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
