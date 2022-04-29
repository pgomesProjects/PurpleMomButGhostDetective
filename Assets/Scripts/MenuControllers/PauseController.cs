using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    private PlayerControlSystem playerControls;
    internal bool isPaused;
    [SerializeField] private GameObject pauseUI;

    public static PauseController main;

    private void Awake()
    {
        main = this;
        playerControls = new PlayerControlSystem();
        playerControls.Player.Pause.performed += _ => PauseToggle();
    }

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void PauseToggle()
    {
        //If the inventory is not active and a cutscene is not active, allow the player to pause
        if (!GameManager.instance.isInventoryActive && !GameManager.instance.isCutsceneActive && !DialogController.main.historyLogActive)
        {
            isPaused = !isPaused;
            pauseUI.SetActive(isPaused);
            //Pause
            if (isPaused)
            {
                //Time scale being set to 0 prevents anything in the main thread from moving
                //UI input can still be tracked, though
                Time.timeScale = 0.0f;

                //Pause the in-game music
                if (FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Pause("Morgue");
                }
            }
            //Resume
            else
            {
                Time.timeScale = 1.0f;
                if (FindObjectOfType<AudioManager>() != null)
                {
                    FindObjectOfType<AudioManager>().Resume("Morgue");
                }
            }
        }
        //If the history menu is active, hide it
        else if (DialogController.main.historyLogActive)
        {
            DialogController.main.HideHistoryLog();
        }
    }

    public void Resume()
    {
        //Play the mouse click SFX and continue the in-game music
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("MouseClick", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
            FindObjectOfType<AudioManager>().Resume("Morgue");
        }

        //Hide the pause menu and start time again
        isPaused = false;
        pauseUI.SetActive(isPaused);

        Time.timeScale = 1.0f;
    }

    public void ReturnToMain()
    {
        //Start time again, play the mouse click SFX, and stop the in-game music
        Time.timeScale = 1.0f;
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("MouseClick", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
            FindObjectOfType<AudioManager>().Stop("Morgue");
        }

        string levelMain = "Titlescreen";

        //If the level fader is in the level, use it to move to the next scene
        if (LevelFader.instance != null)
            LevelFader.instance.FadeToLevel(levelMain);
        //If not, just load the scene as a failsafe
        else
            SceneManager.LoadScene(levelMain);
    }
}
