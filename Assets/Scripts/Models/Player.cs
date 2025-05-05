using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public float Speed = 5f;
    public AudioClip ThrowSound;

    [Header("Pokeball Settings")]
    public Pokeball PokeballPrefab;
    public Transform container;

    // Propriétés
    public Rigidbody2D Rigidbody { get; private set; }
    private Animator Animator { get; set; }
    private AudioSource AudioSource { get; set; }
    private Vector2 Movement { get; set; }

    private void Awake()
    {
        this.Animator = GetComponent<Animator>();
        this.Rigidbody = GetComponent<Rigidbody2D>();
        this.AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        this.Movement = new Vector2(horizontal, vertical).normalized;

        this.Animator.SetFloat("Horizontal", horizontal);
        this.Animator.SetFloat("Vertical", vertical);
        this.Animator.SetFloat("Velocity", this.Movement.sqrMagnitude);

        if (Input.GetMouseButtonDown(0)) ThrowPokeball();
    }

    private void FixedUpdate()
    {
        this.Rigidbody.linearVelocity = this.Movement * this.Speed;
    }

    void ThrowPokeball()
    {
        if (this.PokeballPrefab == null) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector2 direction = (mousePosition - transform.position).normalized;
        Pokeball pokeball = Instantiate(this.PokeballPrefab, transform.position, Quaternion.identity);
        pokeball.transform.parent = container;
        pokeball.Rigidbody.linearVelocity = direction * pokeball.Speed;

        // 🔊 Joue le son
        if (this.ThrowSound != null && this.AudioSource != null)
        {
            this.AudioSource.PlayOneShot(this.ThrowSound);
        }
    }
}
