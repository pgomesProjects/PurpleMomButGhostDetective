using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    private PlayerControlSystem playerControls;
    private bool isPaused;
    [SerializeField] private GameObject pauseUI;

    private void Awake()
    {
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
        if (!GameManager.instance.isInventoryActive && !GameManager.instance.isCutsceneActive)
        {
            isPaused = !isPaused;
            pauseUI.SetActive(isPaused);
            //Pause
            if (isPaused)
            {
                Time.timeScale = 0.0f;
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
    }

    public void Resume()
    {
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("MouseClick", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
            FindObjectOfType<AudioManager>().Resume("Morgue");
        }

        isPaused = false;
        pauseUI.SetActive(isPaused);

        Time.timeScale = 1.0f;
    }

    public void ReturnToMain()
    {
        Time.timeScale = 1.0f;
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("MouseClick", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
            FindObjectOfType<AudioManager>().Stop("Morgue");
        }
        SceneManager.LoadScene("Titlescreen");
    }
}
