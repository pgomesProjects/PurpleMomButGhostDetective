using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorCutsceneEvents : CustomEvent
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
            case 0:
                cutsceneDialogHandler.ChangeSprite(3);
                cutsceneDialogHandler.ShowSprite();
                cutsceneDialogHandler.SpriteJump();
                break;
            case 2:
                cutsceneDialogHandler.ChangeSprite(0);
                break;
            case 4:
                cutsceneDialogHandler.ChangeSprite(1);
                break;
            case 5:
                cutsceneDialogHandler.ChangeSprite(2);
                break;
        }
    }

    public override void CustomOnEventComplete()
    {

    }
}
