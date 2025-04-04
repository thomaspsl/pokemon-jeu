using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class PokemonManager : MonoBehaviour
{
    public Transform spawner;
    public Transform container;
    public float spawnDelay = 2f;
    public List<Pokemon> pokemonList = new List<Pokemon>();

    private List<Pokemon> _pokemons = new List<Pokemon>();
    private Coroutine _spawnRoutine;
    public event Action<Pokemon> OnCollected;

    public void StartSpawning()
    {
        _spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        if (_spawnRoutine == null) return;
        StopCoroutine(_spawnRoutine);
        _spawnRoutine = null;
    }

    private void Spawn()
    {
        // var data = pokemonList[UnityEngine.Random.Range(0, pokemonList.Count)];
        // var pokemon = Instantiate(data, spawner.position, Quaternion.identity);
        // pokemon.transform.parent = container;
        // pokemon.Data = data;
        // AddPokemon(pokemon);
    }

    private IEnumerator SpawnRoutine()
    {
        Spawn();
        yield return new WaitForSeconds(spawnDelay);
        StartSpawning();
    }

    private void AddPokemon(Pokemon pokemon)
    {
        pokemon.OnCollected += PokemonCollectedHandler;
        _pokemons.Add(pokemon);
    }

    private void RemovePokemon(Pokemon pokemon)
    {
        pokemon.OnCollected -= PokemonCollectedHandler;
        _pokemons.Remove(pokemon);
        Destroy(pokemon.gameObject);
    }

    private void PokemonCollectedHandler(Pokemon pokemon)
    {
        OnCollected?.Invoke(pokemon);
        RemovePokemon(pokemon);
    }

    // -- 

    public void StartGame()
    {
        StartSpawning();
    }

    public void StopGame()
    {
        StopSpawning();

        for (int i = _pokemons.Count - 1; i >= 0; i--)
        {
            RemovePokemon(_pokemons[i]);
        }
    }
}
