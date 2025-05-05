using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private GameManager _gm;

    // Propriétés
    public int Score { get; private set; } = 0;

    private void Awake()
    {
        _gm = GameManager.Instance;
        _gm.PokemonManager.OnCollected += PokemonCollectedHandler;
    }

    private void PokemonCollectedHandler(Pokemon pokemon)
    {
        Score += pokemon.Score;
    }

    private void OnDestroy()
    {
        _gm.PokemonManager.OnCollected -= PokemonCollectedHandler;
    }
}
