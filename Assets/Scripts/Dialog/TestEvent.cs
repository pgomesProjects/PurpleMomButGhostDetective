using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestEvent : CutsceneEvent
{
    public override void OnDialogStart()
    {
        cutsceneUI.SetActive(true);
        ChangeSprite(0);
    }

    public override void CheckEvents(ref TextWriter.TextWriterSingle textWriterObj)
    {
        string message = dialogLines[currentLine];
        currentLine++;
        continueObject.SetActive(false);

        switch (currentLine)
        {
            case 2:
                HideSprite();
                textWriterObj = TextWriter.AddWriter_Static(HideNameBox, messageText, message, .05f, true, true, OnTextComplete);
                break;
            default:
                textWriterObj = TextWriter.AddWriter_Static(null, messageText, message, .05f, true, true, OnTextComplete);
                break;
        }
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
