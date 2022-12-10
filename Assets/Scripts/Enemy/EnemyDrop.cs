using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] float dropChance = 50;
    [SerializeField] int itemID = 1;
    [SerializeField] ClubScarf scarf;
    // Start is called before the first frame update

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
