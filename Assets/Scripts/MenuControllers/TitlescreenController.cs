using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlescreenController : MonoBehaviour
{
    [SerializeField] private GameObject[] menuStateObjects;
    enum MenuState { TITLESCREEN, SETTINGS };
    private MenuState currentMenu = MenuState.TITLESCREEN;
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void SettingsToggle()
    {
        switch (currentMenu)
        {
            case MenuState.TITLESCREEN:
                currentMenu = MenuState.SETTINGS;
                break;
            case MenuState.SETTINGS:
                currentMenu = MenuState.TITLESCREEN;
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
