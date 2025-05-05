using System.Collections;
using UnityEngine;

public class Pokeball : MonoBehaviour
{
    [Header("Pokeball Settings")]
    public float Speed { get; set; } = 10f;
    public AudioClip ShakeSound;
    public AudioClip CatchSound;
    public AudioClip OutSound;
    public Rigidbody2D Rigidbody { get; private set; }
    private Color capturedColor { get; set; } = Color.gray;
    private CapsuleCollider2D Collider { get; set; }
    private SpriteRenderer SpriteRenderer { get; set; }
    private AudioSource AudioSource { get; set; }

    private void Awake()
    {
        Collider = GetComponent<CapsuleCollider2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody = GetComponent<Rigidbody2D>();
        AudioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Rigidbody != null) Rigidbody.linearVelocity = Vector2.zero;

        if (other.gameObject.CompareTag("Pokemon"))
        {
            Pokemon pokemon = other.gameObject.GetComponent<Pokemon>();

            if (pokemon.IsBeingCaptured)
            {
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

        Collider.enabled = false;

        int roll = Random.Range(1, 101);
        int shakes = CalculateShakes(roll, pokemon.Luck);

        if (shakes == 0)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            for (int i = 0; i < shakes; i++)
            {
                PlaySound(ShakeSound); // Shake sound
                yield return StartCoroutine(Shake());
            }
        }

        if (roll <= pokemon.Luck)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            SpriteRenderer.color = capturedColor;
            PlaySound(CatchSound); // Capture réussie
            pokemon.Collect();
            yield return StartCoroutine(FadeOutAndDestroy());
        }
        else
        {
            PlaySound(OutSound); // Pokémon s’échappe après shakes

            // Désactivation visuelle et physique
            if (SpriteRenderer != null) SpriteRenderer.enabled = false;
            if (Collider != null) Collider.enabled = false;
            if (Rigidbody != null)
            {
                Rigidbody.linearVelocity = Vector2.zero;
                Rigidbody.bodyType = RigidbodyType2D.Kinematic; // empêche les interactions physiques
            }

            pokemon.RestartMovement();

            // Attend la fin du son
            yield return new WaitForSeconds(OutSound.length);

            Destroy(gameObject);
        }
    }

    private int CalculateShakes(int roll, int luck)
    {
        if (roll <= luck)
            return 3;
        else if (roll <= luck + 20)
            return 2;
        else if (roll <= luck + 40)
            return 1;
        else
            return 0;
    }

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

    private IEnumerator FadeOutAndDestroy()
    {
        yield return new WaitForSeconds(1f);

        Color originalColor = SpriteRenderer.color;
        float duration = 1.0f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            SpriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && AudioSource != null)
        {
            AudioSource.PlayOneShot(clip);
        }
    }
}
