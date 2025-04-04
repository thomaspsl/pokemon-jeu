using UnityEngine;
using System;

public class Pokemon : MonoBehaviour
{
    public int luck;              // Chance d'être capturé
    public float speed;           // Vitesse de déplacement
    public int score;             // Score à la capture

    public event Action<Pokemon> OnCollected;
    private SpriteRenderer _spriteRenderer;
    private Pokemon _data;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public Pokemon Data
    {
        get => _data;
        set => _data = value;
    }
}
