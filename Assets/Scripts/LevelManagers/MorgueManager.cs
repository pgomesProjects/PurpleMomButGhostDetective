using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorgueManager : MonoBehaviour
{
    [SerializeField] private DialogEvent startingCutscene;

    // Start is called before the first frame update
    void Start()
    {
        //Give the dialog event to the cutscene controller and start the cutscene
        CutsceneController.main.dialogEvent = startingCutscene;
        CutsceneController.main.TriggerDialogEvent();
    }

}
