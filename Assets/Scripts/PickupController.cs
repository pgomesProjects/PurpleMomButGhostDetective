using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.Universal;

public class PickupController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Item Data")]
    public int ID = -1;
    public string itemName = "Test Item";
    public int[] combineItemIDs;

    private Item itemData;
    private Light2D highlight;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        highlight = transform.Find("Highlight").GetComponent<Light2D>();
        player = FindObjectOfType<PlayerController>();
        itemData = new Item(ID, itemName, 1, GetComponent<SpriteRenderer>(), combineItemIDs);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(itemData.name + " Collected!");
        player.AddToInventory(itemData);
        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.gameObject.SetActive(false);
    }
}
