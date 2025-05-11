using System.Collections.Generic;
using UnityEngine;

public class PokeballManager : MonoBehaviour
{
    private List<Pokeball> pokeballs = new List<Pokeball>();

    [Header("Pokeballs Settings")]
    public Transform container;
    public List<Pokeball> pokeballList = new List<Pokeball>();

    [Header("Audio Settings")]
    public AudioClip shakeSound;
    public AudioClip catchSound;
    public AudioClip outSound;

    // Propriétés
    private GameManager Game { get; set; }

    /*
    * Function to wake managers 
    */
    private void Awake()
    {
        this.Game = GameManager.Instance;
    }

    /*
    * Function to add a pokeball to the list
    */
    public void AddPokeball(Pokeball pokeball)
    {
        pokeballs.Add(pokeball);
    }

    /*
    * Function to remove a pokeball from the list and destroy it
    */
    public void RemovePokeball(Pokeball pokeball)
    {
        this.pokeballs.Remove(pokeball);
        Destroy(pokeball.gameObject);
    }

    /*
    * Function to clear all pokeballs
    */
    public void ClearPokeballs()
    {
        foreach (var pokeball in this.pokeballs)
        {
            Destroy(pokeball.gameObject);
        }
        this.pokeballs.Clear();
    }

    /*
    * Function to start the gameplay
    */
    public void StartGameplay()
    {
        ClearPokeballs();
    }
}
