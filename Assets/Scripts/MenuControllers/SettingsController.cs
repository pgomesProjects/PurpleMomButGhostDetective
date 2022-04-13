using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private Slider[] volumeSliders;
    [SerializeField] private Toggle vSyncToggle;

    // Start is called before the first frame update
    void Start()
    {
        volumeSliders[0].value = PlayerPrefs.GetFloat("BGMVolume", 0.5f) * 10;
        volumeSliders[1].value = PlayerPrefs.GetFloat("SFXVolume", 0.5f) * 10;
    }

    public void OnButtonClick()
    {
        //Play click SFX
        if (FindObjectOfType<AudioManager>() != null)
            FindObjectOfType<AudioManager>().Play("MouseClick", PlayerPrefs.GetFloat("SFXVolume", 0.5f));
    }

    public void BGMVolume(float volume)
    {
        PlayerPrefs.SetFloat("BGMVolume", volume * 0.1f);

        //Update the titlescreen music volume in realtime when adjusting value
        if (FindObjectOfType<AudioManager>() != null)
            FindObjectOfType<AudioManager>().ChangeVolume("Titlescreen", PlayerPrefs.GetFloat("BGMVolume", 0.5f));
    }

    public void SFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume * 0.1f);
    }

    public void VSyncEnabled(bool vSync)
    {

    }
}
