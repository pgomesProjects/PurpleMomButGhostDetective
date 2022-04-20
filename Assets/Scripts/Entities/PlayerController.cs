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

    [SerializeField] private Animator charAnimator;
    private bool isLeft;

    [HideInInspector]
    public bool canMove;

    public static PlayerController main;

    //The playerControls variable is an example of using Unity's new input system
    private void Awake()
    {
        main = this;
        playerControls = new PlayerControlSystem();
        playerControls.Player.Click.performed += _ => canMove = true;
        playerControls.Player.Click.canceled += _ => canMove = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        isLeft = true;
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
        //If the player can move, their inventory isn't active, and there's no active cutscene, check for movement input
        if (canMove && !GameManager.instance.isInventoryActive && !GameManager.instance.isCutsceneActive)
            GetPlayerMovementInput();

        //Always check to see if the player can be animation
        CheckPlayerAnimation();
    }

    private void FixedUpdate()
    {
        //The actual movement is called here because it performs nicer via FixedUpdate
        if (canMove && !GameManager.instance.isInventoryActive && !GameManager.instance.isCutsceneActive)
            MovePlayer();
    }

    private void GetPlayerMovementInput()
    {
        //Get the mouse position on the player's window
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.nearClipPlane;

        /*
         * A neat function to convert the mouse position to the game's window
         * The top left corner of the screen is (0,0) and the bottom right corner is (1, 1)
         * For this purpose, the x position is what's important
         */
        Vector3 screenPos = Camera.main.ScreenToViewportPoint(mousePos);

        //If the mouse is to the left of the player, move the player to the left using the following algorithm
        if (screenPos.x < 0.5f)
        {
            movement.x = -(2 * (1 - screenPos.x) - 1);
            if (movement.x < -1)
                movement.x = -1;
        }
        //If the mouse is to the right of the player, move the player to the right using the following algorithm
        else
        {
            movement.x = (screenPos.x - 0.5f) * 2;
            if (movement.x > 1)
                movement.x = 1;
        }
    }

    private void MovePlayer()
    {
        //This code works, but the player will not collide with anything, so this is commented out
        //transform.position += movement * speed * Time.deltaTime;

        //Move the player using rigidbody velocity, or else they will not collide into anything
        //This is true for anything 3D when it comes to 3D rigidbody objects
        GetComponent<Rigidbody>().velocity = movement * speed;
    }

    private void CheckPlayerAnimation()
    {
        //Animate the sprite when the game is not paused
        if(PauseController.main != null && !PauseController.main.isPaused && !GameManager.instance.isInventoryActive)
        {
            //If the player is not moving and not in a cutscene, set the multiplier to 1
            if (!GameManager.instance.isCutsceneActive)
            {
                if (!canMove)
                {
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    charAnimator.SetFloat("SpeedX", -1);
                }
                else
                {
                    //Set the speed and multiplier of the player animator
                    charAnimator.SetFloat("SpeedX", Mathf.Abs(movement.x));
                    charAnimator.SetFloat("WalkMultiplier", Mathf.Abs(movement.x));
                }
            }

            //If the player is switching from right to left, make sure they're looking left
            if (movement.x < 0 && !isLeft)
            {
                isLeft = true;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            //If the player is switching from left to right, flip the sprite, since it will show the player looking / moving right
            else if (movement.x > 0 && isLeft)
            {
                isLeft = false;
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    public void PlayStepSFX01()
    {
        //Left step SFX (this function is called in the player's animator)
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("StepLeft", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
        }
    }

    public void PlayStepSFX02()
    {
        //Right step SFX (this function is called in the player's animator)
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("StepRight", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
        }
    }

    public List<Item> GetInventory() { return inventory; }

    public void AddToInventory(Item itemData)
    {
        //If the item is not the notebook and the player has their inventory
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
            InventoryController.main.ShowPickupText(itemData.name, 1.05f);
        }
        //If the player does not have their inventory
        else if(!GameManager.instance.playerHasInventory)
        {
            //If the player collected the notebook, do not add it to the inventory, but let them access the inventory
            GameManager.instance.playerHasInventory = true;
            //Show the pickup text UI
            InventoryController.main.ShowPickupText(itemData.name, 5);
        }
    }

    public void RemoveFromInventory(int itemID)
    {
        bool deleteItem = true;
        int counter = 0;
        foreach (var i in inventory)
        {
            //If the item trying to be removed has been found, stop looking for it with the break statement
            if (i.ID == itemID)
            {
                //If there's more than one of the item, just reduce the quantity
                if(i.quantity > 1)
                {
                    i.quantity--;
                    deleteItem = false;
                }
                //Forcefully exits the foreach loop
                break;
            }
            counter++;
        }

        //If the item needs to be deleted from the inventory, remove it from the inventory list
        if (deleteItem)
        {
            inventory.RemoveAt(counter);
        }
    }

    public Animator GetCharacterAnimator() { return charAnimator; }
}
