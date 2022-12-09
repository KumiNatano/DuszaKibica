using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : MonoBehaviour
{
    [SerializeField] int healAmount = 40;
    [SerializeField] int potionsLeft = 3;
    [SerializeField] double cooldown = 1.5;
    private double timeLeft;
    // Start is called before the first frame update
    // wzasadzie to konstruktor w tym wypadku
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft!=0)
            timeLeft -= Time.deltaTime;
        
    }
    public bool DrinkPotion(HealthSystem hps)
    {
        if (potionsLeft != 0 && timeLeft <= 0.0)
        {
            hps.Heal(healAmount);
            potionsLeft--;
            timeLeft = cooldown;
            return true;
        }
        return false;
    }
    public void setAmount(int amount)
    {
        healAmount = amount;
    }
    public void setPotionNumber(int num)
    {
        potionsLeft = num;
    }
    public void setCooldown(double num)
    {
        cooldown = num;
    }
    public int getPotionsLeft()
    {
        return potionsLeft;
    }
    public int getHealAmount()
    {
        return healAmount;
    }
    public double getCooldown()
    {
        return cooldown;
    }
}
