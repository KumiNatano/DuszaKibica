using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] public int damage = 3;
    [SerializeField] private float cooldown = 2f;
    [SerializeField] EnemyDetectPlayer enemyDetectPlayer;
    [SerializeField] EnemyAnimationsAndModel enemyAnimationsAndModel;
    [SerializeField] EnemyWeapon leftFist;
    [SerializeField] EnemyWeapon rightFist;
    [SerializeField] EnemyWeapon actualWeapon;

    public bool canAttack = true;

    private void Start()
    {
        enemyDetectPlayer = GetComponentInParent<EnemyDetectPlayer>();
        enemyAnimationsAndModel = GetComponentInParent<EnemyAnimationsAndModel>();
        leftFist = enemyAnimationsAndModel.GetLeftFist();
        rightFist = enemyAnimationsAndModel.GetRightFist();
        leftFist.SetDamage(damage);
        rightFist.SetDamage(damage);
    }

    private void Update()
    {

        if (canAttack == true && enemyDetectPlayer.GetDistance() <= 1.4f) //jesli obiekt z ktorym kolidujemy ma system zycia i mamy mozliwosc ataku i obiekt z ktorym kolidujemy to gracz
        {
            int animacjaAtaku = Random.Range(1, 3);

            if (animacjaAtaku == 1)
            {
                enemyAnimationsAndModel.setIsPreparingAttackingLeftTrue();
                enemyAnimationsAndModel.setIsAttackingLeftTrue();
                actualWeapon = leftFist;
                StartCoroutine(waitUntilIsReadyToPunchLeft());
            }

            else if (animacjaAtaku == 2)
            {
                enemyAnimationsAndModel.setIsPreparingAttackingRightTrue();
                enemyAnimationsAndModel.setIsAttackingRightTrue();
                actualWeapon = rightFist;
                StartCoroutine(waitUntilIsReadyToPunchRight());
            }

            StartCoroutine(AttackCoroutine());
            //this.GetComponent<PunchAudio>().PlayAttackSound();
        }
    }

    IEnumerator waitUntilIsReadyToPunchLeft() //dawna wartosc dla wylaczenia animacji (0.467f)
    {
        yield return new WaitForSeconds(0.467f);
        enemyAnimationsAndModel.setIsPreparingAttackingLeftFalse();
        StartCoroutine(turnOffAnimationLeftFist());
    }
    IEnumerator waitUntilIsReadyToPunchRight()
    {
        yield return new WaitForSeconds(0.467f); 
        enemyAnimationsAndModel.setIsPreparingAttackingRightFalse();
        StartCoroutine(turnOffAnimationRightFist());
    }

    IEnumerator turnOffAnimationLeftFist()
    {
        yield return new WaitForSeconds(0.633f);
        enemyAnimationsAndModel.setIsAttackingLeftFalse();
    }
    IEnumerator turnOffAnimationRightFist()
    {
        yield return new WaitForSeconds(0.633f);
        enemyAnimationsAndModel.setIsAttackingRightFalse();
    }

    public IEnumerator AttackCoroutine()
    {
        print("test");
        canAttack = false; //zmienia, ze nie mozemy zaatakowac
        yield return new WaitForSeconds(cooldown); //czekaj "cooldown" sekund zanim wykona instrukcje ponizej
        if (actualWeapon.IsHit())
            actualWeapon.WasHit();
        canAttack = true; //a po czasie juz mozna
    }

}
