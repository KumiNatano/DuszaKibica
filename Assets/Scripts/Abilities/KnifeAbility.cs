using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeAbility : MonoBehaviour
{
    [SerializeField] UpgradeController upgradeController;
    bool isKnifeBuyed = false;

    float timeDelay = 0f;
    [SerializeField] float abilityDelay = 5f;
    [SerializeField] GameObject knifePrefab;
    [SerializeField] public float knifeSpeed = 5f;
    [SerializeField] public int knifeDamage = 50;

    Vector3 mouseWorldPosition;

    [SerializeField] Camera mainCamera;

    [SerializeField] GameObject UI = null;
    [SerializeField] GameObject abilities = null;
    [SerializeField] GameObject knifeImage = null;

    void Start()
    {

        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        upgradeController = GameObject.Find("UpgradeController").GetComponent<UpgradeController>();
        if (upgradeController.Knife == true)
        {
            isKnifeBuyed = true;
            UI = GameObject.Find("UI");
            abilities = UI.transform.Find("Abilities").gameObject;
            knifeImage = abilities.transform.Find("Knife").gameObject;
            knifeImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().knifeTextures[0]; //ustawiamy obrazek na wyszarzony
        }
    }

    void Update()
    {

        if (isKnifeBuyed)
        {

            if (Time.time > timeDelay)
            {
                knifeImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().knifeTextures[1]; //ustawiamy obrazek na dostepny

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    activateAbility();
                    timeDelay = Time.time + abilityDelay;
                }
            }
        }
    }

    void activateAbility()
    {
        knifeImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().knifeTextures[0]; //ustawiamy obrazek na wyszarzony

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            mouseWorldPosition = raycastHit.point;
        }

        GameObject knife = Instantiate(knifePrefab, new Vector3(this.transform.position.x, 1, this.transform.position.z), this.transform.rotation);
        knife.GetComponent<KnifeBehaviour>().destinationPosition = new Vector3(mouseWorldPosition.x*1, 1, mouseWorldPosition.z*1);
        knife.GetComponent<KnifeBehaviour>().knifeSpeed = knifeSpeed;
        knife.GetComponent<KnifeBehaviour>().knifeDamage = knifeDamage;
    }

}
