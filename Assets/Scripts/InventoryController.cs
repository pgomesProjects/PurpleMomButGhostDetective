using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    private PlayerControlSystem playerControls;
    [SerializeField] private GameObject inventoryUI;
    private bool isInventoryActive;
    private PlayerController player;
    [SerializeField] private Image[] imageGrid;

    private void Awake()
    {
        playerControls = new PlayerControlSystem();
        playerControls.Player.Inventory.performed += _ => ToggleInventory();
    }

    // Start is called before the first frame update
    void Start()
    {
        isInventoryActive = false;
        player = FindObjectOfType<PlayerController>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ToggleInventory()
    {
        isInventoryActive = !isInventoryActive;
        inventoryUI.SetActive(isInventoryActive);
        if (isInventoryActive)
            DisplayInventory();
    }//end of ToggleInventory

    private void DisplayInventory()
    {
        int counter = 0;
        foreach(var i in player.GetInventory())
        {
            imageGrid[counter].sprite = i.itemImage;
            imageGrid[counter].color = i.imageColor;
            counter++;
        }
    }
}
