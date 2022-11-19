using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSystemParent : MonoBehaviour
{
    private int staminaAmount;
    private int maxStaminaAmount = 200;
    private int refillRate = 2;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        staminaAmount = maxStaminaAmount;
        timer = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (staminaAmount<maxStaminaAmount)
            timer += Time.deltaTime;

        if (timer > 5)
        {
            RefillTick();
            timer = 0;
        }

    }
    void TakeAction(int cost)
    {
        staminaAmount -= cost;
    }
    void RefillTick()
    {
        staminaAmount += refillRate;
        if (staminaAmount > maxStaminaAmount)
        {
            staminaAmount = maxStaminaAmount;
        }
    }
}
