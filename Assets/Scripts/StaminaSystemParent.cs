using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSystemParent : MonoBehaviour
{
    private int staminaAmount;
    private int maxStaminaAmount = 200;
    private int refillRate = 2;
    private float timer;
    public BarParent staminaBar;

    // Start is called before the first frame update
    void Start()
    {
        staminaAmount = maxStaminaAmount;
        staminaBar.SetMax(maxStaminaAmount);
        timer = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (staminaAmount<maxStaminaAmount)
            timer += Time.deltaTime;

        if (timer > 0.5)
        {
            RefillTick();
            timer = 0;
        }

    }
    public bool TakeAction(int cost)
    {
        if (staminaAmount - cost >= 0)
        {
            staminaAmount -= cost;
            staminaBar.Change(staminaAmount);
            return true;
        }
        return false;
    }
    void RefillTick()
    {
        staminaAmount += refillRate;
        if (staminaAmount > maxStaminaAmount)
        {
            staminaAmount = maxStaminaAmount;
        }
        staminaBar.Change(staminaAmount);
    }
}
