using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class MorgueManager : MonoBehaviour
{
    [SerializeField] private DialogEvent startingCutscene;
    [SerializeField] private GameObject spawnPoint;

    public enum Interaction { VIEWBODY, VIEWDESK, VIEWAUTOPSY };

    private bool [] hasInteracted = new bool[] { false, false, false };

    public static MorgueManager main;
    void Awake()
    {
        //Singleton-ify
        if (main == null) { main = this; } else { Destroy(this); }

        //Start the player without their inventory and having not viewed any of the tutorials
        if (GameManager.instance != null)
        {
            GameData.playerReference = Instantiate(GameManager.instance.playerPrefab, spawnPoint.transform);

            var vcam = FindObjectOfType<CinemachineVirtualCamera>();
            vcam.Follow = GameData.playerReference.transform;

            //Make sure the player does not their inventory and have not seen the tutorials
            GameManager.instance.playerHasInventory = false;
            GameManager.instance.tutorialsShown[(int)GameManager.Tutorial.USEINVENTORY] = false;

            //Reset data for new game
            GameData.NewGame();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Give the dialog event to the cutscene controller and start the cutscene immediately
        GameData.currentLevelName = "Morgue";
        GameData.readyForNextLevel = false;
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
        GameData.readyForNextLevel = true;
    }

}
