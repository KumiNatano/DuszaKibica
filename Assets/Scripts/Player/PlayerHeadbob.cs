using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadbob : PlayerModule
{
    public float runAmplitude = 0.075f;
    public float runFrequency = 12f;
    public float walkAmplitude = 0.05f;
    public float walkFrequency = 8f;
    public float duckAmplitude = 0.03f;
    public float duckFrequency = 4f;

    PlayerCamera playerCamera => parent.playerCamera;
    PlayerController controller => parent.controller;


    public override void OnLateUpdate(float deltaTime)
    {
        Vector3 velocity = parent.characterController.velocity;
        velocity.y = 0;
        if (velocity.magnitude > float.Epsilon)
        {
            float frequency = walkFrequency;
            float amplitude = walkAmplitude;
            if (controller.isRunning)
            {
                frequency = runFrequency;
                amplitude = runAmplitude;
            }
            else if (controller.isDucking)
            {
                frequency = duckFrequency;
                amplitude = duckAmplitude;
            }
            Vector3 newPos = playerCamera.GetPosition();
            newPos.y += Mathf.Sin(Time.time * frequency) * amplitude;
            playerCamera.SetPosition(newPos);
        }
    }
}