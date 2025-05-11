using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public float speed = 5f;

    // Propriétés
    private Rigidbody2D Rigidbody { get; set; }

    /*
    * Function to wake setup properties
    */
    private void Awake()
    {
        this.Rigidbody = GetComponent<Rigidbody2D>();
    }

    /*
    * Function to randomly move the spawner
    */
    public void StartGameplay()
    {
        Vector2 direction = Random.insideUnitCircle.normalized;
        this.Rigidbody.linearVelocity = direction * this.speed;
    }
}
