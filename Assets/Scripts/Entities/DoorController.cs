using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.Universal;

public class DoorController : SelectableObject, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool isUnlocked;

    private Light2D highlight;
    [SerializeField] private DialogEvent doorCutscene;

    // Start is called before the first frame update
    void Start()
    {
        highlight = transform.Find("Highlight").GetComponent<Light2D>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!GameManager.instance.isCutsceneActive && isHighlighted && !KeypadController.AnyKeypadActive())
        {
            //Play click SFX
            if (FindObjectOfType<AudioManager>() != null)
                FindObjectOfType<AudioManager>().Play("MouseClick", PlayerPrefs.GetFloat("SFXVolume", 0.5f));

            //If the door is unlocked
            if (isUnlocked)
            {
                //If the level is ready to advance, play the exit cutscene
                if (GameData.readyForNextLevel)
                {
                    StartCoroutine(StartCutsceneDelay(0.1f));
                }
                //If not, tell the player that they still need to investigate
                else
                {
                    PopupController.main.DisplayPopup("<i>There Are Still Items You Must Investigate Here.</i>", 0.25f, 0.25f, 3);
                }
            }
            //If the door is locked, tell the player that the door is locked with a pop up
            else
            {
                PopupController.main.DisplayPopup("<i>The Door Is Locked. Find A Way To Open It.</i>", 0.25f, 0.25f, 3);
            }
        }
    }

    IEnumerator StartCutsceneDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (doorCutscene != null)
        {
            CutsceneController.main.dialogEvent = doorCutscene;
            CutsceneController.main.TriggerDialogEvent();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHighlighted = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHighlighted = false;
        GameManager.instance.playerSelectingItem = false;
    }

    public void UnlockDoor()
    {
        //If the door is unlocked and the playeer can unlock it, unlock it
        if (!isUnlocked)
        {
            //Unlock door SFX
            if(FindObjectOfType<AudioManager>() != null)
                FindObjectOfType<AudioManager>().Play("DoorUnlockSFX", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
            isUnlocked = true;

            //If there's a cutscene on this door, get rid of it
            if(GetComponent<ExamineController>() != null)
                Destroy(GetComponent<ExamineController>());
        }
    }

    private void Update()
    {
        //If there's no cutscene active and inventory open and the player isn't actively moving
        if (!GameManager.instance.isCutsceneActive && !GameManager.instance.isInventoryActive && !PlayerController.main.isMoving)
        {
            highlight.gameObject.SetActive(isHighlighted);
        }
    }
}
