using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Slot
{
    public GameObject root;
    public Image background;
    public Image pokeballImage;
    public Text countText;

    /*
    * Constructor
    */
    public Slot(GameObject slotObject)
    {
        this.root = slotObject;
        this.background = slotObject.GetComponent<Image>();
        this.pokeballImage = slotObject.transform.Find("Image").GetComponent<Image>();
        this.countText = slotObject.transform.Find("Count").GetComponent<Text>();
    }

    /*
    * Function to set the 
    */
    public void SetSprite(Sprite sprite)
    {
        this.pokeballImage.sprite = sprite;
    }

    /*
    * Function to change the state of current pokeball selected
    */
    public void UpdateDisplay(bool selected, int count)
    {
        this.background.color = selected ? Color.gray * 1.5f : Color.white;
        this.root.transform.localScale = selected ? Vector3.one * 1.1f : Vector3.one;
        this.countText.text = $"x{count}";
    }
}