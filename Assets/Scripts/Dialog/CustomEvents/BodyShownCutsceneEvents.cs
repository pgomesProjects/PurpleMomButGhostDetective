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

                    cutsceneDialogHandler.ChangeSprite(1);
                    cutsceneDialogHandler.SpriteJump();
                    cutsceneDialogHandler.ShowSprite();
                    break;
                case 3:
                    cutsceneDialogHandler.ChangeSprite(2);
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

        //Tell the level that the player interacted with the body
        MorgueManager.main.SetInteraction(MorgueManager.Interaction.VIEWBODY, true);
        MorgueManager.main.UpdateReadyForNextLevel();
    }
}
