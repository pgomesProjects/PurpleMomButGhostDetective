using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlescreenController : MonoBehaviour
{
    [SerializeField] private string levelToStart;
    [SerializeField] private GameObject[] menuStateObjects;
    enum MenuState { TITLESCREEN, SETTINGS };
    private MenuState currentMenu = MenuState.TITLESCREEN;
    public void StartGame()
    {
        SceneManager.LoadScene(levelToStart);
    }

    public void SettingsToggle()
    {
        switch (currentMenu)
        {
            case MenuState.TITLESCREEN:
                currentMenu = MenuState.SETTINGS;
                menuStateObjects[(int)MenuState.TITLESCREEN].SetActive(false);
                menuStateObjects[(int)MenuState.SETTINGS].SetActive(true);
                break;
            case MenuState.SETTINGS:
                currentMenu = MenuState.TITLESCREEN;
                menuStateObjects[(int)MenuState.TITLESCREEN].SetActive(true);
                menuStateObjects[(int)MenuState.SETTINGS].SetActive(false);
                break;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
