using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    internal bool isCutsceneActive;
    internal bool isInventoryActive;
    internal bool playerHasInventory;
    internal bool playerSelectingItem;
    public GameObject playerPrefab;

    internal enum Tutorial { USEINVENTORY };
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
