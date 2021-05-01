using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody rigidBody;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float jumpForce = 0.5f;
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
        if (Input.GetKey(KeyCode.W))
        {
            MoveForward();
        }
    }

    private void MoveForward()
    {
        transform.position += transform.forward * Time.deltaTime * movementSpeed;
    }

    private void Rotate(bool clockwise)
    {
        transform.Rotate(Vector3.up * 2f * (clockwise ? 1 : -1));
    }

    private void Jump()
    {
        if (Input.GetButton("Jump") && isGrounded)
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
