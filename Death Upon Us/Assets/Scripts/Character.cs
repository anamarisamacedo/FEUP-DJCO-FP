using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody rigidBody;
    private float movementSpeed = 10f;
    private float rotationSpeed = 6f;
    private float jumpForce = 3f;
    private float movementDirection;
    private bool isGrounded = true;

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

        if (Input.GetButton("Jump"))
        {
            Jump();
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
        if(isGrounded) {
            rigidBody.velocity += Vector3.up * jumpForce;
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
