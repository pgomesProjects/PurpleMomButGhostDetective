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
    private Image gridImage, gridItemImage;
    private Vector3 originalPosition;
    private bool isSelected;
    private int siblingIndex;

    private RectTransform itemImageTransform;

    private void Start()
    {
        gridImage = GetComponent<Image>();
        gridItemImage = transform.Find("ItemImage").GetComponent<Image>();
        isSelected = false;
        gridColor = gridImage.color;
        itemImageTransform = gridItemImage.GetComponent<RectTransform>();
        originalPosition = itemImageTransform.position;
        siblingIndex = gridImage.transform.GetSiblingIndex();
    }

    private void Update()
    {
        if (isSelected)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            itemImageTransform.position = new Vector3(mousePos.x, mousePos.y, itemImageTransform.position.z);
            gridImage.color = gridColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gridImage.color = gridHoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gridImage.color = gridColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isSelected = true;
        gridImage.transform.SetSiblingIndex(1);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isSelected = false;
        gridImage.transform.SetSiblingIndex(siblingIndex);
        itemImageTransform.position = originalPosition;
    }
}
