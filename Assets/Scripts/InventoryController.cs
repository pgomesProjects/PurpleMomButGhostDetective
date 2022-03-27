using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private PlayerControlSystem playerControls;
    [SerializeField] private GameObject inventoryUI;
    private bool isInventoryActive;

    private void Awake()
    {
        playerControls = new PlayerControlSystem();
        playerControls.Player.Inventory.performed += _ => ToggleInventory();
    }

    // Start is called before the first frame update
    void Start()
    {
        isInventoryActive = false;   
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
    }//end of ToggleInventory
}
