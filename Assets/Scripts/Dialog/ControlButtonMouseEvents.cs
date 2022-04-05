using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ControlButtonMouseEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI buttonText;
    private Color defaultTextColor;
    [SerializeField] private Color hoverTextColor = new Color(1, 1, 1, 1);

    private void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        defaultTextColor = buttonText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DialogController.main.isControlButtonHovered = true;
        buttonText.color = hoverTextColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DialogController.main.isControlButtonHovered = false;
        buttonText.color = defaultTextColor;
    }
}
