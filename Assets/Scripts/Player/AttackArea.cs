using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private int damage;
    private int baseDamage = 70;
    [SerializeField] private GameObject Player;
    public Weapons weapons;
    [SerializeField] private float knockbackForce = 10f;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<HealthSystem>() != null) //jesli nie ma systemu zycia to sie nie wykona
        {
            HealthSystem health = collider.GetComponent<HealthSystem>(); //bierzemy system zycia
            health.TakeDamage(damage); //i dajemy damage
            Player.GetComponent<PunchAudio>().PlayAttackSound(); //odgrywamy dzwiek uderzenia
            // weapons.checkForWeapons(); //sprawdzenie i podjęcie akcji związanych z updatem broni, obrażeń itp.
            Rigidbody enemyRigidbody = collider.gameObject.GetComponent<Rigidbody>();
            Vector3 knockbackDirection = (enemyRigidbody.transform.position - transform.position).normalized;
            enemyRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }
        
    }

    public void setDamage(int bonusValue) {
        damage += bonusValue;
    }
    public int getBaseDamage() {
        return baseDamage;
    }
}
