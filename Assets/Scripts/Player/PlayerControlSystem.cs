using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Takes and handles input and movement for a player character
public class PlayerControlSystem : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float collisionOffset = 0.05f;

    Vector3 movementInput;
    Rigidbody collisionShape;

    // Start is called before the first frame update
    void Start()
    {
        collisionShape = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // If movement input is different than 0, try to move
        if (movementInput != Vector3.zero)
        {
            TryToMove(movementInput);
            
        }
    }

    private void TryToMove(Vector3 direction)
    {
        Vector3 high = new Vector3(0f, 1f, 0f);
        Debug.DrawRay(transform.position + high, movementInput * 2, Color.red);

        if (!Physics.Raycast(transform.position + high, movementInput, 2f))
        {
            collisionShape.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector3>();

    }
}
