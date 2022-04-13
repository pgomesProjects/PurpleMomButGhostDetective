using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingCutsceneEvents : CustomEvent
{
    private CutsceneDialogHandler cutsceneDialogHandler;
    [SerializeField] private GameObject blackScreen;

    private void Awake()
    {
        cutsceneDialogHandler = GetComponent<CutsceneDialogHandler>();
    }

    public override void CheckForCustomEvent(int indexNumber)
    {
        switch (indexNumber)
        {
            case 0:
                cutsceneDialogHandler.HideSprite();
                ShowBlackScreen();
                break;
            case 2:
                cutsceneDialogHandler.SetNameBoxText("Unknown Voice 1");
                break;
            case 3:
                cutsceneDialogHandler.SetNameBoxText("Unknown Voice 2");
                break;
            case 5:
                cutsceneDialogHandler.SetNameBoxText("Unknown Voice 1");
                break;
            case 6:
                cutsceneDialogHandler.SetNameBoxText("Unknown Voice 2");
                break;
            case 7:
                cutsceneDialogHandler.SetNameBoxText("Clementine");
                break;
            case 8:
                cutsceneDialogHandler.HideNameBox();
                break;
            case 11:
                cutsceneDialogHandler.ShowNameBox();
                break;
            case 12:
                cutsceneDialogHandler.SetForceSkip(true);
                break;
            case 23:
                cutsceneDialogHandler.SetForceSkip(false);
                break;
            case 26:
                HideBlackScreen();
                cutsceneDialogHandler.ShowSprite();
                break;
            case 29:
                cutsceneDialogHandler.SpriteJump();
                break;
        }
    }

    public void ShowBlackScreen()
    {
        blackScreen.SetActive(true);
    }

    public void HideBlackScreen()
    {
        blackScreen.SetActive(false);
    }

    public override void CustomOnEventComplete()
    {
        StartCoroutine(TutorialController.main.ShowTutorialBox("Hold Left Click On The Mouse And Drag Left Or Right To Move Around The Scene.", 1, 1, 5));
    }
}
