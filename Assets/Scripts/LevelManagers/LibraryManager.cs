using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryManager : MonoBehaviour
{
    [SerializeField] private DialogEvent startingCutscene;
    [SerializeField] private GameObject spawnPoint;

    public static LibraryManager main;

    void Awake()
    {
        //Singleton-ify
        if (main == null) { main = this; } else { Destroy(this); }

        if (GameManager.instance != null)
        {
            GameData.playerReference = Instantiate(GameManager.instance.playerPrefab, spawnPoint.transform);

            var vcam = FindObjectOfType<CinemachineVirtualCamera>();
            vcam.Follow = GameData.playerReference.transform;

            //Make sure the player has their inventory
            if (!GameManager.instance.playerHasInventory)
                GameManager.instance.playerHasInventory = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Give the dialog event to the cutscene controller and start the cutscene immediately
        CutsceneController.main.dialogEvent = startingCutscene;
        CutsceneController.main.TriggerDialogEvent();
    }

}
