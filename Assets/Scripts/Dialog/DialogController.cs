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
    }

    private void Start()
    {
        historyLog = new List<string>();
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
    }
}