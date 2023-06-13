#define ENEMYATTACK_USEOBSOLETEHEALTH

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage = 3;
    [SerializeField] private float cooldown = 2f;
    [SerializeField] GameObject parent;

    private bool canAttack = true;
    [SerializeField] EnemyWeapon weapon;
    //private bool hit = false;

    private void Update()
    {
        if (canAttack == true && parent.GetComponent<EnemyDetectPlayer>().GetDistance() <= 2f) //jesli obiekt z ktorym kolidujemy ma system zycia i mamy mozliwosc ataku i obiekt z ktorym kolidujemy to gracz
        {
            int animacjaAtaku = Random.Range(1, 3);

            if (animacjaAtaku == 1)
            {
                parent.GetComponent<EnemyAnimationsAndModel>().setIsAttackingLeftTrue();
                weapon = parent.GetComponent<EnemyAnimationsAndModel>().GetLeftFist();

                StartCoroutine(turnOffAnimation());
                StartCoroutine(AttackCoroutine()); //wywolujemy korutyne

                if (weapon.IsHit() == true)
                {
                    //HealthSystem health = GetComponent<Collider>().GetComponent<HealthSystem>(); //bierzemy system zycia
                    var player = parent.GetComponent<EnemyDetectPlayer>().GetPlayer();
#if ENEMYATTACK_USEOBSOLETEHEALTH
                    var health = player.GetComponent<HealthSystem>();
                    health.TakeDamage(damage); //i dajemy damage
#else
                    var health = player.GetComponent<LivingMixin>();
                    health.Hurt(damage);
#endif
                }
                weapon.WasHit();
                this.GetComponent<PunchAudio>().PlayAttackSound();
            }

            else if (animacjaAtaku == 2)
            {
                parent.GetComponent<EnemyAnimationsAndModel>().setIsAttackingRightTrue();
                weapon = parent.GetComponent<EnemyAnimationsAndModel>().GetRightFist();

                StartCoroutine(turnOffAnimation());
                StartCoroutine(AttackCoroutine()); //wywolujemy korutyne

                if (weapon.IsHit() == true)
                {
                    //HealthSystem health = GetComponent<Collider>().GetComponent<HealthSystem>(); //bierzemy system zycia
                    var player = parent.GetComponent<EnemyDetectPlayer>().GetPlayer();
#if ENEMYATTACK_USEOBSOLETEHEALTH
                    var health = player.GetComponent<HealthSystem>();
                    health.TakeDamage(damage); //i dajemy damage
#else
                    var health = player.GetComponent<LivingMixin>();
                    health.Hurt(damage);
#endif
                }
                weapon.WasHit();
                this.GetComponent<PunchAudio>().PlayAttackSound();
            }
        }
    }

    IEnumerator turnOffAnimation()
    {
        yield return new WaitForSeconds(0.467f); //idealnie dlugosc animacji uderzania, w przyszlosci lepiej wrzucic to do zmiennej jesli beda np animacje o roznej dlugosci
        parent.GetComponent<EnemyAnimationsAndModel>().setIsAttackingLeftFalse();
        parent.GetComponent<EnemyAnimationsAndModel>().setIsAttackingRightFalse();
        weapon = null;
    }

    IEnumerator AttackCoroutine()
    {
        canAttack = false; //zmienia, ze nie mozemy zaatakowac
        yield return new WaitForSeconds(cooldown); //czekaj "cooldown" sekund zanim wykona instrukcje ponizej
        canAttack = true; //a po czasie juz mozna
    }

}
