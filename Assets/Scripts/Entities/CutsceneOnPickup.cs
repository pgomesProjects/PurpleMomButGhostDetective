using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneOnPickup : MonoBehaviour
{
    [SerializeField] private DialogEvent pickupCutscene;

    public void OnPickup()
    {
        //When an item is picked up, start a delay for the cutscene
        StartCoroutine(StartCutsceneDelay(0.1f));
    }

    IEnumerator StartCutsceneDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //If the cutscene exists, trigger the cutscene
        if (pickupCutscene != null)
        {   //Give the dialog event to the cutscene controller and start the cutscene
            CutsceneController.main.dialogEvent = pickupCutscene;
            CutsceneController.main.TriggerDialogEvent();
            Destroy(gameObject, seconds);
        }
    }
}
