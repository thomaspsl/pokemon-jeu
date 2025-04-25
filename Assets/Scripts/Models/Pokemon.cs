using UnityEngine;

public class Pokemon : MonoBehaviour
{
    [Header("Pokemon")]
    public int Luck = 50;
    public float Speed = 5f;
    public int Score = 100;

    // Propriétés
    public bool IsBeingCaptured { get; set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public CapsuleCollider2D Collider { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }

    private void Awake()
    {
        this.SpriteRenderer = GetComponent<SpriteRenderer>();
        this.Collider = GetComponent<CapsuleCollider2D>();
        this.Rigidbody = GetComponent<Rigidbody2D>();
        this.Animator = GetComponent<Animator>();
    }

    void Start()
    {
        RestartMovement();
    }

    private void Update()
    {
        if (this.Animator != null && this.Rigidbody != null)
        {
            Vector2 velocity = this.Rigidbody.linearVelocity;

            Animator.SetFloat("Horizontal", velocity.normalized.x);
            Animator.SetFloat("Vertical", velocity.normalized.y);
            Animator.SetFloat("Velocity", velocity.magnitude);
        }
    }

    public void RestartMovement()
    {
        if (this.Rigidbody != null)
        {
            Vector2 direction = Random.insideUnitCircle.normalized;
            this.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
            this.Rigidbody.linearVelocity = direction * this.Speed;
        }

        if (this.Collider != null) this.Collider.enabled = true;
        if (this.SpriteRenderer != null) this.SpriteRenderer.enabled = true;

        this.IsBeingCaptured = false;
    }
}
