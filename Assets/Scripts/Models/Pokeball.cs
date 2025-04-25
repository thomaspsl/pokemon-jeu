using System.Collections;
using UnityEngine;

public class Pokeball : MonoBehaviour
{
    [Header("Pokemon")]
    public Color capturedColor = Color.gray;

    // Propriétés
    private CapsuleCollider2D Collider { get; set; }
    private SpriteRenderer SpriteRenderer { get; set; }
    public Rigidbody2D Rigidbody { get; private set; }

    private void Awake()
    {
        this.Collider = GetComponent<CapsuleCollider2D>();
        this.SpriteRenderer = GetComponent<SpriteRenderer>();
        this.Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (this.Rigidbody != null) this.Rigidbody.linearVelocity = Vector2.zero;

        if (other.gameObject.CompareTag("Pokemon"))
        {
            Pokemon pokemon = other.gameObject.GetComponent<Pokemon>();

            // Déjà entrain de se faire capturer
            if (pokemon.IsBeingCaptured) { 
                Destroy(gameObject); 
                return; 
            }

            pokemon.IsBeingCaptured = true;
            StartCoroutine(CaptureAnimation(pokemon));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator CaptureAnimation(Pokemon pokemon)
    {
        transform.position = pokemon.transform.position;
        pokemon.SpriteRenderer.enabled = false;
        if (pokemon.Rigidbody != null) pokemon.Rigidbody.bodyType = RigidbodyType2D.Static;
        if (pokemon.Collider != null) pokemon.Collider.enabled = false;

        int tries = GenerateWeightedTry();

        this.Collider.enabled = false;

        for (int i = 0; i < tries; i++)
        {
            yield return StartCoroutine(Shake());
        }

        if (tries == 3)
        {
            Destroy(pokemon.gameObject);
            gameObject.layer = LayerMask.NameToLayer("Default");
            this.SpriteRenderer.color = this.capturedColor;
            yield return StartCoroutine(FadeOutAndDestroy());
        }
        else
        {
            Destroy(gameObject);
            pokemon.RestartMovement();
        }
    }

    private IEnumerator Shake()
    {
        Vector3 start = transform.position;
        
        float duration = 0.2f;
        float elapsed = 0f;
        float shakeAmount = 0.1f;

        while (elapsed < duration)
        {
            float offset = Mathf.Sin(elapsed * 30f) * shakeAmount;
            transform.position = start + new Vector3(offset, 0, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = start;

        yield return new WaitForSeconds(0.3f);
    }

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

        this.SpriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        Destroy(gameObject);
    }

    private int GenerateWeightedTry()
    {
        float r = Random.value;
        if (r < 0.25f) return 1;
        else if (r < 0.50f) return 2;
        else return 3;
    }
}
