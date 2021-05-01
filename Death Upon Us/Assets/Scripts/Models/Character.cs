using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static utils.Configs;

public class Character : MonoBehaviour
{
    private Rigidbody rigidBody;
    private float movementSpeed;
    private bool isGrounded;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        float mouseDelta = Input.GetAxis("Mouse X");
        if (mouseDelta != 0)
        {
            Rotate(mouseDelta < 0);
        }

        if (Input.GetButton("Vertical"))
        {
            MoveForward();
        }

        if (Input.GetButton("Vertical"))
        {
            MoveForward();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = RunningSpeed;
        }
        else
        {
            movementSpeed = WalkingSpeed;
        }
    }

    private void MoveForward()
    {
        transform.position += transform.forward * Time.deltaTime * movementSpeed;
    }

    private void Rotate(bool clockwise)
    {
        transform.Rotate(Vector3.up * RotationSpeed * (clockwise ? -1 : 1));
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rigidBody.velocity = Vector3.up * JumpForce;
        }
    }

    private void OnCollisionStay()
    {
        isGrounded = true;
    }

    private void OnCollisionExit()
    {
        isGrounded = false;
    }
}
