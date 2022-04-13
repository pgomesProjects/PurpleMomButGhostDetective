using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextWriter))]
public class CutsceneController : MonoBehaviour
{
    [HideInInspector]
    public TextWriter.TextWriterSingle textWriterSingle;
    private PlayerControlSystem playerControls;
    [HideInInspector]
    public DialogEvent dialogEvent;
    private bool isDialogActive;

    public bool playOnStart = false;

    public static CutsceneController main;

    public bool isSkipping = false;
    public bool isAuto = false;

    public float textSpeed = 30;
    public float skipSpeedMultiplier = 2.5f;
    [HideInInspector]
    public float currentTextSpeed;
    public Animator spriteAnimator;

    private void Awake()
    {
        main = this;
        isDialogActive = false;
        playerControls = new PlayerControlSystem();
        playerControls.UI.Click.performed += _ => {
            //If cutscene dialog is advanced, play mouse click SFX
            if (isDialogActive && !DialogController.main.isControlButtonHovered)
            {
                if (FindObjectOfType<AudioManager>() != null)
                    FindObjectOfType<AudioManager>().Play("MouseClick", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
            }

            //Advance text if they are not selecting a button
            if (!DialogController.main.isControlButtonHovered)
                AdvanceText();
        };
        playerControls.Player.ToggleDialogBox.performed += _ => DialogController.main.ToggleDialog();
    }

    public void AdvanceText()
    {
        //If the dialog is activated and not in the control / history menu
        if (isDialogActive && !DialogController.main.historyLogActive)
        {
            //If the dialog box is hidden, unhide it
            if (!DialogController.main.isDialogShown)
                DialogController.main.ToggleDialog();

            //If there is text being written already, write everything
            if (textWriterSingle != null && textWriterSingle.IsActive())
                textWriterSingle.WriteAllAndDestroy();

            //If there is no text and there are still lines left, check for events needed to display the text
            else if (dialogEvent.GetCurrentLine() < dialogEvent.GetDialogLength())
                dialogEvent.CheckEvents(ref textWriterSingle);

            //If all of the text has been shown, call the event for when the text is complete
            else
            {
                isDialogActive = false;
                GameManager.instance.isCutsceneActive = false;
                dialogEvent.OnEventComplete();
            }
        }
    }

    public IEnumerator ForceAdvance()
    {
        yield return new WaitForSeconds(0.1f);
        AdvanceText();
    }

    public IEnumerator AutoAdvance()
    {
        yield return new WaitForSeconds(0.5f);
        AdvanceText();
    }

    public void CheckForAdvance()
    {
        //If there is text being written already, write everything
        if (textWriterSingle != null && textWriterSingle.IsActive())
            textWriterSingle.WriteAllAndDestroy();

        StartCoroutine(ForceAdvance());
    }

    private void Start()
    {
        //If the dialog is meant to be played at the start, trigger it immediately
        if (playOnStart)
            TriggerDialogEvent();
    }

    public void TriggerDialogEvent()
    {
        //Start text event
        isDialogActive = true;
        GameManager.instance.isCutsceneActive = true;
        dialogEvent.OnDialogStart();
        dialogEvent.CheckEvents(ref textWriterSingle);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
