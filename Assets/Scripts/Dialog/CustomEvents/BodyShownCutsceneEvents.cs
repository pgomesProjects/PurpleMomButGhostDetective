using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyShownCutsceneEvents : CustomEvent
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
                    cutsceneDialogHandler.ChangeStill(0);
                    cutsceneDialogHandler.ShowStill();
                    break;
            }
        }
        
        //Seen cutscene events
        else
        {
            switch (indexNumber)
            {
                case 0:
                    cutsceneDialogHandler.ChangeStill(0);
                    cutsceneDialogHandler.ShowStill();
                    cutsceneDialogHandler.HideSprite();
                    break;
            }
        }
    }

    public override void CustomOnEventComplete()
    {
        cutsceneDialogHandler.HideStill();
    }
}
