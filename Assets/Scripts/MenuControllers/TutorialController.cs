using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private GameObject tutorialBox;
    private TextMeshProUGUI tutorialText;
    private Animator tutorialAnimator;

    public static TutorialController main;

    void Awake()
    {
        if (main == null) { main = this; } else { Destroy(this); }
    }

    // Start is called before the first frame update
    void Start()
    {
        tutorialText = tutorialBox.GetComponentInChildren<TextMeshProUGUI>();
        tutorialAnimator = tutorialBox.GetComponentInChildren<Animator>();
    }

    public IEnumerator ShowTutorialBox(string message, float fadeInTime, float fadeOutTime, int secondsActive)
    {
        tutorialText.text = message;
        tutorialAnimator.speed = 1 / fadeInTime;
        tutorialAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(secondsActive + 1);
        tutorialAnimator.speed = 1 / fadeOutTime;
        tutorialAnimator.SetTrigger("FadeOut");
    }
}
