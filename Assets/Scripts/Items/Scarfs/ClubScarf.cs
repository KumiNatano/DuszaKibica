using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubScarf : DropItem
{
    float timeLived=0;
    [SerializeField] float timeToLive = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update(){
        timeLived+=Time.deltaTime;
        rotating();

    }
    private void OnTriggerEnter(Collider collider) 
    {
        if(this.gameObject.tag == "scarf" && collider.gameObject.tag == "player" && timeLived>=timeToLive)
        {
            Destroy(this.gameObject);
            Inventory inventory = collider.GetComponent<Inventory>();
            inventory.addScarfNumber();

            int UpgradeID = Random.Range(1, 10);
            // zwiekszenie Staminy
            if (UpgradeID == 1) {
                inventory.stamina.setMaxStaminaAmount(inventory.stamina.getMaxStaminaAmount() + 5);
                //inventory.upgradeText.UpdateTextBox("+5 Staminy", "green");
            }
            // zwiekszenie HP
            if (UpgradeID == 6) {
                inventory.health.setMaxHealthAmount(inventory.health.getMaxHealthAmount() + 5);
                //inventory.upgradeText.UpdateTextBox("+5 Zdrowia", "red");
            }
        }
    } 
}
