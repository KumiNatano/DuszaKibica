using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    [SerializeField] int scarfNumber = 0;
    public ShowStatUpgrade upgradeText;
    public HealthSystem health;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
      
    }
    public int getScarfNumber() {
        return scarfNumber;
    }
    public void addScarfNumber(){
        scarfNumber++;
    }
}
