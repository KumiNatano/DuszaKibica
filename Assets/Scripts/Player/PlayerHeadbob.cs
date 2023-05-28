using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadbob : PlayerModule
{
    public float walkAmplitude = 0.05f;
    public float walkFrequency = 8f;

    PlayerCamera playerCamera => parent.playerCamera;


    public override void OnLateUpdate(float deltaTime)
    {
        Vector3 velocity = parent.characterController.velocity;
        velocity.y = 0;
        if (velocity.magnitude > float.Epsilon && !parent.controller.isDucking)
        {
            Vector3 newPos = playerCamera.GetPosition();
            newPos.y += Mathf.Sin(Time.time * walkFrequency) * walkAmplitude;
            playerCamera.SetPosition(newPos);
        }
    }
}