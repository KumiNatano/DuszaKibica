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
    [SerializeField] private List<GameObject> enemyLeftFists;
    [SerializeField] private List<GameObject> enemyRightFists;
    [SerializeField] private EnemyWeapon enemyLeftFist;
    [SerializeField] private EnemyWeapon enemyRightFist;
    private void Start()
    {
        int randomModelNumber = Random.Range(1, enemyModels.Count);
        enemyModels[randomModelNumber].SetActive(true);
        animator = animatorsList[randomModelNumber];
        enemyLeftFists[randomModelNumber].AddComponent<EnemyWeapon>();
        enemyRightFists[randomModelNumber].AddComponent<EnemyWeapon>();
        enemyLeftFist = enemyRightFists[randomModelNumber].GetComponent<EnemyWeapon>();
        enemyRightFist = enemyRightFists[randomModelNumber].GetComponent<EnemyWeapon>();

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

    public void setIsAttackingLeftTrue()
    {
        animator.SetBool("isAttackingLeft", true);
        GetLeftFist().GetComponent<Collider>().enabled = true;
    }

    public void setIsAttackingLeftFalse()
    {
        animator.SetBool("isAttackingLeft", false);
        GetLeftFist().GetComponent<Collider>().enabled = false;
    }

    public void setIsAttackingRightTrue()
    {
        animator.SetBool("isAttackingRight", true);
        GetRightFist().GetComponent<Collider>().enabled = true;
    }

    public void setIsAttackingRightFalse()
    {
        animator.SetBool("isAttackingRight", false);
        GetRightFist().GetComponent<Collider>().enabled = false;
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
