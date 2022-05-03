using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.Universal;

public class KeypadObjectController : SelectableObject, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string keycode;
    internal bool hasAccess;
    [SerializeField] private DoorController door;
    
    private Light2D highlight;
    private GameObject keypadUI;
    internal bool isKeypadUp;

    // Start is called before the first frame update
    void Start()
    {
        highlight = transform.Find("Highlight").GetComponent<Light2D>();
        keypadUI = transform.Find("KeypadCanvas").Find("Keypad").gameObject;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!GameManager.instance.isCutsceneActive && isHighlighted && !hasAccess && !keypadUI.activeInHierarchy && !isKeypadUp)
        {
            //Play click SFX
            if (FindObjectOfType<AudioManager>() != null)
                FindObjectOfType<AudioManager>().Play("MouseClick", PlayerPrefs.GetFloat("SFXVolume", 0.5f));

            //Show the keypad UI
            ShowKeypadUI(true);

            //Force highlight to be false
            isHighlighted = false;
            highlight.gameObject.SetActive(isHighlighted);
        }
    }

    public void ShowKeypadUI(bool keypadUIActive)
    {
        keypadUI.SetActive(keypadUIActive);
        isKeypadUp = keypadUIActive;
    }

    public void UnlockDoor()
    {
        hasAccess = true;
        if (door != null)
            door.UnlockDoor();
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
        if (!GameManager.instance.isCutsceneActive && !GameManager.instance.isInventoryActive && !PlayerController.main.isMoving && !hasAccess && !isKeypadUp)
        {
            highlight.gameObject.SetActive(isHighlighted);
        }
    }

}
