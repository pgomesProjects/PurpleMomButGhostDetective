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
    private float timeToReadText;

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
        string message = "";
        if (hasSeen)
        {
            message = hasSeenLines[currentLine];
            //If the lines have not been written into the history log, write them in
            if (!hasSeenWrittenInHistory)
            {
                //Add to history log
                if (nameBox.activeInHierarchy)
                    DialogController.main.AddToLog(nameText.text + ":<br>");
                DialogController.main.AddToLog(message + "<br><br>");
            }
        }
        else
        {
            message = dialogLines[currentLine];
            //If the lines have not been written into the history log, write them in
            if (!dialogWrittenInHistory)
            {
                //Add to history log
                if (nameBox.activeInHierarchy)
                    DialogController.main.AddToLog(nameText.text + ":<br>");
                DialogController.main.AddToLog(message + "<br><br>");
            }
        }

        textCompleted = false;

        //Get the text to read the text (based on text count and text speed)
        timeToReadText = message.Length / CutsceneController.main.currentTextSpeed;

        //Check for custom events if present
        if (cutsceneCustomEvents != null)
            cutsceneCustomEvents.CheckForCustomEvent(currentLine);

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
        Debug.Log("Time To Read Text: " + timeToReadText + " seconds");
        StartCoroutine(CutsceneController.main.AutoAdvance(timeToReadText));
    }

    public void SetForceSkip(bool skip)
    {
        forceSkip = skip;
        if (!CutsceneController.main.isSkipping)
        {
            CutsceneController.main.forceSkip = forceSkip;
            if (forceSkip)
            {
                CutsceneController.main.currentTextSpeed = CutsceneController.main.textSpeed * skipTextMultiplier;
            }
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
        ControlButtonMouseEvents[] allButtons = FindObjectsOfType<ControlButtonMouseEvents>();

        //Turn off all buttons that are currently highlighted or triggered
        foreach(var i in allButtons)
        {
            if (i.isHighlighted)
                i.ToggleHighlight();
        }

        //If skipping or auto, toggle them off
        if (CutsceneController.main.isSkipping)
            DialogController.main.ToggleSkip();

        if (CutsceneController.main.isAuto)
            DialogController.main.ToggleAuto();

        //Hide the dialog box and continue object
        cutsceneUI.SetActive(false);
        continueObject.SetActive(false);

        //If the scene has already been viewed, mark it read so it doesn't get written into the history
        if (hasSeen)
        {
            hasSeenWrittenInHistory = true;
        }
        //If not the cutscene has just been viewed
        else
        {
            hasSeen = true;
            dialogWrittenInHistory = true;
        }

        //Reset lines
        currentLine = 0;

        //Check for custom events if present
        if (cutsceneCustomEvents != null)
            cutsceneCustomEvents.CustomOnEventComplete();
    }
}
