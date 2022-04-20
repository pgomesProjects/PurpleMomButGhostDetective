using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    [SerializeField] private DoorController door;

    public void UseKeyOnDoor()
    {
        if (door != null)
        {
            door.UnlockDoor();
        }
    }
}
