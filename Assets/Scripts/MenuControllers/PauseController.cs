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
        if(!GameManager.instance.isInventoryActive && !GameManager.instance.isCutsceneActive)
        {
            isPaused = !isPaused;
            pauseUI.SetActive(isPaused);
            //Pause
            if (isPaused)
            {
                Time.timeScale = 0.0f;
            }
            //Resume
            else
            {
                Time.timeScale = 1.0f;
            }
        }
    }

    public void ReturnToMain()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Titlescreen");
    }
}
