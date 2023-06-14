using System;
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
        this.gameObject.GetComponent<Player>().viewmodel.Drink();
        yield return new WaitForSeconds(2.967f);
        isInAnimation = false;
        furyImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().furyTextures[2]; //ustawiamy obrazek na aktywny

        Debug.LogError("Zmiana cooldowna PlayerAttack i stamina");
        
        yield return new WaitForSeconds(abilityActiveTime);
        deactivateAbility();
        furyImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().furyTextures[0]; //ustawiamy obrazek na wyszarzony
        isActive = false;
        isRecharging = true;
        
        yield return new WaitForSeconds(abilityDelay);
        isRecharging = false;
        furyImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().furyTextures[1]; //ustawiamy obrazek na dostepny

    }

    void deactivateAbility()
    {
        Debug.LogError("Zmiana cooldowna PlayerAttack");
        isActive = false;
        Debug.Log("tu wstawic zeby zmniejszylo szybkosc");
    }
    
}
