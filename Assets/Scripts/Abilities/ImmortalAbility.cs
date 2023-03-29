using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImmortalAbility : MonoBehaviour
{
    [SerializeField] UpgradeController upgradeController;
    bool isImmortalBuyed = false;
    bool isActive = false;

    float timeDelay = 0f;
    [SerializeField] float abilityDelay = 15f;

    float timeDelay2 = 0f;
    [SerializeField] float abilityActiveTime = 5f;

    [SerializeField] GameObject UI = null;
    [SerializeField] GameObject abilities = null;
    [SerializeField] GameObject immortalImage = null;

    void Start()
    {
        upgradeController = GameObject.Find("UpgradeController").GetComponent<UpgradeController>();
        if (upgradeController.Immortal == true)
        {
            isImmortalBuyed = true;
            UI = GameObject.Find("UI");
            abilities = UI.transform.Find("Abilities").gameObject;
            immortalImage = abilities.transform.Find("Immortal").gameObject;
            immortalImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().immortalTextures[0]; //ustawiamy obrazek na wyszarzony
        }
    }

    void Update()
    {
        if (isImmortalBuyed)
        {
            if (Time.time > timeDelay)
            {
                immortalImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().immortalTextures[1]; //ustawiamy obrazek na dostepny

                if (Input.GetKeyDown(KeyCode.Alpha1))
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
                    immortalImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().immortalTextures[0]; //ustawiamy obrazek na wyszarzony
                    deactivateAbility();
                }
            }

            else
            {
                immortalImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().immortalTextures[2]; //ustawiamy obrazek na aktywny
            }
        }
    }

    void activateAbility()
    {
        isActive = true;
        this.gameObject.GetComponent<HealthSystem>().setImmortal(5, 15);
        //Debug.Log("tu wstawic zeby dalo niesmiertelnosc");
    }

    void deactivateAbility()
    {
        isActive = false;
        //Debug.Log("tu wstawic zeby zabralo niesmiertelnosc");
    }
}
