using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class CutsceneEvent : DialogEvent
{
    [Header("Cutscene Objects")]
    [SerializeField][Tooltip("The container that holds all cutscene UI.")]
    protected GameObject cutsceneUI;
    [SerializeField][Tooltip("The object that displays the sprite.")]
    protected Image dialogSprite;
    [SerializeField][Tooltip("The object that displays the Name Box.")]
    protected GameObject nameBox;
    [SerializeField]
    [Tooltip("The text box for the name box.")]
    protected TextMeshProUGUI nameText;

    [SerializeField][Tooltip("The images for the sprites in the cutscene.\nNote: The cutscene loads the first sprite given on start.")]
    protected Sprite[] spriteImages;

    public void ShowNameBox()
    {
        nameBox.SetActive(true);
    }

    public void SetNameBoxText(string name)
    {
        nameText.text = name;
    }

    public void HideNameBox()
    {
        nameBox.SetActive(false);
    }

    public void ToggleDialogBox()
    {

    }

    public void ChangeSprite(int index)
    {
        dialogSprite.sprite = spriteImages[index];
    }

    public void HideSprite()
    {
        dialogSprite.color = new Color(1, 1, 1, 0);
    }

    public void ShowSprite()
    {
        dialogSprite.color = new Color(1, 1, 1, 1);
    }
}
