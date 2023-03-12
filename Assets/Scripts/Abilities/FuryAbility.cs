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

    void activateAbility()
    {
        isActive = true;
        Debug.Log("tu wstawic zeby dodalo dmg");
    }

    void deactivateAbility()
    {
        isActive = false;
        Debug.Log("tu wstawic zeby zabralo dmg");
    }
}
