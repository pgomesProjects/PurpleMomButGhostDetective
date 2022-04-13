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
        historyLog.Clear();
    }

    public void AddToLog(string message)
    {
        historyLog.Add(message);
    }

    private void DisplayLog()
    {
        logText.text = "";
        foreach(var i in historyLog)
        {
            logText.text += i;
        }
    }

    public void ToggleDialog()
    {
        isDialogShown = !isDialogShown;
        dialogObject.SetActive(isDialogShown);
    }

    public void ShowHistoryLog()
    {
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
    }

    public void ToggleSkip()
    {
        CutsceneController.main.isSkipping = !CutsceneController.main.isSkipping;
        if (CutsceneController.main.isSkipping)
        {
            CutsceneController.main.currentTextSpeed = CutsceneController.main.textSpeed * CutsceneController.main.skipSpeedMultiplier;
            CutsceneController.main.spriteAnimator.SetFloat("Multiplier", 10);
            CutsceneController.main.CheckForAdvance();
        }
        else
        {
            CutsceneController.main.currentTextSpeed = CutsceneController.main.textSpeed;
            CutsceneController.main.spriteAnimator.SetFloat("Multiplier", 1);
            FindObjectOfType<CutsceneDialogHandler>().CheckForceSkip();
        }
    }

    public void ToggleAuto()
    {
        CutsceneController.main.isAuto = !CutsceneController.main.isAuto;
    }
}
