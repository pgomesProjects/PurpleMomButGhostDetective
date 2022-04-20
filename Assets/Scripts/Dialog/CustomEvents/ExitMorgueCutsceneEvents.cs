using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMorgueCutsceneEvents : CustomEvent
{
    private CutsceneDialogHandler cutsceneDialogHandler;

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
                //Play the door open SFX if the audio manager is working in the level
                if (FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("DoorOpenSFX", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
                }
                cutsceneDialogHandler.HideSprite();
                cutsceneDialogHandler.ShowBlackScreen();
                break;
            case 10:
                cutsceneDialogHandler.HideNameBox();
                break;
        }
    }

    public override void CustomOnEventComplete()
    {
        //Stop the background music
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Stop("Morgue");
        }
        //Use the level fader to fade into the titlescreen scene
        LevelFader.instance.FadeToLevel("Titlescreen");
    }
}
