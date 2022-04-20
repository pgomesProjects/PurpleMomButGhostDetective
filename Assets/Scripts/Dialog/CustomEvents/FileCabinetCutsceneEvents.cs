using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileCabinetCutsceneEvents : CustomEvent
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
                    cutsceneDialogHandler.ChangeSprite(0);
                    cutsceneDialogHandler.ShowSprite();
                    break;
            }
        }

        //Seen cutscene events
        else
        {
            switch (indexNumber)
            {
                case 0:
                    cutsceneDialogHandler.ChangeSprite(0);
                    cutsceneDialogHandler.ShowSprite();
                    break;
            }
        }
    }

    public override void CustomOnEventComplete()
    {
        //Tell the level that the player interacted with the file cabinet
        MorgueManager.main.SetInteraction(MorgueManager.Interaction.VIEWDESK, true);
        MorgueManager.main.UpdateReadyForNextLevel();
    }
}
