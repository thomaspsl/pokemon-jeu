using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class PokemonManager : MonoBehaviour
{
    private Coroutine spawnRoutine;
    private List<Pokemon> pokemons = new List<Pokemon>();

    [Header("Spawner Settings")]
    public Transform spawner;
    public float spawnDelay = 0.5f;

    [Header("Pokemon Settings")]
    public List<Pokemon> pokemonList = new List<Pokemon>();
    public Transform container;

    // Événements
    public event Action<Pokemon> OnCollected;

    /*
    * Function to start the gameplay
    */
    public void StartGameplay()
    {
        this.StartSpawning();
    }
    
    /*
    * Function to start spawning pokemons
    */
    public void StartSpawning()
    {
        this.spawnRoutine = StartCoroutine(this.SpawnRoutine());
    }

    /*
    * Routine to spawn pokemons
    */
    private IEnumerator SpawnRoutine()
    {
        this.Spawn();
        yield return new WaitForSeconds(spawnDelay);
        this.StartSpawning();
    }

    /*
    * Function to spawn a pokemon
    */
    private void Spawn()
    {
        var prefab = pokemonList[UnityEngine.Random.Range(0, pokemonList.Count)];
        var pokemon = Instantiate(prefab, spawner.position, Quaternion.identity);
        pokemon.transform.parent = container;
        AddPokemon(pokemon);
    }

    /*
    * Function to add a pokemon to the list
    */
    private void AddPokemon(Pokemon pokemon)
    {
        pokemon.OnCollected += this.PokemonCollectedHandler;
        this.pokemons.Add(pokemon);
    }

    /*
    * Function to handle the pokemon collection
    */
    private void PokemonCollectedHandler(Pokemon pokemon)
    {
        this.OnCollected?.Invoke(pokemon);
        RemovePokemon(pokemon);
    }

    /*
    * Function to remove a pokemon from the list
    */
    private void RemovePokemon(Pokemon pokemon)
    {
        pokemon.OnCollected -= this.PokemonCollectedHandler;
        this.pokemons.Remove(pokemon);
        Destroy(pokemon.gameObject);
    }

    /*
    * Function to stop the gameplay
    */
    public void StopGameplay()
    {
        StopCoroutine(this.spawnRoutine);
        this.spawnRoutine = null;

        foreach (var pokemon in this.pokemons)
        {
            pokemon.OnCollected -= this.PokemonCollectedHandler;
            Destroy(pokemon.gameObject);
        }
        this.pokemons.Clear();
    }
}
