using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public float speed = 5f;
    public AudioClip throwSound;

    [Header("Inventory Settings")]
    public Transform inventoryContainer;
    public GameObject inventorySlotPrefab;
    public List<InventoryItem> inventoryItems = new();
    public List<InventoryItem> inventoryItems_save;
    public int inventoryIndex = 0;

    private List<Slot> slotUIs = new();

    private Animator animator;
    private AudioSource audioSource;
    private Rigidbody2D rb;
    private Vector2 movement;

    private void Awake()
    {
        this.animator = GetComponent<Animator>();
        this.audioSource = GetComponent<AudioSource>();
        this.rb = GetComponent<Rigidbody2D>();
        this.inventoryItems_save = new List<InventoryItem>(
            this.inventoryItems.ConvertAll(item => new InventoryItem {
                prefab = item.prefab,
                icon = item.icon,
                count = item.count
            })
        );
    }

    private void Start()
    {
        CreateInventorySlots();
    }

    private void Update()
    {
        HandleMovementInput();
        HandlePokeballSelection();
        HandleInput();
        UpdateInventoryDisplay();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement * speed;
    }

    private void HandleMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2(horizontal, vertical).normalized;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Velocity", movement.sqrMagnitude);
    }

    private void HandlePokeballSelection()
    {
        if (inventoryItems.Count == 0) return;

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            inventoryIndex = (inventoryIndex + 1) % inventoryItems.Count;
        }
        else if (scroll < 0f)
        {
            inventoryIndex = (inventoryIndex - 1 + inventoryItems.Count) % inventoryItems.Count;
        }

        for (int i = 0; i < Mathf.Min(9, inventoryItems.Count); i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                inventoryIndex = i;
            }
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowPokeball();
        }
    }

    private void ThrowPokeball()
    {
        if (inventoryItems.Count == 0) return;

        var item = inventoryItems[inventoryIndex];
        if (item.count <= 0) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector2 direction = (mousePosition - transform.position).normalized;

        Pokeball pokeball = Instantiate(item.prefab, transform.position, Quaternion.identity);
        pokeball.Rigidbody.linearVelocity = direction * pokeball.speed;

        item.count--;

        audioSource.PlayOneShot(throwSound);
    }

    private void CreateInventorySlots()
    {
        foreach (var item in inventoryItems)
        {
            GameObject slotObject = Instantiate(inventorySlotPrefab, inventoryContainer);
            Slot slot = new Slot(slotObject);
            slot.SetSprite(item.icon);
            slotUIs.Add(slot);
        }

        UpdateInventoryDisplay();
    }

    private void UpdateInventoryDisplay()
    {
        for (int i = 0; i < slotUIs.Count; i++)
        {
            bool selected = (i == inventoryIndex);
            int count = inventoryItems[i].count;
            slotUIs[i].UpdateDisplay(selected, count);
        }
    }
}
