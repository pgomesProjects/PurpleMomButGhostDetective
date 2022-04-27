using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.Universal;

public class ExamineController : SelectableObject, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Light2D highlight;
    [SerializeField] private DialogEvent examineCutscene;

    // Start is called before the first frame update
    void Start()
    {
        highlight = transform.Find("Highlight").GetComponent<Light2D>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!GameManager.instance.isCutsceneActive)
        {
            if (GameManager.instance.playerHasInventory)
            {
                //Play click SFX
                if (FindObjectOfType<AudioManager>() != null)
                    FindObjectOfType<AudioManager>().Play("MouseClick", PlayerPrefs.GetFloat("SFXVolume", 0.5f));

                //Start cutscene with a tiny delay so that it's not called in the same frame
                //This gives some of the code some time to freeze the player
                StartCoroutine(StartCutsceneDelay(0.1f));
            }
        }
    }

    IEnumerator StartCutsceneDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (examineCutscene != null)
        {
            CutsceneController.main.dialogEvent = examineCutscene;
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
    }

    private void Update()
    {
        if (!GameManager.instance.isCutsceneActive && !GameManager.instance.isInventoryActive && !PlayerController.main.isMoving)
        {
            if (GameManager.instance.playerHasInventory)
            {
                highlight.gameObject.SetActive(isHighlighted);
            }
        }
    }
}
