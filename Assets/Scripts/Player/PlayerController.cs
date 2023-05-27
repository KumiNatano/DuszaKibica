using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerModule
{
    public float walkSpeed = 5f;

    public PlayerCamera playerCamera => parent.playerCamera;
    public CharacterController controller => parent.characterController;


    public override void OnUpdate(float deltaTime)
    {
        Move();
    }

    private void Move(){
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        float vy = playerCamera.viewAngles.y;
        float speed = walkSpeed;
        Vector3 dir = AngleToDir(vy) * input.y + AngleToDir(vy + 90f) * input.x;

        dir.Normalize();

        controller.SimpleMove(dir * walkSpeed);
    }
    private Vector3 AngleToDir(float angle){
        return Quaternion.Euler(Vector3.up * angle) * Vector3.forward;
    }
}
