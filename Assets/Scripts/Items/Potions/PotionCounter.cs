using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PotionCounter : MonoBehaviour
{
    public TMP_Text counter;
    //public Potions potions;
    // Start is called before the first frame update
    void Start()
    {
        //counter.text =getPotionsLeft.ToString()+"x Potions"
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateCounter(int potions)
    {
        counter.text = potions.ToString()+"x Potions";
    }
}
