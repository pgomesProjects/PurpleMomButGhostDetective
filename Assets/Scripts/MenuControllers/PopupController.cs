using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupController : MonoBehaviour
{
    [SerializeField] private GameObject popupBox;
    private TextMeshProUGUI popupText;
    private Animator popupAnimator;

    public static PopupController main;

    private IEnumerator showPopBoxCoroutine;

    void Awake()
    {
        //Singleton-ify
        if (main == null) { main = this; } else { Destroy(this); }
    }

    // Start is called before the first frame update
    void Start()
    {
        popupText = popupBox.GetComponentInChildren<TextMeshProUGUI>();
        popupAnimator = popupBox.GetComponent<Animator>();
    }

    public void DisplayPopup(string message, float fadeInTime, float fadeOutTime, int secondsActive)
    {
        //If there's currently a popup box being shown, stop it from being shown
        if (showPopBoxCoroutine != null)
        {
            StopCoroutine(showPopBoxCoroutine);
        }

        //Set the popup box coroutine to the newest popup box and start the coroutine
        showPopBoxCoroutine = ShowPopupBox(message, fadeInTime, fadeOutTime, secondsActive);
        StartCoroutine(showPopBoxCoroutine);
    }

    private IEnumerator ShowPopupBox(string message, float fadeInTime, float fadeOutTime, int secondsActive)
    {
        //Change message on popup
        popupText.text = message;
        //Change speed of fade in based on the given time
        popupAnimator.speed = 1 / fadeInTime;
        popupAnimator.SetTrigger("FadeIn");
        //Wait a certain amount of time before fading out
        yield return new WaitForSeconds(secondsActive + 1);
        //Change speed of fade out based on the given time
        popupAnimator.speed = 1 / fadeOutTime;
        popupAnimator.SetTrigger("FadeOut");
    }
}
