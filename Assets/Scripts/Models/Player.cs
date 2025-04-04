using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;

    private Animator _animator;
    private Vector2 _movement;
    private Rigidbody2D _rigidbody2D;

    [Header("Pokéball Settings")]
    public GameObject pokeballPrefab; // Référence au prefab de la Pokéball
    public float pokeballSpeed = 10f; // Vitesse de la Pokéball

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _movement = new Vector2(horizontal, vertical).normalized;

        _animator.SetFloat("Horizontal", horizontal);
        _animator.SetFloat("Vertical", vertical);
        _animator.SetFloat("Velocity", _movement.sqrMagnitude);

        if (Input.GetMouseButtonDown(0)) ThrowPokeball();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocity = _movement * speed;
    }

    void ThrowPokeball()
    {
        if (pokeballPrefab == null) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector2 direction = (mousePosition - transform.position).normalized;
        GameObject pokeball = Instantiate(pokeballPrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = pokeball.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = direction * pokeballSpeed;
    }
}
