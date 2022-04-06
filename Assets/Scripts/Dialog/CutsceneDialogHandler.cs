using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneDialogHandler : CutsceneEvent
{
    private CustomEvent cutsceneCustomEvents;
    [HideInInspector] public bool canAutoAdvance = false;
    [HideInInspector] public bool forceSkip = false;
    [HideInInspector] public bool textCompleted = false;
    [SerializeField] private float skipTextMultiplier = 1.5f;

    private void Awake()
    {
        cutsceneCustomEvents = GetComponent<CustomEvent>();
    }

    public override void OnDialogStart()
    {
        CutsceneController.main.currentTextSpeed = CutsceneController.main.textSpeed;
        cutsceneUI.SetActive(true);
        ChangeSprite(0);
        SetNameBoxText("Clementine");
    }

    public override void CheckEvents(ref TextWriter.TextWriterSingle textWriterObj)
    {
        string message = dialogLines[currentLine];
        textCompleted = false;

        //Check for custom events if present
        if (cutsceneCustomEvents != null)
            cutsceneCustomEvents.CheckForCustomEvent(currentLine);

        //Add to history log
        if (nameBox.activeInHierarchy)
            DialogController.main.AddToLog(nameText.text + ":<br>");
        DialogController.main.AddToLog(message + "<br><br>");
        continueObject.SetActive(false);

        if (CutsceneController.main.isSkipping)
            CutsceneController.main.currentTextSpeed = CutsceneController.main.textSpeed * CutsceneController.main.skipSpeedMultiplier;

        Debug.Log("Current Text Speed: " + CutsceneController.main.currentTextSpeed);
        textWriterObj = TextWriter.AddWriter_Static(null, messageText, message, 1 / CutsceneController.main.currentTextSpeed, true, true, OnTextComplete);
        currentLine++;
    }

    private void SkipText()
    {
        StartCoroutine(CutsceneController.main.ForceAdvance());
    }

    private void AutoAdvanceText()
    {
        StartCoroutine(CutsceneController.main.AutoAdvance());
    }

    public void SetForceSkip(bool skip)
    {
        forceSkip = skip;
        if (!CutsceneController.main.isSkipping)
        {
            if (forceSkip)
                CutsceneController.main.currentTextSpeed = CutsceneController.main.textSpeed * skipTextMultiplier;
            else
                CutsceneController.main.currentTextSpeed = CutsceneController.main.textSpeed;
        }
    }

    public void CheckForceSkip()
    {
        if (forceSkip)
            CutsceneController.main.currentTextSpeed = CutsceneController.main.textSpeed * skipTextMultiplier;
        else
            CutsceneController.main.currentTextSpeed = CutsceneController.main.textSpeed;
    }

    public void OnTextComplete()
    {
        textCompleted = true;
        if (forceSkip || CutsceneController.main.isSkipping)
        {
            SkipText();
        }
        else if (CutsceneController.main.isAuto)
        {
            AutoAdvanceText();
        }
        else
        {
            continueObject.SetActive(true);
        }
    }

    public override void OnEventComplete()
    {
        //Reset values
        CutsceneController.main.isSkipping = false;
        ControlButtonMouseEvents[] allButtons = FindObjectsOfType<ControlButtonMouseEvents>();

        //Turn off all buttons that are currently highlighted
        foreach(var i in allButtons)
        {
            if (i.isHighlighted)
                i.ToggleHighlight();
        }

        //Hide the dialog box and continue object
        cutsceneUI.SetActive(false);
        continueObject.SetActive(false);
    }
}
