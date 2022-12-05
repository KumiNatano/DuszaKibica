using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    [SerializeField] int scarfNumber = 0;
    [SerializeField] Potions potions;
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
        }
    }
    public void usePotion(){
        potions.DrinkPotion(gameObject.GetComponent<HealthSystem>());
    }
}
