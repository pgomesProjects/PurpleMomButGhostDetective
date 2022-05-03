using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadController : MonoBehaviour
{
    private KeypadObjectController keypadObject;
    [SerializeField] private TextMeshProUGUI keycodeText;
    [SerializeField] private GameObject backButton;
    [SerializeField] private Color accessDeny = new Color(1, 1, 1, 1);
    [SerializeField] private Color accessGranted = new Color(1, 1, 1, 1);
    private Color defaultColor;
    private bool inputAllowed;

    private void Start()
    {
        keypadObject = GetComponentInParent<KeypadObjectController>();
        defaultColor = keycodeText.color;
        inputAllowed = true;
    }

    public static bool AnyKeypadActive()
    {
        //Return true if any keypad is currently up 
        foreach (var i in FindObjectsOfType<KeypadObjectController>())
            if (i.isKeypadUp)
                return true;

        return false;
    }

    public void EnterNumber(int number)
    {
        if (inputAllowed)
        {
            keycodeText.text += number.ToString();
            CheckForValidCode();
        }
    }

    private void CheckForValidCode()
    {
        if (inputAllowed)
        {
            //If the keycode is the correct length, check to see if it works
            if (keycodeText.text.Length == keypadObject.keycode.Length)
            {
                if (keycodeText.text == keypadObject.keycode)
                {
                    Debug.Log("Access Granted.");
                    StartCoroutine(GrantedAnimation());
                }
                else
                {
                    Debug.Log("Access Denied.");
                    StartCoroutine(DeniedAnimation());
                }
            }
        }
    }

    IEnumerator DeniedAnimation()
    {
        //Show denied color
        keycodeText.color = accessDeny;
        backButton.SetActive(false);
        inputAllowed = false;

        yield return new WaitForSeconds(0.5f);

        //Clear text and change back to normal color
        keycodeText.text = string.Empty;
        keycodeText.color = defaultColor;
        backButton.SetActive(true);
        inputAllowed = true;
    }

    IEnumerator GrantedAnimation()
    {
        //Show granted color
        keycodeText.color = accessGranted;
        backButton.SetActive(false);
        inputAllowed = false;

        yield return new WaitForSeconds(0.5f);

        //Allow player access
        keypadObject.ShowKeypadUI(false);
        keypadObject.UnlockDoor();

        //Clear text and change back to normal color
        keycodeText.text = string.Empty;
        keycodeText.color = defaultColor;
        backButton.SetActive(true);
        inputAllowed = true;
    }

    public void Clear()
    {
        if (inputAllowed)
        {
            keycodeText.text = string.Empty;
        }
    }

    public void Backspace()
    {
        if (inputAllowed)
        {
            if (keycodeText.text.Length > 0)
            {
                //Remove last character of string
                keycodeText.text = keycodeText.text.Substring(0, keycodeText.text.Length - 1);
            }
        }
    }

    public void BackButton()
    {
        if (inputAllowed)
        {
            //Clear the keypad and hide the UI
            Clear();
            keypadObject.ShowKeypadUI(false);
        }
    }
}
