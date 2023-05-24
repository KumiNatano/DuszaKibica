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
    private PerspectiveController perspectiveController;
    CharacterController controller;

    Vector3 movementInput;
    Rigidbody collisionShape;

    // Start is called before the first frame update
    void Start()
    {
        perspectiveController = this.gameObject.GetComponent<PerspectiveController>(); // pobieramy perspektywe z kontrolera perspektywy
        collisionShape = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        startSpeed = moveSpeed;
    }

    private void FixedUpdate()
    {
        // If movement input is different than 0, try to move
        if (movementInput != Vector3.zero)
        {
            TryToMove(movementInput);
            this.gameObject.GetComponent<PlayerAnimations>().setIsWalkingTrue();
        }
        else
        {
            this.gameObject.GetComponent<PlayerAnimations>().setIsWalkingFalse();
        }


        manageSpeedTimeLimit();
    }

    private void TryToMove(Vector3 direction)
    { 
        direction = this.transform.rotation * direction; // jesli widok top down to uzaleznij ruch od lokalnego zwrotu postaci, a nie od calego swiata
        Vector3 high = new Vector3(0f, 0.1f, 0f);
        //Debug.DrawRay(transform.position + high, movementInput * 4f, Color.red);

        controller.SimpleMove((transform.forward * movementInput.z + transform.right * movementInput.x).normalized * moveSpeed);
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector3>();

    }

    public void changeSpeedTimeLimit(float speed,float timeL, float cooldown) //doda�em mo�liwo�� zmiany pr�dko�ci z okre�lonym czasem trwania i cooldownu
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
