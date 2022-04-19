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
                cutsceneDialogHandler.ChangeStill(0);
                cutsceneDialogHandler.ShowStill();
                cutsceneDialogHandler.HideNameBox();
                break;
            case 11:
                cutsceneDialogHandler.ChangeStill(1);
                cutsceneDialogHandler.ShowNameBox();
                break;
            case 12:
                cutsceneDialogHandler.SetForceSkip(true);
                break;
            case 23:
                cutsceneDialogHandler.HideStill();
                cutsceneDialogHandler.SetForceSkip(false);
                break;
            case 27:
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
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("Morgue", PlayerPrefs.GetFloat("BGMVolume", 0.5f));
        }
        StartCoroutine(TutorialController.main.ShowTutorialBox("Hold Left Click On The Mouse And Drag Left Or Right To Move Around The Scene.", 1, 1, 5));
    }
}
