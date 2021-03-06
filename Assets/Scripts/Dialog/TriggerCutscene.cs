using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCutscene : MonoBehaviour
{
    [SerializeField] private DialogEvent currentCutscene;

    //An example class of triggering a cutscene when you collide with a box
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Give the dialog event to the cutscene controller and start the cutscene
            CutsceneController.main.dialogEvent = currentCutscene;
            CutsceneController.main.TriggerDialogEvent();
            //Destroy the collider
            Destroy(GetComponent<BoxCollider2D>());
        }
    }
}
