using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    [Header("Time Settings")]
    public float duration = 60f;

    // Propriétés
    public float Remaining { get; private set; }
    public bool Running { get; private set; }

    // Événements
    public event Action OnTimeUp;

    /*
    * Update the rest of the time
    */
    public void Update()
    {
        if(this.Running) this.Tick(Time.deltaTime);
    }

    /*
    * Function all every second
    */
    private void Tick(float deltaTime)
    {
        this.Remaining -= deltaTime;

        if (this.Remaining <= 0f)
        {
            this.Remaining = 0f;
            this.OnTimeUp?.Invoke();
        }
    }

    /*
    * Function to start the gameplay
    */
    public void StartGameplay()
    {
        this.Running = true;
        this.Remaining = this.duration;
    }

    /*
    * Function to stop the gameplay
    */
    public void StopGameplay()
    {
        this.Running = false;
    }
}
