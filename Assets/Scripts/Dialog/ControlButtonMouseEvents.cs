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
    [SerializeField] private Color selectedTextColor = new Color(1, 1, 1, 1);

    [HideInInspector]
    public bool isHighlighted;

    private void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        defaultTextColor = buttonText.color;
        isHighlighted = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DialogController.main.isControlButtonHovered = true;

        if (!isHighlighted)
        {
            buttonText.color = hoverTextColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DialogController.main.isControlButtonHovered = false;
        if (!isHighlighted)
        {
            buttonText.color = defaultTextColor;
        }
    }

    public void ToggleHighlight()
    {
        isHighlighted = !isHighlighted;
        buttonText.color = selectedTextColor;
    }
}
