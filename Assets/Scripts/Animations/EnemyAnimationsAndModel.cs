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
    [SerializeField] private EnemyWeapon enemyLeftFist;
    [SerializeField] private EnemyWeapon enemyRightFist;
    private bool dying;

    private void Start()
    {
        int randomModelNumber = 3;//Random.Range(1, enemyModels.Count);
        enemyModels[randomModelNumber].SetActive(true);
        animator = animatorsList[randomModelNumber];
        dying = false;
    }

    public EnemyWeapon GetLeftFist() { return enemyLeftFist; }
    public EnemyWeapon GetRightFist() { return enemyRightFist; }

    public void setIsWalkingTrue()
    {
        animator.SetBool("isWalking", true);
    }

    public void setIsWalkingFalse()
    {
        animator.SetBool("isWalking", false);
    }

    public void setIsPreparingAttackingLeftTrue()
    {
        animator.SetBool("isPreparingLeft", true);
    }

    public void setIsAttackingLeftTrue()
    {
        animator.SetBool("isAttackingLeft", true);
        //GetLeftFist().GetComponent<Collider>().enabled = true;
    }

    public void setIsAttackingLeftFalse()
    {
        animator.SetBool("isAttackingLeft", false);
        GetLeftFist().GetComponent<Collider>().enabled = false;
    }

    public void setIsPreparingAttackingLeftFalse()
    {
        animator.SetBool("isPreparingLeft", false);
        GetLeftFist().GetComponent<Collider>().enabled = true;
    }

    public void setIsPreparingAttackingRightTrue()
    {
        animator.SetBool("isPreparingRight", true);
    }

    public void setIsAttackingRightTrue()
    {
        animator.SetBool("isAttackingRight", true);
        //GetRightFist().GetComponent<Collider>().enabled = true;
    }

    public void setIsAttackingRightFalse()
    {
        animator.SetBool("isAttackingRight", false);
        GetRightFist().GetComponent<Collider>().enabled = false;
    }

    public void setIsPreparingAttackingRightFalse()
    {
        animator.SetBool("isPreparingRight", false);
        GetRightFist().GetComponent<Collider>().enabled = true;
    }


    public void PlayDeathAnim()
    {
        animator.SetBool("isDying", true);
        Invoke("DisableAnimator", 1);
        dying = true;
    }

    public void DisableAnimator()
    {
        animator.enabled = false;
    }

    public bool isDying()
    {
        return dying;
    }
}
