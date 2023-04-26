using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : MonoBehaviour
{
    [SerializeField] int healAmount = 40;
    [SerializeField] int potionsLeft = 0;
    [SerializeField] double cooldown = 1.5;

    [SerializeField] UpgradeController upgradeController;

    private double timeLeft;
    public PotionCounter counter;
    // Start is called before the first frame update
    // wzasadzie to konstruktor w tym wypadku
    void Start()
    {
        counter.updateCounter(potionsLeft);
        upgradeController = GameObject.FindGameObjectsWithTag("UpgradeController")[0].GetComponent<UpgradeController>(); ;
        potionsLeft = upgradeController.GetComponent<UpgradeController>().PotionAmount; //z upgrade controllera bierze poczatkowa ilosc potek
        counter.updateCounter(potionsLeft);
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
            upgradeController.GetComponent<UpgradeController>().PotionAmount--; //w upgrade controllerze tez musi zmniejszac ilosc potionkow
            counter.updateCounter(potionsLeft);
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
