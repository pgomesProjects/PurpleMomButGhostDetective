using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
abstract public class DialogEvent : MonoBehaviour
{
    protected int currentLine;

    [Header("Dialog Objects")]
    [SerializeField] [Tooltip("The lines of dialog shown in order.")]
    protected string[] dialogLines;
    [SerializeField]
    [Tooltip("The number of characters to display per second.")]
    protected float textSpeed = 20;
    protected float currentTextSpeed;
    [SerializeField] [Tooltip("The object that holds the message text.")]
    protected TextMeshProUGUI messageText;
    [SerializeField] [Tooltip("The object that holds whatever will be used to tell the player to continue the dialog.")]
    protected GameObject continueObject;

    public abstract void CheckEvents(ref TextWriter.TextWriterSingle textWriterObj);

    public int GetCurrentLine() { return this.currentLine; }
    public int GetDialogLength() { return this.dialogLines.Length; }

    public abstract void OnDialogStart();
    public abstract void OnEventComplete();
}
