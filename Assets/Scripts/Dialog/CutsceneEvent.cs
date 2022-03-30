using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CutsceneEvent : DialogEvent
{
    [Header("Cutscene Objects")]
    [SerializeField] protected GameObject cutsceneUI;
    [SerializeField] protected Image dialogSprite;
    [SerializeField] protected GameObject nameBox;

    [SerializeField] protected Sprite[] spriteImages;

    protected void ShowNameBox()
    {
        nameBox.SetActive(true);
    }

    protected void HideNameBox()
    {
        nameBox.SetActive(false);
    }

    protected void ChangeSprite(int index)
    {
        dialogSprite.sprite = spriteImages[index];
    }

    protected void HideSprite()
    {
        dialogSprite.color = new Color(1, 1, 1, 0);
    }

    protected void ShowSprite()
    {
        dialogSprite.color = new Color(1, 1, 1, 1);
    }
}
