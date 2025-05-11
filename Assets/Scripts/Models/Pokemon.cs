using UnityEngine;
using System;

public class Pokemon : MonoBehaviour
{
    [Header("Pokemon Settings")]
    public int luck = 50;
    public float speed = 5f;
    public int score = 100;

    // Propriétés
    public bool IsBeingCaptured { get; set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public CapsuleCollider2D Collider { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    
    // Événements
    public event Action<Pokemon> OnCollected;

    /*
    * Function to wake setup properties
    */
    private void Awake()
    {
        this.SpriteRenderer = GetComponent<SpriteRenderer>();
        this.Collider = GetComponent<CapsuleCollider2D>();
        this.Rigidbody = GetComponent<Rigidbody2D>();
        this.Animator = GetComponent<Animator>();
    }

    /*
    * Function to start movement
    */
    private void Start()
    {
        this.StartMovement();
    }

    /*
    * Function to update the animator of the pokemon
    */
    private void Update()
    {
        Vector2 movement = Rigidbody.linearVelocity.normalized;

        this.Animator.SetFloat("Horizontal", movement.x);
        this.Animator.SetFloat("Vertical", movement.y);
        this.Animator.SetFloat("Velocity", movement.sqrMagnitude);
    }

    /*
    * Function to restart the movement of the pokemon
    */
    public void StartMovement()
    {
        if (this.Rigidbody != null)
        {
            this.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
            this.Rigidbody.linearVelocity = UnityEngine.Random.insideUnitCircle.normalized * this.speed;
        }

        if (this.Collider != null) this.Collider.enabled = true;
        if (this.SpriteRenderer != null) this.SpriteRenderer.enabled = true;

        this.IsBeingCaptured = false;
    }

    /*
    * Function to setup the collect state
    */
    public void Collect()
    {
        if (this.SpriteRenderer != null) this.SpriteRenderer.enabled = false;
        if (this.Collider != null) this.Collider.enabled = false;
        if (this.Rigidbody != null) this.Rigidbody.bodyType = RigidbodyType2D.Static;

        this.OnCollected?.Invoke(this);
    }
}
