using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Propriétés
    public int Score { get; private set; } = 0;

    private GameManager Game { get; set; }

    /*
    * Function to wake game manager
    */
    private void Awake()
    {
        this.Game = GameManager.Instance;
        this.Game.PokemonManager.OnCollected += this.PokemonCollectedHandler;
    }

    /*
    * Function to increment the score on pokemon collection
    */
    private void PokemonCollectedHandler(Pokemon pokemon)
    {
        this.Score += pokemon.score;
    }

    /*
    * Function to clear the event created
    */
    private void OnDestroy()
    {
        this.Game.PokemonManager.OnCollected -= this.PokemonCollectedHandler;
    }

    /*
    * Function to clear the event created
    */
    public void StartGameplay()
    {
        this.Score = 0;
    }
}
