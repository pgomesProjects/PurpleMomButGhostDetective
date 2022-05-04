using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBinCutsceneEvents : CustomEvent
{
    private CutsceneDialogHandler cutsceneDialogHandler;

    private void Awake()
    {
        cutsceneDialogHandler = GetComponent<CutsceneDialogHandler>();
    }

    public override void CheckForCustomEvent(int indexNumber)
    {
        //First time cutscene events
        if (!cutsceneDialogHandler.HasSeenCutscene())
        {
            switch (indexNumber)
            {
                case 0:
                    cutsceneDialogHandler.ChangeSprite(0);
                    cutsceneDialogHandler.ShowSprite();
                    break;
                case 2:
                    cutsceneDialogHandler.ChangeSprite(1);
                    break;
                case 4:
                    cutsceneDialogHandler.ChangeSprite(2);
                    break;
                case 5:
                    cutsceneDialogHandler.ItalicizeText();
                    break;
                case 6:
                    cutsceneDialogHandler.NormalizeText();
                    break;
            }
        }

        //Seen cutscene events
        else
        {
            switch (indexNumber)
            {
                case 0:
                    cutsceneDialogHandler.ChangeSprite(2);
                    cutsceneDialogHandler.ShowSprite();
                    break;
            }
        }
    }

    public override void CustomOnEventComplete()
    {

    }
}
