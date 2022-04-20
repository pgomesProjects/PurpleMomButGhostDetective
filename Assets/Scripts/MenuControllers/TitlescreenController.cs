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

    private void Start()
    {
        //Play the title screen music upon starting the game
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("Titlescreen", PlayerPrefs.GetFloat("BGMVolume", 0.5f));
        }
    }

    public void StartGame()
    {
        //Click SFX
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("MouseClick", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
            FindObjectOfType<AudioManager>().Stop("Titlescreen");
        }

        //If the level fade exists, use it to fade into the first game scene
        if (LevelFader.instance != null)
            LevelFader.instance.FadeToLevel(levelToStart);
        //If not, just load the scene as a failsafe
        else
            SceneManager.LoadScene(levelToStart);
    }

    public void SettingsToggle()
    {
        //Click SFX
        if (FindObjectOfType<AudioManager>() != null)
            FindObjectOfType<AudioManager>().Play("MouseClick", PlayerPrefs.GetFloat("SFXVolume", 0.5f));

        //Check which state the menu is in
        switch (currentMenu)
        {
            //If the menu is currently the titlescreen
            case MenuState.TITLESCREEN:
                //Switch it to the settings screen
                currentMenu = MenuState.SETTINGS;
                menuStateObjects[(int)MenuState.TITLESCREEN].SetActive(false);
                menuStateObjects[(int)MenuState.SETTINGS].SetActive(true);
                break;
            //If the menu is currently the settings screen
            case MenuState.SETTINGS:
                //Switch it to the titlescreen
                currentMenu = MenuState.TITLESCREEN;
                menuStateObjects[(int)MenuState.TITLESCREEN].SetActive(true);
                menuStateObjects[(int)MenuState.SETTINGS].SetActive(false);
                break;
        }
    }

    public void QuitGame()
    {
        //Click SFX
        if (FindObjectOfType<AudioManager>() != null)
            FindObjectOfType<AudioManager>().Play("MouseClick", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
        Debug.Log("Quitting Game...");
        //Quits the application
        Application.Quit();

        //If playing the game in the Unity Editor, tell the editor to stop playing the game as well
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
