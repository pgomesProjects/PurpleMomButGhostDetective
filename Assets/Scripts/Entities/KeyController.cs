using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    [SerializeField] private DoorController door;

    public void UseKeyOnDoor()
    {
        //If the door to the key exists, unlock the door
        if (door != null)
        {
            door.UnlockDoor();
        }
    }
}
