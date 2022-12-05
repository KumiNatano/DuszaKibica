using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] float dropChance;
    [SerializeField] int itemID;
    [SerializeField] ClubScarf scarf;
    // Start is called before the first frame update
    void Start()
    {
        dropChance = 100;
        itemID = 1;

        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public bool DropLottery(){
        
        int chance = Random.Range(0,100);
        if(chance <= dropChance && chance != 0)
        {
           Instantiate(scarf, transform.position, scarf.transform.rotation);
            
            return true;
        }
        return false;
    }

}
