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
    
    // on trigger przeniesiony do ClubScarf J.S
    public void usePotion() {
        potions.DrinkPotion(gameObject.GetComponent<HealthSystem>());
    }

    public int getScarfNumber() {
        return scarfNumber;
    }
    public void addScarfNumber(){
        scarfNumber++;
    }
    public StaminaSystem getStaminaSystem(){
        return stamina;
    }
}
