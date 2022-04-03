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
        if (InventoryController.main.isDragging)
        {
            Debug.Log("Item Dragged Out Of Inventory!");
            inventoryUI.SetActive(false);
            InventoryController.main.isHidden = true;
            Transform currentGridPiece = inventoryUI.transform.GetChild(InventoryController.main.activeSiblingIndex);
            currentGridPiece.SetParent(HUD.gameObject.transform);
        }
    }
}
