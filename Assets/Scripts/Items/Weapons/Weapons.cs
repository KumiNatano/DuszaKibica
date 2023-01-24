using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : DropItem
{
    float timeLived=0;
    [SerializeField] float timeToLive = 0.5f;
    public bool butelkaInHand = false;
    public bool tulipanInHand = false;
    public AttackArea attack;
    public Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update(){
        timeLived+=Time.deltaTime;
        rotating();
    }
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        // TULIPAN
        if (this.gameObject.tag == "butelka" && collider.gameObject.tag == "player" && timeLived>=timeToLive)
        {
            Destroy(this.gameObject);
            // inventory = collider.GetComponent<Inventory>();

            butelka();
        }
        // Potencjalnie inne bronie ...
    }

    public void butelka() {
        inventory.upgradeText.UpdateTextBox("Podniesiono butelke!", "");
        // Tekstury ...
        int bonusButelkaDamage = 20;
        butelkaInHand = true;
        attack.setDamage(bonusButelkaDamage);
    }
    public void tulipan() {
        inventory.upgradeText.UpdateTextBox("Zrobiles tulipana!", "");
        // Tekstury ...
        int bonusTulipanDamage = 10;
        butelkaInHand = false;
        tulipanInHand = true;
        attack.setDamage(bonusTulipanDamage);
    }
    public void checkForWeapons() {
        if (butelkaInHand) {
            // Upgrade broni do tulipana
            tulipan();
        }
        else if (tulipanInHand) {
            // Przywróć obrażenia podstawowe
            attack.setDamage(attack.getBaseDamage());
            tulipanInHand = false;
            // Usuń teksturę
        }
    }
}