using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    private GameManager _gm;

    [Header("Time Settings")]
    public float duration = 60f;

    // Propriétés
    public float Remaining { get; private set; }
    public bool Running { get; private set; }

    // Événements
    public event Action OnTimeUp;

    public void Update()
    {
        if(Running)
        {
            Tick(Time.deltaTime);
        }
    }

    private void Tick(float deltaTime)
    {
        Remaining -= deltaTime;

        if (Remaining <= 0f)
        {
            Remaining = 0f;
            OnTimeUp?.Invoke();
        }
    }

    public void StartGame()
    {
        Remaining = duration;
        Running = true;
    }
}
