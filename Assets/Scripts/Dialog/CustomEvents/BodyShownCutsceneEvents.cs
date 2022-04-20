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
        //Call certain functions based on the index number of the dialog being given
        //First time cutscene events
        if (!cutsceneDialogHandler.HasSeenCutscene())
        {
            switch (indexNumber)
            {
                case 0:
                    //Change the still image and then show it
                    cutsceneDialogHandler.ChangeStill(0);
                    cutsceneDialogHandler.ShowStill();

                    //Change the sprite and then give it a little jump animation
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
                    //Show the still, but don't show the sprite
                    cutsceneDialogHandler.ChangeStill(0);
                    cutsceneDialogHandler.ShowStill();
                    cutsceneDialogHandler.HideSprite();
                    break;
            }
        }
    }

    public override void CustomOnEventComplete()
    {
        //Hide the still after the cutscene is over
        cutsceneDialogHandler.HideStill();

        //Tell the level that the player interacted with the body
        MorgueManager.main.SetInteraction(MorgueManager.Interaction.VIEWBODY, true);
        MorgueManager.main.UpdateReadyForNextLevel();
    }
}
