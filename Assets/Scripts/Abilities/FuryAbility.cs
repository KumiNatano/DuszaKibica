using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuryAbility : MonoBehaviour
{
    [SerializeField] UpgradeController upgradeController;
    bool isFuryBuyed = false;
    bool isActive = false;

    float timeDelay = 0f;
    [SerializeField] float abilityDelay = 15f;

    float timeDelay2 = 0f;
    [SerializeField] float abilityActiveTime = 5f;

    [SerializeField] float furyAttackCooldown = 0.2f;
    [SerializeField] int furyAttackStamina = 500;

    void Start()
    {
        upgradeController = GameObject.Find("UpgradeController").GetComponent<UpgradeController>();
        if (upgradeController.Fury == true)
        {
            isFuryBuyed = true;
        }
    }

    void Update()
    {
        if(isFuryBuyed)
        {
            if (Time.time > timeDelay)
            {
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    activateAbility();
                    timeDelay = Time.time + abilityDelay;
                    timeDelay2 = Time.time + abilityActiveTime;
                }
            }

            if (Time.time > timeDelay2)
            {
                if (isActive)
                {
                    deactivateAbility();
                }
            }
        }
    }

    private float previousCooldown;

    void activateAbility()
    {
        isActive = true;
        previousCooldown = this.gameObject.GetComponent<PlayerAttack>().cooldown;
        this.gameObject.GetComponent<PlayerAttack>().cooldown = furyAttackCooldown;
        this.gameObject.GetComponent<StaminaSystem>().staminaAmount = furyAttackStamina;
        Debug.Log("tu wstawic zeby zwiekszylo szybkosc i dalo stamine");
    }

    void deactivateAbility()
    {
        this.gameObject.GetComponent<PlayerAttack>().cooldown = previousCooldown;
        isActive = false;
        Debug.Log("tu wstawic zeby zmniejszylo szybkosc");
    }
}
