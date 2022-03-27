using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridPieceEvents : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Color gridColor;
    [SerializeField] private Color gridHoverColor;
    private Image gridImage;

    private void Start()
    {
        gridImage = GetComponent<Image>();
        gridColor = gridImage.color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gridImage.color = gridHoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gridImage.color = gridColor;
    }
}
