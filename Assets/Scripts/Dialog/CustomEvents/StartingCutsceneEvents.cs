using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingCutsceneEvents : CustomEvent
{
    private CutsceneDialogHandler cutsceneDialogHandler;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private Color scaredTextColor;

    private void Awake()
    {
        cutsceneDialogHandler = GetComponent<CutsceneDialogHandler>();
    }

    public override void CheckForCustomEvent(int indexNumber)
    {
        switch (indexNumber)
        {
            case 0:
                if (FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("CutsceneBGM", PlayerPrefs.GetFloat("BGMVolume", 0.5f));
                }
                cutsceneDialogHandler.HideSprite();
                cutsceneDialogHandler.ShowBlackScreen();
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
                if (FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Stop("CutsceneBGM");
                }
                cutsceneDialogHandler.ChangeStill(0);
                cutsceneDialogHandler.ShowStill();
                cutsceneDialogHandler.HideNameBox();
                break;
            case 11:
                if (FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("ShockedBGM", PlayerPrefs.GetFloat("BGMVolume", 0.5f));
                    FindObjectOfType<AudioManager>().Play("ShockedSFX", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
                }
                cutsceneDialogHandler.ChangeStill(1);
                cutsceneDialogHandler.ShowNameBox();
                break;
            case 12:
                cutsceneDialogHandler.SetTextColor(scaredTextColor);
                cutsceneDialogHandler.SetForceSkip(true);
                break;
            case 23:
                if (FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Stop("ShockedBGM");
                }
                cutsceneDialogHandler.HideStill();
                cutsceneDialogHandler.ResetTextColor();
                cutsceneDialogHandler.SetForceSkip(false);
                break;
            case 27:
                if (FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("CutsceneBGM", PlayerPrefs.GetFloat("BGMVolume", 0.5f));
                }
                cutsceneDialogHandler.HideBlackScreen();
                cutsceneDialogHandler.ChangeSprite(2);
                cutsceneDialogHandler.ShowSprite();
                break;
            case 29:
                cutsceneDialogHandler.ChangeSprite(3);
                cutsceneDialogHandler.SpriteJump();
                break;
            case 32:
                cutsceneDialogHandler.ChangeSprite(0);
                break;
            case 35:
                cutsceneDialogHandler.ChangeSprite(1);
                break;
        }
    }

    public override void CustomOnEventComplete()
    {
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Stop("CutsceneBGM");
            FindObjectOfType<AudioManager>().Play("Morgue", PlayerPrefs.GetFloat("BGMVolume", 0.5f));
        }
        PopupController.main.DisplayPopup("Hold Left Click On The Mouse And Drag Left Or Right To Move Around The Scene.", 1, 1, 5);
    }
}
