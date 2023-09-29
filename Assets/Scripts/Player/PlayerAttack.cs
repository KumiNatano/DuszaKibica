﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerModule
{
    /// <summary>
    /// Referencja do lewej atakującej ręki
    /// </summary>
    public PlayerPunchMachine leftArm => _leftArm;
    /// <summary>
    /// Referencja do prawej atakującej ręki
    /// </summary>
    public PlayerPunchMachine rightArm => _rightArm;

    public bool canAttack => leftArm.isIdling || rightArm.isIdling;


    public override void OnInit()
    {
        leftArm.Init(parent);
        rightArm.Init(parent);
    }
    public override void OnUpdate(float deltaTime)
    {
        if (!parent.living.isAlive)
        {
            return;
        }
        Debug.Log(Input.GetAxisRaw("Throw Knife") + " " + Input.GetAxisRaw(rightArm.keyName));
        if (Input.GetAxisRaw(leftArm.keyName) > 0 && !rightArm.isAttacking)
        {
            leftArm.Attack();
        }
        else if (Input.GetAxisRaw(rightArm.keyName) > 0 && !leftArm.isAttacking) 
        {
            rightArm.Attack();
        }
    }


    [Header("Arms")]
    [SerializeField] PlayerPunchMachine _leftArm;
    [SerializeField] PlayerPunchMachine _rightArm;
}
