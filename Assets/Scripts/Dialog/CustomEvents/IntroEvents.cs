using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroEvents : CustomEvent
{
    //This class is used as an example event system
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

    public override void CustomOnEventComplete()
    {
        
    }
}
