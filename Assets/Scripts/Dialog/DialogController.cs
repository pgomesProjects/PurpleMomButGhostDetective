using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogController : MonoBehaviour
{
    private List<string> historyLog;

    [SerializeField] private GameObject dialogObject;
    [SerializeField] private GameObject historyObject;
    [SerializeField] private TextMeshProUGUI logText;

    public static DialogController main;

    [HideInInspector]
    public bool isDialogShown;
    [HideInInspector]
    public bool historyLogActive;
    [HideInInspector]
    public bool isControlButtonHovered;

    private void Awake()
    {
        main = this;
        historyLog = new List<string>();
    }

    private void Start()
    {
        isDialogShown = true;
        historyLogActive = false;
        isControlButtonHovered = false;
    }

    public void ClearLog()
    {
        //Wipe out the history log
        historyLog.Clear();
    }

    public void AddToLog(string message)
    {
        //Add a line to the history log
        historyLog.Add(message);
    }

    private void DisplayLog()
    {
        //Display all of the lines that are in the history log
        logText.text = "";
        foreach(var i in historyLog)
        {
            logText.text += i;
        }
    }

    public void ToggleDialog()
    {
        //Hides or unhides the dialog, depending on which state it's in
        isDialogShown = !isDialogShown;
        dialogObject.SetActive(isDialogShown);
    }

    public void ShowHistoryLog()
    {
        //Mouse click SFX
        if (FindObjectOfType<AudioManager>() != null)
            FindObjectOfType<AudioManager>().Play("MouseClick", PlayerPrefs.GetFloat("SFXVolume", 0.5f));

        historyObject.SetActive(true);
        historyLogActive = true;
        DisplayLog();
    }

    public void HideHistoryLog()
    {
        historyObject.SetActive(false);
        historyLogActive = false;

        //If the text is complete through skip or auto, make it advance
        if (GameManager.instance.isCutsceneActive && FindObjectOfType<CutsceneDialogHandler>().textCompleted)
        {
            FindObjectOfType<CutsceneDialogHandler>().OnTextComplete();
        }
        else
        {
            //Mouse click SFX
            if (FindObjectOfType<AudioManager>() != null)
                FindObjectOfType<AudioManager>().Play("MouseClick", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
        }
    }

    public void ToggleSkip()
    {
        //Mouse click SFX
        if (FindObjectOfType<AudioManager>() != null)
            FindObjectOfType<AudioManager>().Play("MouseClick", PlayerPrefs.GetFloat("SFXVolume", 0.5f));

        CutsceneController.main.isSkipping = !CutsceneController.main.isSkipping;

        //If the cutscene is now skipping, increase the text speed, any UI animation speeds, and auto advance
        if (CutsceneController.main.isSkipping)
        {
            CutsceneController.main.currentTextSpeed = CutsceneController.main.textSpeed * CutsceneController.main.skipSpeedMultiplier;
            CutsceneController.main.spriteAnimator.SetFloat("Multiplier", 10);
            CutsceneController.main.CheckForAdvance();
        }
        //If the scene is no longer skipping, set the text speed to normal and the UI animation speeds to normal
        else
        {
            CutsceneController.main.currentTextSpeed = CutsceneController.main.textSpeed;
            CutsceneController.main.spriteAnimator.SetFloat("Multiplier", 1);
            FindObjectOfType<CutsceneDialogHandler>().CheckForceSkip();
        }
    }

    public void ToggleAuto()
    {
        //Mouse click SFX
        if (FindObjectOfType<AudioManager>() != null)
            FindObjectOfType<AudioManager>().Play("MouseClick", PlayerPrefs.GetFloat("SFXVolume", 0.5f));

        //Toggle the is auto variable. No need to do anything else here since it will start on its own in the dialog controller
        CutsceneController.main.isAuto = !CutsceneController.main.isAuto;
    }
}
