using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneOnPickup : MonoBehaviour
{
    [SerializeField] private DialogEvent pickupCutscene;

    private void OnDestroy()
    {
        if (pickupCutscene != null)
        {
            //Give the dialog event to the cutscene controller and start the cutscene
            CutsceneController.main.dialogEvent = pickupCutscene;
            CutsceneController.main.TriggerDialogEvent();
        }
    }
}
