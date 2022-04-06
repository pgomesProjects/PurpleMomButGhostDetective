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
    [SerializeField] private float skipTextMultiplier = 1.5f;

    private void Awake()
    {
        cutsceneCustomEvents = GetComponent<CustomEvent>();
    }

    public override void OnDialogStart()
    {
        currentTextSpeed = textSpeed;
        cutsceneUI.SetActive(true);
        ChangeSprite(0);
        SetNameBoxText("Clementine");
    }

    public override void CheckEvents(ref TextWriter.TextWriterSingle textWriterObj)
    {
        string message = dialogLines[currentLine];

        //Check for custom events if present
        if (cutsceneCustomEvents != null)
            cutsceneCustomEvents.CheckForCustomEvent(currentLine);

        //Add to history log
        if (nameBox.activeInHierarchy)
            DialogController.main.AddToLog(nameText.text + ":<br>");
        DialogController.main.AddToLog(message + "<br><br>");
        continueObject.SetActive(false);

        textWriterObj = TextWriter.AddWriter_Static(null, messageText, message, 1 / currentTextSpeed, true, true, OnTextComplete);
        currentLine++;
    }

    private void SkipText()
    {
        StartCoroutine(CutsceneController.main.ForceAdvance());
    }

    public void SetForceSkip(bool skip)
    {
        forceSkip = skip;
        if (forceSkip)
            currentTextSpeed = textSpeed * skipTextMultiplier;
        else
            currentTextSpeed = textSpeed;
    }

    private void OnTextComplete()
    {
        if (forceSkip)
        {
            SkipText();
        }
        else
        {
            continueObject.SetActive(true);
        }
    }

    public override void OnEventComplete()
    {
        //Hide the dialog box and continue object
        cutsceneUI.SetActive(false);
        continueObject.SetActive(false);
    }
}
