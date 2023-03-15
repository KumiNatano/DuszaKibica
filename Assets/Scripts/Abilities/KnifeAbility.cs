using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        upgradeController = GameObject.Find("UpgradeController").GetComponent<UpgradeController>();
        if (upgradeController.Knife == true)
        {
            isKnifeBuyed = true;
        }
    }

    void Update()
    {

        if (isKnifeBuyed)
        {

            if (Time.time > timeDelay)
            {
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
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            mouseWorldPosition = raycastHit.point;
        }

        GameObject knife = Instantiate(knifePrefab, new Vector3(this.transform.position.x, 1, this.transform.position.z), this.transform.rotation);
        knife.GetComponent<KnifeBehaviour>().destinationPosition = new Vector3(mouseWorldPosition.x*1, 1, mouseWorldPosition.z*1); //przemno¿one przez 100 ¿eby lecia³o dalej
        knife.GetComponent<KnifeBehaviour>().knifeSpeed = knifeSpeed;
        knife.GetComponent<KnifeBehaviour>().knifeDamage = knifeDamage;
    }

}
