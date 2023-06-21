using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Obsolete]
public class KnifeAbility : MonoBehaviour
{
    [SerializeField] UpgradeController upgradeController;
    bool isKnifeBuyed = false;

    float timeDelay = 0f;
    [SerializeField] float abilityDelay = 5f;
    [SerializeField] GameObject knifePrefab;
    [SerializeField] public float knifeSpeed = 5f;
    [SerializeField] public int knifeDamage = 50;
    public bool busy = false;
    public float busyDelay = 2f;

    Vector3 mouseWorldPosition;

    [SerializeField] Camera mainCamera;

    [SerializeField] GameObject UI = null;
    [SerializeField] GameObject abilities = null;
    [SerializeField] GameObject knifeImage = null;

    Player player;
    PlayerAttack attack;

    void Start()
    {

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        upgradeController = GameObject.Find("UpgradeController").GetComponent<UpgradeController>();
        if (upgradeController.Knife == true)
        {
            isKnifeBuyed = true;
            UI = GameObject.Find("UI");
            abilities = UI.transform.Find("Abilities").gameObject;
            knifeImage = abilities.transform.Find("Knife").gameObject;
            knifeImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().knifeTextures[0]; //ustawiamy obrazek na wyszarzony
        }
        player = gameObject.GetComponent<Player>();
        attack = player.GetModule<PlayerAttack>();
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
        if (!busy)
        {
            StartCoroutine(ActivateSeq());
        }
    }

    IEnumerator ActivateSeq()
    {
        busy = true;
        Camera mainCamera = Camera.main;

        knifeImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().knifeTextures[0]; //ustawiamy obrazek na wyszarzony

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            mouseWorldPosition = raycastHit.point;
        }

        attack.rightArm.Block();
        attack.rightArm.Interrupt();

        player.viewmodel.ThrowKnife();
        yield return new WaitForSeconds(0.967f);
        GameObject knife = Instantiate(knifePrefab, new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z),
            mainCamera.transform.rotation * Quaternion.Euler(-90, 90, 0));
        yield return null;
        attack.rightArm.Unblock();

        var behaviour = knife.GetComponent<KnifeBehaviour>();
        behaviour.SetDirection(mainCamera.transform.forward);
        behaviour.flySpeed = knifeSpeed;
        behaviour.hitDamage = knifeDamage;
        yield return new WaitForSeconds(busyDelay);
        busy = false;
    }

}
