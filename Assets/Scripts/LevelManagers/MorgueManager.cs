using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MorgueManager : MonoBehaviour
{
    [SerializeField] private DialogEvent startingCutscene;
    [SerializeField] private GameObject tutorialBox;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance != null)
        {
            GameManager.instance.playerHasInventory = false;
            GameManager.instance.tutorialsShown[(int)GameManager.Tutorial.USEINVENTORY] = false;
        }

        //Give the dialog event to the cutscene controller and start the cutscene
        CutsceneController.main.dialogEvent = startingCutscene;
        CutsceneController.main.TriggerDialogEvent();
    }
}
