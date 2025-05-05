using UnityEngine;
using System;

public class Pokemon : MonoBehaviour
{
    [Header("Pokemon Settings")]
    public int Luck = 50;
    public float Speed = 5f;
    public int Score = 100;

    // Propriétés
    public bool IsBeingCaptured { get; set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public CapsuleCollider2D Collider { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    
    // Événements
    public event Action<Pokemon> OnCollected;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Collider = GetComponent<CapsuleCollider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        RestartMovement();
    }

    private void Update()
    {
        if (Animator != null && Rigidbody != null)
        {
            Vector2 velocity = Rigidbody.linearVelocity;
            Animator.SetFloat("Horizontal", velocity.normalized.x);
            Animator.SetFloat("Vertical", velocity.normalized.y);
            Animator.SetFloat("Velocity", velocity.magnitude);
        }
    }

    public void RestartMovement()
    {
        if (Rigidbody != null)
        {
            Rigidbody.bodyType = RigidbodyType2D.Dynamic;
            Rigidbody.linearVelocity = UnityEngine.Random.insideUnitCircle.normalized * Speed;
        }

        if (Collider != null) Collider.enabled = true;
        if (SpriteRenderer != null) SpriteRenderer.enabled = true;

        IsBeingCaptured = false;
    }

    public void Collect()
    {
        // Actions effectuées au moment de la capture
        if (SpriteRenderer != null) SpriteRenderer.enabled = false;
        if (Collider != null) Collider.enabled = false;
        if (Rigidbody != null) Rigidbody.bodyType = RigidbodyType2D.Static;

        OnCollected?.Invoke(this);
    }
}
