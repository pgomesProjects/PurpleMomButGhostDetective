using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.Universal;

public class DoorController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool isUnlocked;

    private Light2D highlight;
    private bool isSelected;
    [SerializeField] private DialogEvent doorCutscene;

    // Start is called before the first frame update
    void Start()
    {
        highlight = transform.Find("Highlight").GetComponent<Light2D>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!GameManager.instance.isCutsceneActive && isSelected)
        {
            //Play click SFX
            if (FindObjectOfType<AudioManager>() != null)
                FindObjectOfType<AudioManager>().Play("MouseClick", PlayerPrefs.GetFloat("SFXVolume", 0.5f));

            if (isUnlocked)
            {
                if (MorgueManager.main.readyForNextLevel)
                {
                    StartCoroutine(StartCutsceneDelay(0.1f));
                }
                else
                {
                    PopupController.main.DisplayPopup("<i>There Are Still Items You Must Investigate Here.</i>", 0.25f, 0.25f, 3);
                }
            }
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
        if (!GameManager.instance.isCutsceneActive)
        {
            isSelected = true;
            highlight.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isSelected = false;
        highlight.gameObject.SetActive(false);
    }

    public void UnlockDoor()
    {
        if (!isUnlocked)
        {
            if(FindObjectOfType<AudioManager>() != null)
                FindObjectOfType<AudioManager>().Play("DoorUnlockSFX", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
            isUnlocked = true;
        }
    }
}
