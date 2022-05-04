using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryController : MonoBehaviour
{
    private PlayerControlSystem playerControls;
    [SerializeField] private GameObject inventoryUI;
    private bool isInventoryActive;
    private IEnumerator displayPickupTextCoroutine;
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

        if (GameData.playerReference == null)
            Debug.LogError("ERROR: Player Object Does Not Exist!");
        else
            player = GameData.playerReference.GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void ToggleInventory()
    {
        //If there's no cutscene and the player has the inventory, toggle the inventory visibility
        if (GameManager.instance.playerHasInventory && !GameManager.instance.isCutsceneActive && !KeypadController.AnyKeypadActive())
        {
            isInventoryActive = !isInventoryActive;


            if (!isInventoryActive)
            {
                //Hide any active tooltips if closing inventory
                foreach (var i in FindObjectsOfType<GridPieceEvents>())
                {
                    //Make sure nothing is hovered
                    i.ForceUnhover();
                }
            }

            inventoryUI.SetActive(isInventoryActive);
            GameManager.instance.isInventoryActive = isInventoryActive;

            //Display all of the items in the inventory
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
            //If the player has the key in their inventory for the first time, display the tutorial
            if (i.name == "Door Keychain" && !GameManager.instance.tutorialsShown[(int)GameManager.Tutorial.USEINVENTORY])
            {
                GameManager.instance.tutorialsShown[(int)GameManager.Tutorial.USEINVENTORY] = true;
                PopupController.main.DisplayPopup("To Use An Item, Drag It Out Of The Inventory And Onto An Area You Would Like To Use It On.", 0.5f, 0.5f, 5);
            }

            imageGrid[counter].sprite = i.itemImage;
            imageGrid[counter].color = i.imageColor;
            imageGrid[counter].GetComponentInChildren<TextMeshProUGUI>().text = i.description;
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
            player.RemoveFromInventory(activeInventoryID);
            //Clear the inventory display so that it can be updated
            ClearInventoryDisplay();
            //Make sure the inventory is inactive
            isInventoryActive = false;
            hasSuccessfulInteraction = false;
        }

        inventoryUI.SetActive(isInventoryActive);
        GameManager.instance.isInventoryActive = isInventoryActive;

        //Create the inventory display again with the updated information
        if (isInventoryActive)
        {
            DisplayInventory();
        }
    }

    public void ShowPickupText(string item, float showSeconds)
    {
        //If there's already pickup text showing, stop it from showing
        if (displayPickupTextCoroutine != null)
        {
            StopCoroutine(displayPickupTextCoroutine);
        }
        //Update the coroutine with the new popup text and show the new popup
        displayPickupTextCoroutine = PickupTextAnimation(item, showSeconds);
        StartCoroutine(displayPickupTextCoroutine);
    }

    private IEnumerator PickupTextAnimation(string item, float showSeconds)
    {
        //Display a different message if the item picked up is the notebook
        if(item == "Notebook")
        {
            pickupText.text = "Picked Up Notebook.<br><size=40>You may now collect items. Press E to access your inventory.</size>";
        }
        else
        {
            pickupText.text = "Picked up " + item + ".";
        }
        pickupIndicatorObject.SetActive(true);
        yield return new WaitForSeconds(showSeconds);
        pickupIndicatorObject.SetActive(false);
    }
}
