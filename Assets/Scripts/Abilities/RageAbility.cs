using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageAbility : BaseAbility
{
    public float speedBonus => _speedBonus;

    protected override void OnUseBegin()
    {
        player.attack.leftArm.Block();
        player.attack.leftArm.Interrupt();
        player.viewmodel.Drink();
    }
    protected override void OnUseEnd()
    {
        player.attack.leftArm.Unblock();
    }
    protected override void OnWorkBegin()
    {
        float sm = 1f / speedBonus;
        player.attack.leftArm.SetSpeed(sm);
        player.attack.rightArm.SetSpeed(sm);
    }
    protected override void OnWorkEnd()
    {
        player.attack.leftArm.SetSpeed(1);
        player.attack.rightArm.SetSpeed(1);
    }

    [SerializeField] float _speedBonus = 1.35f;
}
