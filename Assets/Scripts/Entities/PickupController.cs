using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.Universal;

public class PickupController : SelectableObject, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
        if (!GameManager.instance.isCutsceneActive)
        {
            if (GameManager.instance.playerHasInventory || itemData.name == "Notebook")
            {
                //Play click SFX
                if (FindObjectOfType<AudioManager>() != null)
                    FindObjectOfType<AudioManager>().Play("MouseClick", PlayerPrefs.GetFloat("SFXVolume", 0.5f));

                Debug.Log(itemData.name + " Collected!");

                //Add item to player inventory and destroy the in-game object
                player.AddToInventory(itemData);

                if(GetComponent<CutsceneOnPickup>() != null)
                {
                    GetComponent<CutsceneOnPickup>().OnPickup();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHighlighted = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHighlighted = false;
    }

    private void Update()
    {
        if (!GameManager.instance.isCutsceneActive && !PlayerController.main.isMoving)
        {
            if (GameManager.instance.playerHasInventory || itemData.name == "Notebook")
            {
                highlight.gameObject.SetActive(isHighlighted);
            }
        }
    }
}
