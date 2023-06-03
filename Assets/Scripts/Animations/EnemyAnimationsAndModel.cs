using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using Random = UnityEngine.Random;

public class EnemyAnimationsAndModel : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private List<Animator> animatorsList;
    [SerializeField] private List<GameObject> enemyModels;
    private void Start()
    {
        int randomModelNumber = Random.Range(1, enemyModels.Count);
        enemyModels[randomModelNumber].SetActive(true);
        animator = animatorsList[randomModelNumber];
    }

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
    
    

    public void PlayDeathAnim()
    {
        animator.SetBool("isDying", true);
        Invoke("DisableAnimator", 1);
    }

    public void DisableAnimator()
    {
        animator.enabled = false;
    }
}
