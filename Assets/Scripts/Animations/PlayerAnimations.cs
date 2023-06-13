using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] public Animator animator;

    public void setIsWalkingTrue()
    {
        animator.SetBool("isWalking", true);
    }

    public void setIsWalkingFalse()
    {
        animator.SetBool("isWalking", false);
    }

    public void setIsAttackingLeftTrue()
    {
        animator.SetBool("isAttackingLeft", true);
        //print("bije lewa");
    }

    public void setIsAttackingLeftFalse()
    {
        animator.SetBool("isAttackingLeft", false);
    }

    public void setIsAttackingRightTrue()
    {
        animator.SetBool("isAttackingRight", true);
    }

    public void setIsAttackingRightFalse()
    {
        animator.SetBool("isAttackingRight", false);
    }

    public void setIsDrinkingTrue()
    {
        animator.SetBool("isDrinking", true);
    }
    
    public void setIsDrinkingFalse()
    {
        animator.SetBool("isDrinking", false);
    }
    
    public void setIsFuryTrue()
    {
        animator.SetBool("isFury", true);
    }
    
    public void setIsFuryFalse()
    {
        animator.SetBool("isFury", false);
    }
}
