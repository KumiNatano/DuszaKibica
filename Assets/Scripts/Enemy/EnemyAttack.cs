using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage = 3;
    [SerializeField] private float cooldown = 2f;
    [SerializeField] EnemyDetectPlayer enemyDetectPlayer;
    [SerializeField] EnemyAnimationsAndModel enemyAnimationsAndModel;
    [SerializeField] EnemyWeapon leftFist;
    [SerializeField] EnemyWeapon rightFist;
    [SerializeField] EnemyWeapon actualWeapon;

    private bool canAttack = true;

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
                enemyAnimationsAndModel.setIsAttackingLeftTrue();
                actualWeapon = leftFist;
            }

            else if (animacjaAtaku == 2)
            {
                enemyAnimationsAndModel.setIsAttackingRightTrue();
                actualWeapon = rightFist;
            }

            StartCoroutine(turnOffAnimation());

            StartCoroutine(AttackCoroutine()); //wywolujemy korutyne
            //HealthSystem health = collider.GetComponent<HealthSystem>(); //bierzemy system zycia
            //health.TakeDamage(damage); //i dajemy damage
            this.GetComponent<PunchAudio>().PlayAttackSound();

        }
    }

    IEnumerator turnOffAnimation()
    {
        yield return new WaitForSeconds(0.467f); //idealnie dlugosc animacji uderzania, w przyszlosci lepiej wrzucic to do zmiennej jesli beda np animacje o roznej dlugosci
        enemyAnimationsAndModel.setIsAttackingLeftFalse();
        enemyAnimationsAndModel.setIsAttackingRightFalse();
    }

    IEnumerator AttackCoroutine()
    {
        canAttack = false; //zmienia, ze nie mozemy zaatakowac
        yield return new WaitForSeconds(cooldown); //czekaj "cooldown" sekund zanim wykona instrukcje ponizej
        canAttack = true; //a po czasie juz mozna
    }

}
