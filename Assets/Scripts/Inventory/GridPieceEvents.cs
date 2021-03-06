using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GridPieceEvents : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Color gridColor;
    [SerializeField] private Color gridHoverColor;
    private Image gridImage;
    public Image gridItemImage;
    private Vector3 originalPosition;
    private bool isSelected;
    private GameObject parentUI;
    private int siblingIndex;

    private RectTransform itemImageTransform;

    private int inventoryImageID = -1;
    internal bool showTooltip;

    private void Start()
    {
        gridImage = GetComponent<Image>();
        gridItemImage = transform.Find("ItemImage").GetComponent<Image>();
        parentUI = transform.parent.gameObject;
        isSelected = false;
        gridColor = gridImage.color;
        itemImageTransform = gridItemImage.GetComponent<RectTransform>();
        originalPosition = itemImageTransform.position;
        siblingIndex = gridImage.transform.GetSiblingIndex();
    }

    private void Update()
    {
        //While a grid piece is selected, hide the grid background and have the item image follow the mouse
        if (isSelected)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            itemImageTransform.position = new Vector3(mousePos.x, mousePos.y, itemImageTransform.position.z);
            if (!InventoryController.main.isHidden)
                gridImage.color = gridColor;
            else
                gridImage.color = new Color(0, 0, 0, 0);

            gridItemImage.GetComponentInChildren<CanvasGroup>().alpha = 0;
        }
        else
        {
            //If there's an image, show their tooltip when hovering
            if (showTooltip)
                gridItemImage.GetComponentInChildren<CanvasGroup>().alpha = 1;
            else
                gridItemImage.GetComponentInChildren<CanvasGroup>().alpha = 0;
        }
    }

    public void RefreshItemImage()
    {
        gridItemImage = transform.Find("ItemImage").GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gridImage.color = gridHoverColor;
        //If there's an image, show their tooltip
        if (gridItemImage.color.a != 0)
            showTooltip = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gridImage.color = gridColor;
        //If there's an image, hide their tooltip
        if (gridItemImage.color.a != 0)
            showTooltip = false;
    }

    public void ForceUnhover()
    {
        gridImage.color = gridColor;
        //If there's an image, hide their tooltip
        if (gridItemImage.color.a != 0)
            showTooltip = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //If there's an image visible in the grid and the player has selected the image, drag the image
        if (gridItemImage.color.a != 0)
        {
            //Mouse click SFX
            if (FindObjectOfType<AudioManager>() != null)
                FindObjectOfType<AudioManager>().Play("MouseClick", PlayerPrefs.GetFloat("SFXVolume", 0.5f));

            isSelected = true;
            InventoryController.main.isDragging = isSelected;
            gridImage.transform.SetSiblingIndex(1);
            InventoryController.main.activeInventoryID = inventoryImageID;
            InventoryController.main.activeSiblingIndex = 1;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isSelected)
        {
            //Check for successful interaction
            InventoryController.main.CheckSuccessfulInteraction();
            RefreshItemImage();
            InventoryController.main.activeInventoryID = -1;
        }
        isSelected = false;
        InventoryController.main.isDragging = isSelected;
        //If the inventory is hidden, put the grid piece back in its parent and set the right color
        if (InventoryController.main.isHidden)
        {
            gridImage.transform.SetParent(parentUI.transform);
            gridImage.color = gridColor;
            InventoryController.main.isHidden = false;
        }
        gridImage.transform.SetSiblingIndex(siblingIndex);
        InventoryController.main.activeSiblingIndex = -1;
        itemImageTransform.position = originalPosition;
    }

    public int GetInventoryID() { return inventoryImageID; }
    public void SetInventoryID(int id) { inventoryImageID = id; }
}
