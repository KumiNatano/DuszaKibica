using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubScarf : DropItem
{
    float timeLived=0;
    [SerializeField] float timeToLive = 0.5f;


    protected override void Update(){
        base.Update();
        timeLived+=Time.deltaTime;
    }
    public override void OnPickup(Player player)
    {
        Inventory inventory = player.GetComponent<Inventory>();
        inventory.addScarfNumber();
        GameObject.FindGameObjectWithTag("UpgradeController").GetComponent<UpgradeController>().addMoney(5);

        int UpgradeID = Random.Range(1, 10);
        // zwiekszenie Staminy
        if (UpgradeID == 1)
        {
            inventory.stamina.setMaxStaminaAmount(inventory.stamina.getMaxStaminaAmount() + 5);
            //inventory.upgradeText.UpdateTextBox("+5 Staminy", "green");
        }
        // zwiekszenie HP
        if (UpgradeID == 6)
        {
            inventory.health.setMaxHealthAmount(inventory.health.getMaxHealthAmount() + 5);
            //inventory.upgradeText.UpdateTextBox("+5 Zdrowia", "red");
        }
        Destroy(gameObject);
    }
}
