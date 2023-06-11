using System;
using UnityEngine;

public class PlayerPunchMachine : MonoBehaviour
{
    public string keyName => _keyName;

    public PunchMachineState state => _state;
    public bool isIdling => _state == PunchMachineState.Idle;
    public bool isAttacking => _state == PunchMachineState.Attack;
    public bool isResting => _state == PunchMachineState.Rest;


    public void Init(Player player)
    {
        playerRef = player;
    }
    public void Attack()
    {
        if (isIdling)
        {
            ForceAttack();
        }
    }
    public void ForceAttack()
    {
        throw new NotImplementedException();
    }
    public void Interrupt()
    {
        if (isAttacking)
        {
            throw new NotImplementedException();
        }
    }
    public void SkipAttack()
    {
        if (isAttacking)
        {
            throw new NotImplementedException();
        }
    }
    public void Rest()
    {
        if (isResting)
        {
            throw new NotImplementedException();
        }
    }

    void AttackSeq()
    {

    }
    void RestSeq()
    {

    }

    void FixedUpdate()
    {
        if (isAttacking)
        {
            AttackSeq();
        }
        else if (isResting)
        {
            RestSeq();
        }
    }


    Player playerRef;

    [SerializeField] string _keyName = "Fire1";
    [SerializeField] PunchMachineState _state = PunchMachineState.Idle;
}
public enum PunchMachineState
{
    Idle,
    Attack,
    Rest
}