using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    [SerializeField] int scarfNumber = 0;
    [SerializeField] Potions potions;
    public ShowStatUpgrade upgradeText;
    public StaminaSystem stamina;
    public HealthSystem health;
    public BarParent hpBar;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.H))
        {
            usePotion();
        }
    }
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "scarf"&& this.gameObject.tag == "player")
        {
            scarfNumber++;
            Destroy(collider.gameObject);

            // po 10% szans na dany upgrade
            int UpgradeID = Random.Range(1, 10);
            // zwiekszenie Staminy
            if (UpgradeID == 1) {
                stamina.setMaxStaminaAmount(stamina.getMaxStaminaAmount() + 5);
                upgradeText.UpdateTextBox("+5 Staminy", "green");
            }
            // zwiekszenie HP
            if (UpgradeID == 6) {
                health.setMaxHealthAmount(health.getMaxHealthAmount() + 5);
                upgradeText.UpdateTextBox("+5 Zdrowia", "red");
            }
        }
    }
    public void usePotion() {
        potions.DrinkPotion(gameObject.GetComponent<HealthSystem>());
    }

    public int getScarfNumber() {
        return scarfNumber;
    }
}
