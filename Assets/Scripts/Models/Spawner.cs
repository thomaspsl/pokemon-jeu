using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Mouvement")]
    public float Speed = 5f;

    // Propriétés
    private Rigidbody2D Rigidbody { get; set; }

    private void Awake()
    {
        this.Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Vector2 direction = Random.insideUnitCircle.normalized;
        this.Rigidbody.linearVelocity = direction * this.Speed;
    }
}
