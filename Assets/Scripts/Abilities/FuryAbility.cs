using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuryAbility : MonoBehaviour
{
    public float speedBonus = 1.35f;

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

    Player player;
    PlayerAttack attack;
    

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
        player = gameObject.GetComponent<Player>();
        attack = player.GetModule<PlayerAttack>();
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
        float sm = 1f / speedBonus;

        isActive = true;

        isInAnimation = true;
        player.viewmodel.Drink();
        yield return new WaitForSeconds(2.967f);
        isInAnimation = false;
        furyImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().furyTextures[2]; //ustawiamy obrazek na aktywny

        attack.leftArm.SetSpeed(sm);
        attack.rightArm.SetSpeed(sm);
        
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
        attack.leftArm.SetSpeed(1);
        attack.rightArm.SetSpeed(1);
        isActive = false;
        Debug.Log("tu wstawic zeby zmniejszylo szybkosc");
    }
    
}
