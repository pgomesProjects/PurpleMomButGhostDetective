using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryEvents : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject inventoryUI;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //If the player is dragging an item out of the inventory and successfully does so
        if (InventoryController.main.isDragging)
        {
            Debug.Log("Item Dragged Out Of Inventory!");
            //Temporarily hide the inventory so that the player can potentially use the item
            inventoryUI.SetActive(false);
            InventoryController.main.isHidden = true;

            //Move the selected grid piece out of the inventory UI so that it can be seen
            Transform currentGridPiece = inventoryUI.transform.GetChild(InventoryController.main.activeSiblingIndex);
            currentGridPiece.SetParent(HUD.gameObject.transform);
        }
    }
}
