using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isCutsceneActive;
    public bool isInventoryActive;
    public bool playerHasInventory;

    public enum Tutorial { USEINVENTORY };
    public bool[] tutorialsShown;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

}
