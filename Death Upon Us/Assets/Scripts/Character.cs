using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody rigidBody;
    private float movementSpeed = 5f;
    private float rotationSpeed = 6f;
    private float jumpForce = 7f;
    private float movementDirection;
    private bool isGrounded;

    private void Start()
    {
        movementDirection = 0;
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
            movementSpeed = 10;
        }
        else
        {
            movementSpeed = 5;
        }
    }

    private void MoveForward()
    {
        transform.position += transform.forward * Time.deltaTime * movementSpeed;
    }

    private void Rotate(bool clockwise)
    {
        transform.Rotate(Vector3.up * rotationSpeed * (clockwise ? -1 : 1));
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rigidBody.velocity = Vector3.up * jumpForce;
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
