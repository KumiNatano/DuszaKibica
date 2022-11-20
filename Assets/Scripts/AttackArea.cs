using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private int damage = 3;

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.GetComponent<HealthSystem>() != null) //jesli nie ma systemu zycia to sie nie wykona
        {
            HealthSystem health = collider.GetComponent<HealthSystem>(); //bierzemy system zycia
            health.TakeDamage(damage); //i dajemy damage
        }
    }
}
