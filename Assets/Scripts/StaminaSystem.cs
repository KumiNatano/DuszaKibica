using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    [SerializeField] public int staminaAmount;
    [SerializeField] private int maxStaminaAmount;
    [SerializeField] private int regenRate;
    [SerializeField] private float regenTick = 0.5f;
    [SerializeField] private float timer;
    public BarParent staminaBar;

    [SerializeField] UpgradeController upgradeController;

    // Start is called before the first frame update
    //wzasadzie to konstruktor w tym wypadku
    void Start()
    {

        upgradeController = GameObject.Find("UpgradeController").GetComponent<UpgradeController>(); //z upgrade controllera bierze bonus do staminy
        if (upgradeController.LiveUpgradeLevel != 0)
        {
            maxStaminaAmount = maxStaminaAmount + (upgradeController.StaminaUpgradeLevel * upgradeController.SingleStaminaBonus);
        }


        setMaxStaminaAmount(maxStaminaAmount);
        setRegen(regenRate);
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (staminaAmount<maxStaminaAmount)
            timer += Time.deltaTime;

        if (timer > regenTick)
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
    public void setMaxStaminaAmount(int max)
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
    public int getMaxStaminaAmount()
    {
        return maxStaminaAmount;
    }
}
