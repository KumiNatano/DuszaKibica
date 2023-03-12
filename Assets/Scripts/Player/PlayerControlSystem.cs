using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Takes and handles input and movement for a player character
public class PlayerControlSystem : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float collisionOffset = 0.05f;
    private float startSpeed;
    private float timeLimit;
    private float timeLimitBase;
    [SerializeField] private float cooldownSpeed;

    public ContactFilter2D movementFilter;

    // Vector2 = [x, y]
    // x - left, right
    // y - up, down
    Vector2 movementInput;
    Rigidbody2D collisionShape;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        collisionShape = GetComponent<Rigidbody2D>();
        startSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Fixed update is called a certain number of times per second
    // Recommended when working on physics
    private void FixedUpdate() {        
        // If movement input is different than 0, try to move
        if(movementInput != Vector2.zero) {
            bool successfullMove = TryToMove(movementInput);

            if(!successfullMove) {
                successfullMove = TryToMove(new Vector2(movementInput.x, 0));

                if(!successfullMove) {
                    successfullMove = TryToMove(new Vector2(0, movementInput.y));
                }
            }
        }
        manageSpeedTimeLimit();
    }

    private bool TryToMove(Vector2 direction) {
        // Check for potential collisions
        int count = collisionShape.Cast(                            // if count = 0 the move is valid (no collisions will occur) so we can move
            direction,                                              // X and Y values between - 1 and 1 that represent the direction from the body to look for collisions
            movementFilter,                                         // The settings that determine where a collision can occur on; such as layers to collide with
            castCollisions,                                         // List of collisions to store the found collisions into, after the Cast is finished
            moveSpeed * Time.fixedDeltaTime + collisionOffset);     // The amount to cast equal to the movement plus an offset

        if(count == 0) {
            collisionShape.MovePosition(collisionShape.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        } 
        else {
            return false;
        }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();

    }

    public void changeSpeedTimeLimit(float speed,float timeL, float cooldown) //doda³em mo¿liwoœæ zmiany prêdkoœci z okreœlonym czasem trwania i cooldownu
    {
        if(cooldownSpeed > 0)
        {
            return;
        }
        startSpeed = moveSpeed;
        moveSpeed = speed;
        timeLimit = timeL;
        timeLimitBase = timeLimit;
        cooldownSpeed = cooldown;
    }
    private void manageSpeedTimeLimit()
    {
        if (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
            //rotating(timeLimitBase);
        }
        if(timeLimit <= 0 && moveSpeed != startSpeed)
        {
            moveSpeed = startSpeed;
        }
        if(cooldownSpeed > 0)
        {
            cooldownSpeed -= Time.deltaTime;
        }

    }
}
