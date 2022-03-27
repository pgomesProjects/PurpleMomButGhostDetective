using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public int ID;
    public string name;
    public int quantity;
    public Image itemImage;
    public int [] combineItemIDs;
}
