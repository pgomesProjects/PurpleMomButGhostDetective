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
        if (canMove && !GameManager.instance.isInventoryActive && !GameManager.instance.isCutsceneActive)
            GetPlayerMovementInput();

        CheckPlayerAnimation();
    }

    private void FixedUpdate()
    {
        if (canMove && !GameManager.instance.isInventoryActive && !GameManager.instance.isCutsceneActive)
            MovePlayer();
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
    }

    private void MovePlayer()
    {
        //transform.position += movement * speed * Time.deltaTime;
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

            if (movement.x < 0 && !isLeft)
            {
                isLeft = true;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (movement.x > 0 && isLeft)
            {
                isLeft = false;
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    public void PlayStepSFX01()
    {
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("StepLeft", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
        }
    }

    public void PlayStepSFX02()
    {
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("StepRight", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
        }
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
            InventoryController.main.ShowPickupText(itemData.name, 1.05f);
        }
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

    public Animator GetCharacterAnimator() { return charAnimator; }
}
