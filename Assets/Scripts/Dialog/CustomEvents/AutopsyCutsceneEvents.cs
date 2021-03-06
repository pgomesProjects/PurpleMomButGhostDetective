using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutopsyCutsceneEvents : CustomEvent
{
    private CutsceneDialogHandler cutsceneDialogHandler;
    [SerializeField] private GameObject autopsyReport;

    private void Awake()
    {
        cutsceneDialogHandler = GetComponent<CutsceneDialogHandler>();
    }

    public override void CheckForCustomEvent(int indexNumber)
    {
        //Call certain functions based on the index number of the dialog being given
        switch (indexNumber)
        {
            case 0:
                cutsceneDialogHandler.ShowSprite();
                break;
            case 2:
                //Show the autopsy paper
                ShowAutopsy();
                break;
            case 7:
                cutsceneDialogHandler.ChangeSprite(1);
                break;
            case 8:
                cutsceneDialogHandler.ChangeSprite(2);
                break;
            case 9:
                cutsceneDialogHandler.ChangeSprite(0);
                break;
        }
    }

    public void ShowAutopsy()
    {
        autopsyReport.SetActive(true);
    }

    public void HideAutopsy()
    {
        autopsyReport.SetActive(false);
    }

    public override void CustomOnEventComplete()
    {
        //Hide the autopsy paper
        HideAutopsy();

        //Tell the level that the player interacted with the autopsy report
        MorgueManager.main.SetInteraction(MorgueManager.Interaction.VIEWAUTOPSY, true);
        MorgueManager.main.UpdateReadyForNextLevel();
    }
}
