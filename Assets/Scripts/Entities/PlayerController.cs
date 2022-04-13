using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    [SerializeField] private List<Item> inventory;
    

    private PlayerControlSystem playerControls;
    private Vector3 movement;
    [HideInInspector]
    public bool canMove;

    private void Awake()
    {
        playerControls = new PlayerControlSystem();
        playerControls.Player.Click.performed += _ => canMove = true;
        playerControls.Player.Click.canceled += _ => canMove = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
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
        if (canMove && !GameManager.instance.isInventoryActive && !GameManager.instance.isCutsceneActive)
            GetPlayerMovementInput();
    }

    private void GetPlayerMovementInput()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 screenPos = Camera.main.ScreenToViewportPoint(mousePos);
        if (screenPos.x < 0.5f)
        {
            movement.x = -(2 * (1 - screenPos.x) - 1);
            if (movement.x < -1)
                movement.x = -1;
        }
        else
        {
            movement.x = (screenPos.x - 0.5f) * 2;
            if (movement.x > 1)
                movement.x = 1;
        }

        transform.position += movement * speed * Time.deltaTime;
    }

    public List<Item> GetInventory() { return inventory; }

    public void AddToInventory(Item itemData)
    {
        if(itemData.name != "Notebook" && GameManager.instance.playerHasInventory)
        {
            bool duplicateItem = false;
            foreach (var i in inventory)
            {
                //If the player already has the item in their inventory, add to their quantity
                if (i.ID == itemData.ID)
                {
                    i.quantity++;
                    duplicateItem = true;
                    break;
                }
            }

            //If the current object is not a duplicate of an existing one, add a new item to the inventory
            if (!duplicateItem)
            {
                inventory.Add(itemData);
            }

            //Show the pickup text UI
            StartCoroutine(InventoryController.main.ShowPickupText(itemData.name, 1.05f));
        }
        else if(!GameManager.instance.playerHasInventory)
        {
            //If the player collected the notebook, do not add it to the inventory, but let them access the inventory
            GameManager.instance.playerHasInventory = true;
            //Show the pickup text UI
            StartCoroutine(InventoryController.main.ShowPickupText(itemData.name, 5));
        }
    }

    public void RemoveFromInventory(int itemID)
    {
        bool deleteItem = true;
        int counter = 0;
        foreach (var i in inventory)
        {
            if (i.ID == itemID)
            {
                if(i.quantity > 1)
                {
                    i.quantity--;
                    deleteItem = false;
                }
                break;
            }
            counter++;
        }

        if (deleteItem)
        {
            inventory.RemoveAt(counter);
        }
    }
}
