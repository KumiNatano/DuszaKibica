using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private int damage;
    private int baseDamage = 70;
    [SerializeField] private GameObject Player;
    public Weapons weapons;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<HealthSystem>() != null) //jesli nie ma systemu zycia to sie nie wykona
        {
            HealthSystem health = collider.GetComponent<HealthSystem>(); //bierzemy system zycia
            health.TakeDamage(damage); //i dajemy damage
            Player.GetComponent<PunchAudio>().PlayAttackSound(); //odgrywamy dzwiek uderzenia
            // weapons.checkForWeapons(); //sprawdzenie i podjęcie akcji związanych z updatem broni, obrażeń itp.
        }
    }

    public void setDamage(int bonusValue) {
        damage += bonusValue;
    }
    public int getBaseDamage() {
        return baseDamage;
    }
}
