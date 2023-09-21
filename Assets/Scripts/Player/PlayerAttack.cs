using System.Collections;
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
        if (Input.GetButtonDown(leftArm.keyName) && !rightArm.isAttacking)
        {
            leftArm.Attack();
        }
        else if (Input.GetButtonDown(rightArm.keyName) && !leftArm.isAttacking) 
        {
            rightArm.Attack();
        }
    }


    [Header("Arms")]
    [SerializeField] PlayerPunchMachine _leftArm;
    [SerializeField] PlayerPunchMachine _rightArm;
}
