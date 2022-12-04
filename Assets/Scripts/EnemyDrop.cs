using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] float dropChance;
    [SerializeField] int itemID;
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
    bool dropLottery(){
        
        int chance = Random.Range(0,100);
        if(chance <= dropChance && chance != 0)
        {
            return true;
        }
        return false;
    }

}
