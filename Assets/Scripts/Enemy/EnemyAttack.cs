using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage = 3;
    [SerializeField] private float cooldown = 2f;

    private bool canAttack = true;

    private void OnTriggerStay(Collider collider)
    {
        if (collider.GetComponent<HealthSystem>() != null && canAttack == true && collider.gameObject.tag == "player") //jesli obiekt z ktorym kolidujemy ma system zycia i mamy mozliwosc ataku i obiekt z ktorym kolidujemy to gracz
        {
            StartCoroutine(AttackCoroutine()); //wywolujemy korutyne
            HealthSystem health = collider.GetComponent<HealthSystem>(); //bierzemy system zycia
            health.TakeDamage(damage); //i dajemy damage
            this.GetComponent<PunchAudio>().PlayAttackSound();
        }
    }

    IEnumerator AttackCoroutine()
    {
        canAttack = false; //zmienia, ze nie mozemy zaatakowac
        yield return new WaitForSeconds(cooldown); //czekaj "cooldown" sekund zanim wykona instrukcje ponizej
        canAttack = true; //a po czasie juz mozna
    }

}
