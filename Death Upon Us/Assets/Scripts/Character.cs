using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody rigidBody;
    [SerializeField] private float movementSpeed = 10f;
    private float movementDirection;

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
    }

    private void Move()
    {
        float horizontalMovementDirection = Input.GetAxis("Horizontal");
        float verticalMovementDirection = - Input.GetAxis("Vertical");
        rigidBody.velocity = new Vector3(verticalMovementDirection * movementSpeed, rigidBody.velocity.y, horizontalMovementDirection * movementSpeed);
    }
}
