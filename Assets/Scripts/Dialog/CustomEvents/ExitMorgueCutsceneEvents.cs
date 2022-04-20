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
        switch (indexNumber)
        {
            case 0:
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
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Stop("Morgue");
        }
        LevelFader.instance.FadeToLevel("Titlescreen");
    }
}
