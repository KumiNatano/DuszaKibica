using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalAbility : MonoBehaviour
{
    [SerializeField] UpgradeController upgradeController;
    bool isImmortalBuyed = false;
    bool isActive = false;

    float timeDelay = 0f;
    [SerializeField] float abilityDelay = 15f;

    float timeDelay2 = 0f;
    [SerializeField] float abilityActiveTime = 5f;

    void Start()
    {
        upgradeController = GameObject.Find("UpgradeController").GetComponent<UpgradeController>();
        if (upgradeController.Immortal == true)
        {
            isImmortalBuyed = true;
        }
    }

    void Update()
    {
        if (isImmortalBuyed)
        {
            if (Time.time > timeDelay)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
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
        Debug.Log("tu wstawic zeby dalo niesmiertelnosc");
    }

    void deactivateAbility()
    {
        isActive = false;
        Debug.Log("tu wstawic zeby zabralo niesmiertelnosc");
    }
}
