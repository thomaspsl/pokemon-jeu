using System.Collections;
using UnityEngine;

public class Pokeball : MonoBehaviour
{
    public Color capturedColor = Color.gray;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        if (other.gameObject.CompareTag("Pokemon"))
        {
            StartCoroutine(CaptureAnimation(gameObject, other));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator CaptureAnimation(GameObject pokeball, Collision2D other)
    {
        GameObject pokemon = other.gameObject;
        Rigidbody2D pokemonRb = pokemon.GetComponent<Rigidbody2D>();
        if (pokemonRb != null) pokemonRb.bodyType = RigidbodyType2D.Static;

        // Déplace la pokeball sur le Pokémon
        transform.position = pokemon.transform.position;

        // Génère une proba entre 1 et 3 (pondérée)
        int tries = GenerateWeightedTry();

        for (int i = 0; i < tries; i++)
        {
            pokeball.GetComponent<CapsuleCollider2D>().enabled = false;
            pokemon.GetComponent<SpriteRenderer>().enabled = false;
            yield return StartCoroutine(Shake());
        }

        if (tries == 3)
        {
            Destroy(pokemon);
            pokeball.GetComponent<CapsuleCollider2D>().enabled = true;
            pokeball.GetComponent<SpriteRenderer>().color = capturedColor;
            Debug.Log("Pokémon capturé !");
        }
        else
        {
            Debug.Log("Le Pokémon s’est échappé !");
            Destroy(pokeball);
            pokemon.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private IEnumerator Shake()
    {
        Vector3 start = transform.position;
        float duration = 0.2f;
        float elapsed = 0f;
        float shakeAmount = 0.2f;

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

    private int GenerateWeightedTry()
    {
        float r = Random.value;
        if (r < 0.25f) return 1;         // 25% chance
        else if (r < 0.50f) return 2;   // 25% chance
        else return 3;                 // 50% chance
    }
}
