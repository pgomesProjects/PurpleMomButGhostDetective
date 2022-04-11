using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryController : MonoBehaviour
{
    private PlayerControlSystem playerControls;
    private PlayerController playerController;
    [SerializeField] private GameObject inventoryUI;
    private bool isInventoryActive;
    private PlayerController player;
    [SerializeField] private Image[] imageGrid;
    [SerializeField] private GameObject pickupIndicatorObject;
    [SerializeField] private TextMeshProUGUI pickupText;

    [HideInInspector]
    public bool isDragging, isHidden, hasSuccessfulInteraction;
    [HideInInspector]
    public int activeInventoryID, activeSiblingIndex;

    public static InventoryController main;

    private void Awake()
    {
        main = this;
        playerControls = new PlayerControlSystem();
        playerControls.Player.Inventory.performed += _ => ToggleInventory();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        isDragging = false;
        isHidden = false;
        hasSuccessfulInteraction = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        isInventoryActive = false;
        activeInventoryID = -1;
        activeSiblingIndex = -1;
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
        if (GameManager.instance.playerHasInventory && !GameManager.instance.isCutsceneActive)
        {
            isInventoryActive = !isInventoryActive;
            inventoryUI.SetActive(isInventoryActive);
            GameManager.instance.isInventoryActive = isInventoryActive;
            if (isInventoryActive)
            {
                DisplayInventory();
            }
            else
            {
                activeInventoryID = -1;
            }
        }
    }//end of ToggleInventory

    private void DisplayInventory()
    {
        int counter = 0;
        foreach(var i in player.GetInventory())
        {
            imageGrid[counter].sprite = i.itemImage;
            imageGrid[counter].color = i.imageColor;
            imageGrid[counter].gameObject.GetComponentInParent<GridPieceEvents>().SetInventoryID(i.ID);
            counter++;
        }
    }

    private void ClearInventoryDisplay()
    {
        foreach (var i in imageGrid)
        {
            i.sprite = null;
            i.color = new Color(0, 0, 0, 0);
        }
    }

    public void CheckSuccessfulInteraction()
    {
        if (hasSuccessfulInteraction)
        {
            //Get rid of item from inventory
            playerController.RemoveFromInventory(activeInventoryID);
            ClearInventoryDisplay();
            //Make sure the inventory is inactive
            isInventoryActive = false;
            hasSuccessfulInteraction = false;
        }

        inventoryUI.SetActive(isInventoryActive);
        GameManager.instance.isInventoryActive = isInventoryActive;
        if (isInventoryActive)
        {
            DisplayInventory();
        }
    }
    public IEnumerator ShowPickupText(string item)
    {
        if(item == "Notebook")
        {
            pickupText.text = "Picked Up Notebook.<br><size=40>You may now press E to access your inventory.</size>";
        }
        else
        {
            pickupText.text = "Picked up " + item + ".";
        }
        pickupIndicatorObject.SetActive(true);
        yield return new WaitForSeconds(2);
        pickupIndicatorObject.SetActive(false);
    }
}
