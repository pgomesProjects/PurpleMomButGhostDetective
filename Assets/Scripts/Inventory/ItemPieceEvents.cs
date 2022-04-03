using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemPieceEvents : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private GridPieceEvents gridParent;

    private void Start()
    {
        gridParent = transform.parent.GetComponent<GridPieceEvents>();    
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        gridParent.OnPointerDown(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gridParent.OnPointerEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gridParent.OnPointerExit(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gridParent.OnPointerUp(eventData);
    }
}
