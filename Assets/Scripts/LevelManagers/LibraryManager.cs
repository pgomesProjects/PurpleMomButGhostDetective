using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryManager : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;

    public static LibraryManager main;

    void Awake()
    {
        //Singleton-ify
        if (main == null) { main = this; } else { Destroy(this); }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Start the player without their inventory and having not viewed any of the tutorials
        if (GameManager.instance != null)
        {
            GameManager.instance.player = Instantiate(GameManager.instance.player, spawnPoint.transform);
            var vcam = FindObjectOfType<CinemachineVirtualCamera>();
            vcam.Follow = GameManager.instance.player.transform;
        }
    }

}
