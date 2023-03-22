using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] GameObject UI = null;
    [SerializeField] GameObject abilities = null;
    [SerializeField] GameObject furyImage = null;

    void Start()
    {
        upgradeController = GameObject.Find("UpgradeController").GetComponent<UpgradeController>();
        if (upgradeController.Fury == true)
        {
            isFuryBuyed = true;
            UI = GameObject.Find("UI");
            abilities = UI.transform.Find("Abilities").gameObject;
            furyImage = abilities.transform.Find("Fury").gameObject;
            furyImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().furyTextures[0]; //ustawiamy obrazek na wyszarzony
        }
    }

    void Update()
    {
        if(isFuryBuyed)
        {
            if (Time.time > timeDelay)
            {
                furyImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().furyTextures[1]; //ustawiamy obrazek na dostepny

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    activateAbility();
                    timeDelay = Time.time + abilityDelay;
                    timeDelay2 = Time.time + abilityActiveTime;
                }
            }

            else if (Time.time > timeDelay2)
            {
                if (isActive)
                {
                    furyImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().furyTextures[0]; //ustawiamy obrazek na wyszarzony
                    deactivateAbility();
                }
            }

            else
            {
                furyImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().furyTextures[2]; //ustawiamy obrazek na aktywny
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
