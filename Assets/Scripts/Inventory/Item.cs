using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public int ID;
    public string name;
    public int quantity;
    public Sprite itemImage;
    public Color imageColor;
    public int [] combineItemIDs;

    public Item(int itemID, string itemName, int itemQuantity, SpriteRenderer itemImg, int [] combineItems)
    {
        ID = itemID;
        name = itemName;
        quantity = itemQuantity;
        itemImage = itemImg.sprite;
        imageColor = itemImg.color;
        combineItemIDs = combineItems;
    }
}
