using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.Universal;

public class UseItemController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int requiredID;
    private PlayerControlSystem playerControls;
    private bool isSelected = false;
    private bool isUsed = false;
    private Light2D highlight;

    private void Awake()
    {
        playerControls = new PlayerControlSystem();
        playerControls.Player.UseItem.performed += _ => CheckUseItem();
    }

    private void Start()
    {
        highlight = transform.Find("Highlight").GetComponent<Light2D>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (InventoryController.main.isHidden && !isUsed)
        {
            isSelected = true;
            highlight.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (InventoryController.main.isHidden && !isUsed)
        {
            isSelected = false;
            highlight.gameObject.SetActive(false);
        }
    }

    private void CheckUseItem()
    {
        highlight.gameObject.SetActive(false);

        //If the used volume is hovered over, the right piece is selected, and the used volume is not used, use the item
        if (InventoryController.main.activeInventoryID == requiredID && isSelected && !isUsed)
        {
            Debug.Log("Current ID: " + InventoryController.main.activeInventoryID);
            Debug.Log("Item Successfully Used!");
            InventoryController.main.hasSuccessfulInteraction = true;
            isUsed = true;

            //If this item is a lock, call the appropriate key function
            if (gameObject.CompareTag("Lock")) 
            {
                GetComponent<KeyController>().UseKeyOnDoor();
            }
        }
    }
}
