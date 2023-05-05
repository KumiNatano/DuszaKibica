using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSAnimations : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void setIsAttackingLeftTrue()
    {
        animator.SetBool("isAttackingLeft", true);
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
}
