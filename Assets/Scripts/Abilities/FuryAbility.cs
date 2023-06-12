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

    public bool isInAnimation = false;
    private bool isRecharging = false;
    
    void Start()
    {
        upgradeController = GameObject.Find("UpgradeController").GetComponent<UpgradeController>();
        if (upgradeController.Fury == true)
        {
            isFuryBuyed = true;
            UI = GameObject.Find("UI");
            abilities = UI.transform.Find("Abilities").gameObject;
            furyImage = abilities.transform.Find("Fury").gameObject;
            furyImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().furyTextures[1];
        }
    }

    void Update()
    {
        if (isFuryBuyed == true && Input.GetKeyDown(KeyCode.Alpha2) && isRecharging == false && isActive == false)
        {
            StartCoroutine(activateAbility());
        }
    }

    private float previousCooldown;

    IEnumerator activateAbility()
    {
        isActive = true;

        isInAnimation = true;
        this.gameObject.GetComponent<PlayerAnimations>().setIsDrinkingTrue();
        yield return new WaitForSeconds(2.967f);
        this.gameObject.GetComponent<PlayerAnimations>().setIsDrinkingFalse();
        isInAnimation = false;
        furyImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().furyTextures[2]; //ustawiamy obrazek na aktywny
        
        previousCooldown = this.gameObject.GetComponent<PlayerAttack>().cooldown;
        this.gameObject.GetComponent<PlayerAttack>().cooldown = furyAttackCooldown;
        this.gameObject.GetComponent<StaminaSystem>().staminaAmount = furyAttackStamina;
        this.gameObject.GetComponent<PlayerAnimations>().setIsFuryTrue();
        
        yield return new WaitForSeconds(abilityActiveTime);
        deactivateAbility();
        furyImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().furyTextures[0]; //ustawiamy obrazek na wyszarzony
        this.gameObject.GetComponent<PlayerAnimations>().setIsFuryFalse();
        isActive = false;
        isRecharging = true;
        
        yield return new WaitForSeconds(abilityDelay);
        isRecharging = false;
        furyImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().furyTextures[1]; //ustawiamy obrazek na dostepny

    }

    void deactivateAbility()
    {
        this.gameObject.GetComponent<PlayerAttack>().cooldown = previousCooldown;
        isActive = false;
        this.gameObject.GetComponent<PlayerAnimations>().animator.speed = 1;
        //Debug.Log("tu wstawic zeby zmniejszylo szybkosc");
    }
    
}
