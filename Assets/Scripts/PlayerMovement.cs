using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float maxVelocity;

    private Rigidbody rb;
    private Camera mainCamera;

    private Vector3 movementDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }
    void Update()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
           Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

           Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

            movementDirection = transform.position - worldPosition; // vector pointing from where we touched to the player
            movementDirection.z = 0f; // avoid the player to leave the screen on this axis
            movementDirection.Normalize(); // maintains the direction while touching the screen

        }
        else
        {
            movementDirection = Vector3.zero; // when not touching the screen slow down the movement
        }       
    }

    private void FixedUpdate() // everytime the physics system updated. Stays consistent regardless of the game's performance
    {
        if(movementDirection ==  Vector3.zero) { return; }

        rb.AddForce(movementDirection * forceMagnitude, ForceMode.Force);

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity); // limite the velocity

    }
}
