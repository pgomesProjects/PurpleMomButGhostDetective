using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    private PlayerControlSystem playerControls;
    private Vector3 movement;
    private bool canMove, cutsceneActive;
    private void Awake()
    {
        playerControls = new PlayerControlSystem();
        playerControls.Player.Click.performed += _ => canMove = true;
        playerControls.Player.Click.canceled += _ => canMove = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        cutsceneActive = false;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !cutsceneActive)
            GetPlayerMovementInput();
    }

    private void GetPlayerMovementInput()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 screenPos = Camera.main.ScreenToViewportPoint(mousePos);
        if (screenPos.x < 0.5f)
        {
            movement.x = -(2 * (1 - screenPos.x) - 1);
            if (movement.x < -1)
                movement.x = -1;
        }
        else
        {
            movement.x = (screenPos.x - 0.5f) * 2;
            if (movement.x > 1)
                movement.x = 1;
        }
        Debug.Log(movement.x);

        transform.position += movement * speed * Time.deltaTime;
    }
}
