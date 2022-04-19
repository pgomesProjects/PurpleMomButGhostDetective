using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering.Universal;

public class ExamineController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
        if (!GameManager.instance.isCutsceneActive)
        {
            if (GameManager.instance.playerHasInventory)
            {
                highlight.gameObject.SetActive(true);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GameManager.instance.playerHasInventory)
        {
            highlight.gameObject.SetActive(false);
        }
    }
}
