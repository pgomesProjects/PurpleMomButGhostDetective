using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UseItemController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int requiredID;
    private PlayerControlSystem playerControls;
    private bool isSelected = false;
    private bool isUsed = false;

    private void Awake()
    {
        playerControls = new PlayerControlSystem();
        playerControls.Player.UseItem.performed += _ => CheckUseItem();
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
        isSelected = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isSelected = false;
    }

    private void CheckUseItem()
    {
        if (InventoryController.main.activeInventoryID == requiredID && isSelected && !isUsed)
        {
            Debug.Log("Current ID: " + InventoryController.main.activeInventoryID);
            Debug.Log("Item Successfully Used!");
            InventoryController.main.hasSuccessfulInteraction = true;
            isUsed = true;
        }
    }
}