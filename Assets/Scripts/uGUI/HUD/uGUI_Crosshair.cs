using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class uGUI_Crosshair : MonoBehaviour
{
    [Header("Left arm")]
    [SerializeField] Image lArmImg;
    
    [Header("Right Arm")]
    [SerializeField] Image rArmImg;
    
    [FormerlySerializedAs("DotImg")]
    [Header("Dot")]
    [SerializeField] Image dotImg;
    [SerializeField] private Color grayColor;
    [SerializeField] private Color whiteColor;
    private Color invisibleColor = Color.clear;
    
    //[SerializeField] private float distanceToLightDot = 2.50f;

    //[SerializeField] int layerNumber;
    //int layerMask;

    private GameObject detectionCollider;
    [SerializeField] public bool canAttackLeft = true;
    private bool canAttackRight = true;
    private float coolDown;

    [SerializeField] private bool isLeftColorChangingRightNow = false;
    private bool isRightColorChangingRightNow = false;

    private void Start()
    {
        detectionCollider = GameObject.FindGameObjectWithTag("EnemyDetector");
        //layerMask = 1 << layerNumber;
        //layerMask = ~layerMask;

        coolDown = 1;
    }
    
    void Update()
    {
        //DotDetectionRayCast();
        CrosshairArmsController();
        DotDetectionCollider();
    }

    public bool synchronizationLeft = true;
    public bool synchronizationRight = true;
    
    void CrosshairArmsController()
    {

        //poki co bierze do obu "armow" to samo canAttack, poniewaz nie ma rozroznienia na lewa i prawa
        canAttackLeft = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerAttack>().canAttack;
        canAttackRight = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerAttack>().canAttack;

        //lewa
        if (isLeftColorChangingRightNow == false && canAttackLeft == false && synchronizationLeft == true)
        {
            isLeftColorChangingRightNow = true;
            lArmImg.color = invisibleColor;
            StartCoroutine(ColorLerp(lArmImg, whiteColor, coolDown));
        }
        
        //prawa
        if (isRightColorChangingRightNow == false && canAttackRight == false && synchronizationRight == true)
        {
            isRightColorChangingRightNow = true;
            rArmImg.color = invisibleColor;
            StartCoroutine(ColorLerp(rArmImg, whiteColor, coolDown));
        }
    }
    
    IEnumerator ColorLerp(Image imgOfArm, Color endValue, float duration)
    {
        if (imgOfArm == lArmImg)
        {
            synchronizationLeft = false;
        }
        else if (imgOfArm == rArmImg)
        {
            synchronizationRight = false;
        }
        
        float time = 0;
        Color startValue = imgOfArm.color;
        while (time < duration)
        {
            imgOfArm.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        imgOfArm.color = endValue;

        if (imgOfArm == lArmImg)
        {

            canAttackLeft = true;
            isLeftColorChangingRightNow = false;
        }
        else if (imgOfArm == rArmImg)
        {
            canAttackRight = true;
            isRightColorChangingRightNow = false;
        }
    }
    
    void DotDetectionCollider()
    {
        if (detectionCollider.GetComponent<EnemyDetector>().GetIsEnemyDetected())
        {
            dotImg.color = whiteColor;
        }
        else
        {
            dotImg.color = grayColor;
        }
    }
    /*
    void DotDetectionRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, distanceToLightDot, layerMask))
        {
            if (hit.collider.CompareTag("enemy"))
            {
                dotImg.color = enemyDetectedColor;
            }
            else
            {
                dotImg.color = enemyUndetectedColor;
            }
        }            
        else
        {
            dotImg.color = enemyUndetectedColor;
        }
    }*/
}
