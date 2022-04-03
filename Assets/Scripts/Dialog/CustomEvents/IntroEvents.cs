using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroEvents : CustomEvent
{
    private CutsceneDialogHandler cutsceneDialogHandler;

    private void Awake()
    {
        cutsceneDialogHandler = GetComponent<CutsceneDialogHandler>();
    }

    public override void CheckForCustomEvent(int indexNumber)
    {
        switch (indexNumber)
        {
            case 1:
                cutsceneDialogHandler.HideSprite();
                cutsceneDialogHandler.HideNameBox();
                break;
        }
    }

}
