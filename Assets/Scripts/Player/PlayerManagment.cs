using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagment : MonoBehaviour
{
    [SerializeField] string potionKey;
    [SerializeField] string dodgeKey;
    public ShowStatUpgrade upgradeText;
    public StaminaSystem stamina;
    public HealthSystem health;
    public PlayerControlSystem pcsystem;
    public Inventory inventory;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(potionKey))
        {
            inventory.usePotion();
        }
        if (Input.GetKeyDown(dodgeKey))
        {
            dodge(0.1f, 15,0.1f, 1,30);
        }
    }

    public void dodge(float iTime, float dodgeSpeed, float dodgeLength, float cooldown, int staminaCost)
    {
        if (stamina.TakeAction(staminaCost))
        {
        health.setImmortal(iTime, cooldown - iTime);
        pcsystem.changeSpeedTimeLimit(dodgeSpeed, dodgeLength,cooldown-dodgeLength);
        }

    }

}
