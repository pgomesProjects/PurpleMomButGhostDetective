using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneDialogHandler : CutsceneEvent
{
    private CustomEvent cutsceneCustomEvents;

    private void Awake()
    {
        cutsceneCustomEvents = GetComponent<CustomEvent>();
    }

    public override void OnDialogStart()
    {
        cutsceneUI.SetActive(true);
        ChangeSprite(0);
    }

    public override void CheckEvents(ref TextWriter.TextWriterSingle textWriterObj)
    {
        string message = dialogLines[currentLine];
        continueObject.SetActive(false);

        if (cutsceneCustomEvents != null)
            cutsceneCustomEvents.CheckForCustomEvent(currentLine);

        textWriterObj = TextWriter.AddWriter_Static(null, messageText, message, .05f, true, true, OnTextComplete);
        currentLine++;
    }

    private void OnTextComplete()
    {
        continueObject.SetActive(true);
    }

    public override void OnEventComplete()
    {
        //Hide the dialog box and continue object
        cutsceneUI.SetActive(false);
        continueObject.SetActive(false);
    }
}
