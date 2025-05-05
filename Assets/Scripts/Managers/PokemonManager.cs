using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class PokemonManager : MonoBehaviour
{
    private Coroutine _spawnRoutine;
    private List<Pokemon> _pokemons = new List<Pokemon>();

    [Header("Pokemons Settings")]
    public Transform spawner;
    public Transform container;
    public float spawnDelay = 1f;
    public List<Pokemon> pokemonList = new List<Pokemon>();

    // Événements
    public event Action<Pokemon> OnCollected;

    public void StartGame()
    {
        StartSpawning();
    }
    
    public void StartSpawning()
    {
        _spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        Spawn();
        yield return new WaitForSeconds(spawnDelay);
        StartSpawning();
    }

    private void Spawn()
    {
        var prefab = pokemonList[UnityEngine.Random.Range(0, pokemonList.Count)];
        var pokemon = Instantiate(prefab, spawner.position, Quaternion.identity);
        pokemon.transform.parent = container;
        AddPokemon(pokemon);
    }

    private void AddPokemon(Pokemon pokemon)
    {
        pokemon.OnCollected += PokemonCollectedHandler;
        _pokemons.Add(pokemon);
    }

    private void PokemonCollectedHandler(Pokemon pokemon)
    {
        OnCollected?.Invoke(pokemon);
        RemovePokemon(pokemon);
    }

    private void RemovePokemon(Pokemon pokemon)
    {
        pokemon.OnCollected -= PokemonCollectedHandler;
        _pokemons.Remove(pokemon);
        Destroy(pokemon.gameObject);
    }
}
