using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryEntranceEvents : CustomEvent
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
                if (FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Play("LibraryCutsceneBGM", PlayerPrefs.GetFloat("BGMVolume", 0.5f));
                }
                cutsceneDialogHandler.HideSprite();
                cutsceneDialogHandler.ShowBlackScreen();
                break;
            case 2:
                cutsceneDialogHandler.ItalicizeText();
                break;
            case 3:
                cutsceneDialogHandler.NormalizeText();
                break;
            case 4:
                cutsceneDialogHandler.SetNameBoxText("Constable");
                break;
            case 6:
                if (FindObjectOfType<AudioManager>() != null)
                    FindObjectOfType<AudioManager>().Play("SnoringSFX", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
                break;
            case 7:
                cutsceneDialogHandler.SetNameBoxText("Clementine");
                break;
            case 10:
                cutsceneDialogHandler.HideNameBox();
                break;
            case 11:
                if (FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Stop("SnoringSFX");
                    FindObjectOfType<AudioManager>().Play("BlanketSFX", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
                }
                break;
            case 12:
                cutsceneDialogHandler.ItalicizeText();
                break;
            case 13:
                cutsceneDialogHandler.NormalizeText();
                cutsceneDialogHandler.ShowNameBox();
                break;
            case 15:
                cutsceneDialogHandler.HideBlackScreen();
                cutsceneDialogHandler.ShowSprite();
                break;
        }
    }

    public override void CustomOnEventComplete()
    {
        //Stop the cutscene music and play the background music that will be used in-game
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Stop("LibraryCutsceneBGM");
            FindObjectOfType<AudioManager>().Play("Library", PlayerPrefs.GetFloat("BGMVolume", 0.5f));
        }
    }
}
