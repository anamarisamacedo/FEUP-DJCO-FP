using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static utils.Configs;

public class Character : MonoBehaviour
{
    private Rigidbody rigidBody;
    private float movementSpeed;
    private int rotateDirection;
    private bool isGrounded;
    private bool isMoving;
    private bool isRunning;
    private bool isCrouched;
    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;

    private void Start()
    {
        isMoving = false;
        isRunning = false;
        isCrouched = false;
        rigidBody = GetComponent<Rigidbody>();
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
    }

    private void Update()
    {
        HandleInput();
        UpdateMovementSpeed();
    }
    private void FixedUpdate()
    {
        MoveForward();
        Rotate();
    }

    private void HandleInput()
    {
        float mouseDelta = Input.GetAxis("Mouse X");
        if (mouseDelta != 0)
        {
            rotateDirection = (mouseDelta < 0 ? -1 : 1);
        }
        else
        {
            rotateDirection = 0;
        }

        if (Input.GetButton("Vertical"))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouched = !isCrouched;
        }
    }

    private void UpdateMovementSpeed()
    {
        if (isCrouched)
        {
            movementSpeed = CrouchSpeed;
        }
        else if (isRunning)
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
        if (isMoving)
        {

            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up * RotationSpeed * rotateDirection);
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

    private void OnTriggerEnter(Collider collider)
    {
        WorldItem worldItem = collider.GetComponent<WorldItem>();
        if (worldItem != null)
        {
            inventory.AddItem(worldItem.GetItem());
            worldItem.DestroySelf();
        }
    }
}
