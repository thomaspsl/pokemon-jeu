using System.Collections;
using UnityEngine;

public class Pokeball : MonoBehaviour
{
    [Header("Pokeball Settings")]
    public float bonus = 0f;
    public float speed = 10f;

    // Propriétés
    public Rigidbody2D Rigidbody { get; private set; }

    private GameManager Game { get; set; }
    private Color capturedColor { get; set; } = Color.gray;
    private CapsuleCollider2D Collider { get; set; }
    private SpriteRenderer SpriteRenderer { get; set; }
    private AudioSource AudioSource { get; set; }

    /*
    * Function to wake setup properties
    */
    private void Awake()
    {
        this.Game = GameManager.Instance;
        this.Collider = GetComponent<CapsuleCollider2D>();
        this.SpriteRenderer = GetComponent<SpriteRenderer>();
        this.Rigidbody = GetComponent<Rigidbody2D>();
        this.AudioSource = GetComponent<AudioSource>();
    }

    /*
    * Function to check the collision with the pokemon
    */
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (this.Rigidbody != null) this.Rigidbody.linearVelocity = Vector2.zero;

        if (other.gameObject.CompareTag("Pokemon"))
        {
            Pokemon pokemon = other.gameObject.GetComponent<Pokemon>();

            if (pokemon.IsBeingCaptured)
            {
                this.Game.PokeballManager.RemovePokeball(this);
                return;
            }

            pokemon.IsBeingCaptured = true;
            StartCoroutine(CaptureAnimation(pokemon));
        }
        else
        {
            this.Game.PokeballManager.RemovePokeball(this);
        }
    }

    /*
    * Function to create the capture animation
    */
    private IEnumerator CaptureAnimation(Pokemon pokemon)
    {
        transform.position = pokemon.transform.position;
        pokemon.SpriteRenderer.enabled = false;

        if (pokemon.Rigidbody != null) pokemon.Rigidbody.bodyType = RigidbodyType2D.Static;
        if (pokemon.Collider != null) pokemon.Collider.enabled = false;

        Collider.enabled = false;

        int roll = Random.Range(1, 101);
        float totalLuck = pokemon.luck + this.bonus;
        int shakes = CalculateShakes(roll, totalLuck);

        if (shakes == 0)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            for (int i = 0; i < shakes; i++)
            {
                AudioManager.Play(this.AudioSource, this.Game.PokeballManager.shakeSound);
                yield return StartCoroutine(Shake());
            }
        }

        if (roll <= totalLuck)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            this.SpriteRenderer.color = this.capturedColor;
            AudioManager.Play(this.AudioSource, this.Game.PokeballManager.catchSound);
            pokemon.Collect();
            yield return StartCoroutine(FadeOutAndDestroy());
        }
        else
        {
            AudioManager.Play(this.AudioSource, this.Game.PokeballManager.outSound);
            
            if (this.SpriteRenderer != null) this.SpriteRenderer.enabled = false;
            if (this.Collider != null) this.Collider.enabled = false;
            if (this.Rigidbody != null)
            {
                this.Rigidbody.linearVelocity = Vector2.zero;
                this.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
            }

            pokemon.StartMovement();

            yield return new WaitForSeconds(this.Game.PokeballManager.outSound.length);
            this.Game.PokeballManager.RemovePokeball(this);
        }
    }

    /*
    * Function to calculate the number of shakes
    */
    private int CalculateShakes(int roll, float totalLuck)
    {
        if (roll <= totalLuck) return 3;
        else if (roll <= totalLuck + 20) return 2;
        else if (roll <= totalLuck + 40) return 1;
        else return 0;
    }

    /*
    * Function to skake the pokeball
    */
    private IEnumerator Shake()
    {
        Vector3 startPosition = transform.position;

        float duration = 0.2f;
        float elapsed = 0f;
        float shakeAmount = 0.1f;

        while (elapsed < duration)
        {
            float offset = Mathf.Sin(elapsed * 30f) * shakeAmount;
            transform.position = startPosition + new Vector3(offset, 0, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = startPosition;
        yield return new WaitForSeconds(0.3f);
    }

    /*
    * Function to fade out the pokeball and destroy it
    */
    private IEnumerator FadeOutAndDestroy()
    {
        yield return new WaitForSeconds(1f);

        Color originalColor = this.SpriteRenderer.color;
        float duration = 1.0f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            this.SpriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        this.Game.PokeballManager.RemovePokeball(this);
    }
}
