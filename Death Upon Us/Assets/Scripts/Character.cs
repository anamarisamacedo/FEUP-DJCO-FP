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

    private void Update()
    {
        movementDirection = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float horizontalMovementDirection = Input.GetAxis("Horizontal");
        float verticalMovementDirection = -Input.GetAxis("Vertical");
        rigidBody.velocity = new Vector3(verticalMovementDirection * movementSpeed, rigidBody.velocity.y, horizontalMovementDirection * movementSpeed);
    }

    private void Jump()
    {
        Debug.Log(Input.GetButton("Jump"));
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
