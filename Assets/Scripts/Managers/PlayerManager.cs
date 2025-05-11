using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Settings")]
    public Player player;
    public Transform spawnPoint;

    [Header("Audio Settings")]
    public AudioClip throwSound;

    // Propriétés
    private AudioSource AudioSource { get; set; }
    private Animator Animator { get; set; }

    /*
    * Function to wake setup properties
    */
    public void Awake()
    {
        this.Animator = player.GetComponent<Animator>();
        this.AudioSource = player.GetComponent<AudioSource>();
    }

    /*
    * Function to reset the player position to specified spawn point
    */
    private void ResetPosition()
    {
        Vector3 targetPosition = this.spawnPoint.position;
        this.player.transform.position = targetPosition;
    }

    /*
    * Function to start the gameplay
    */
    public void StartGameplay()
    {
        this.ResetPosition();
        this.player.inventoryIndex = 0;
        this.player.inventoryItems = new List<InventoryItem>(
            this.player.inventoryItems_save.ConvertAll(item => new InventoryItem {
                prefab = item.prefab,
                icon = item.icon,
                count = item.count
            })
        );
    }
}
