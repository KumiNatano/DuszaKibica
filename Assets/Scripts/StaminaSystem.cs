using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    private int staminaAmount;
    private int maxStaminaAmount;
    private int regenRate;
    private float timer;
    public BarParent staminaBar;

    // Start is called before the first frame update
    //wzasadzie to konstruktor w tym wypadku
    void Start()
    {
        setMax(200);
        setRegen(1);
        timer = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (staminaAmount<maxStaminaAmount)
            timer += Time.deltaTime;

        if (timer > 0.05)
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
    private void RefillTick()
    {
        staminaAmount += regenRate;
        if (staminaAmount > maxStaminaAmount)
        {
            staminaAmount = maxStaminaAmount;
        }
        staminaBar.Change(staminaAmount);
    }
    public void setMax(int max)
    {
        maxStaminaAmount = max;
        staminaAmount = maxStaminaAmount;
        staminaBar.SetMax(maxStaminaAmount);
    }
    public void setRegen(int regen)
    {
        regenRate = regen;
    }
    public int getStamina()
    {
        return staminaAmount;
    }
    public int getMaxStamina()
    {
        return maxStaminaAmount;
    }
}
