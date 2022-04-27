using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingCutsceneEvents : CustomEvent
{
    private CutsceneDialogHandler cutsceneDialogHandler;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private Color scaredTextColor;

    private IEnumerator fadeInCoroutine;

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
            //Changing the name boxes for the dialog
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
                //Stop the background music
                if (FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Stop("CutsceneBGM");
                }
                //Show the still of Clementine looking in the mirror
                cutsceneDialogHandler.ChangeStill(0);
                fadeInCoroutine = cutsceneDialogHandler.FadeInStill(1);
                StartCoroutine(fadeInCoroutine);
                cutsceneDialogHandler.HideNameBox();
                break;
            case 9:
                if (fadeInCoroutine != null)
                {
                    StopCoroutine(fadeInCoroutine);
                    cutsceneDialogHandler.ShowStill();
                }
                break;
            case 12:
                //Play the shock sound effect and the shocked background music to accompany it
                if (FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("ShockedBGM", PlayerPrefs.GetFloat("BGMVolume", 0.5f));
                    FindObjectOfType<AudioManager>().Play("ShockedSFX", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
                }
                //Change the still to the scared image
                cutsceneDialogHandler.ChangeStill(1);
                cutsceneDialogHandler.ShowNameBox();
                break;
            case 13:
                //Change the text to red and start forcefully skipping through text
                cutsceneDialogHandler.SetTextColor(scaredTextColor);
                cutsceneDialogHandler.SetForceSkip(true);
                break;
            case 24:
                //Stop the shocked music
                if (FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Stop("ShockedBGM");
                }
                cutsceneDialogHandler.HideStill();

                //Reset the text color to black and stop forcefully skipping through text
                cutsceneDialogHandler.ResetTextColor();
                cutsceneDialogHandler.SetForceSkip(false);
                break;
            case 28:
                //Play the cutscene music again from the beginning
                if (FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("CutsceneBGM", PlayerPrefs.GetFloat("BGMVolume", 0.5f));
                }
                cutsceneDialogHandler.HideBlackScreen();
                cutsceneDialogHandler.ChangeSprite(2);
                cutsceneDialogHandler.ShowSprite();
                break;
            case 30:
                cutsceneDialogHandler.ChangeSprite(3);
                cutsceneDialogHandler.SpriteJump();
                break;
            case 33:
                cutsceneDialogHandler.ChangeSprite(0);
                break;
            case 36:
                cutsceneDialogHandler.ChangeSprite(1);
                break;
        }
    }

    public override void CustomOnEventComplete()
    {
        //Stop the cutscene music and play the background music that will be used in-game
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Stop("CutsceneBGM");
            FindObjectOfType<AudioManager>().Play("Morgue", PlayerPrefs.GetFloat("BGMVolume", 0.5f));
        }

        //Show the tutorial prompt immediately after the cutscene is over
        PopupController.main.DisplayPopup("Hold Left Click On The Mouse And Drag Left Or Right To Move Around The Scene.", 1, 1, 5);
    }
}
