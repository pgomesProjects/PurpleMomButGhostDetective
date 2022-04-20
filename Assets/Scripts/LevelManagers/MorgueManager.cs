using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MorgueManager : MonoBehaviour
{
    [SerializeField] private DialogEvent startingCutscene;

    internal bool readyForNextLevel;
    public enum Interaction { VIEWBODY, VIEWDESK, VIEWAUTOPSY };

    private bool [] hasInteracted = new bool[] { false, false, false };

    public static MorgueManager main;
    void Awake()
    {
        //Singleton-ify
        if (main == null) { main = this; } else { Destroy(this); }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance != null)
        {
            GameManager.instance.playerHasInventory = false;
            GameManager.instance.tutorialsShown[(int)GameManager.Tutorial.USEINVENTORY] = false;
        }

        //Give the dialog event to the cutscene controller and start the cutscene immediately
        CutsceneController.main.dialogEvent = startingCutscene;
        CutsceneController.main.TriggerDialogEvent();
    }

    public void SetInteraction(Interaction interactedItem, bool interacted)
    {
        //Set the interaction boolean
        hasInteracted[(int)interactedItem] = interacted;
    }

    public void UpdateReadyForNextLevel()
    {
        //Check all of the avaiable interactions
        foreach(var interaction in hasInteracted)
        {
            //If the player has not interacted with something, exit the function
            if (!interaction)
                return;
        }

        //If the player has interacted with everything, the player is ready to leave the level
        readyForNextLevel = true;
    }

}
