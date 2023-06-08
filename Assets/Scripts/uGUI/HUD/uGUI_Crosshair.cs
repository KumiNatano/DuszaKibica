using System;
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
    private Color invisible = Color.clear;
    
    //[SerializeField] private float distanceToLightDot = 2.50f;

    //[SerializeField] int layerNumber;
    //int layerMask;

    private GameObject detectionCollider;
    private bool canAttackLeft;
    private bool canAttackRight;
    private float coolDown;
    
    //
    private Color currentColor;
    //
    
    private void Start()
    {
        detectionCollider = GameObject.FindGameObjectWithTag("EnemyDetector");
        //layerMask = 1 << layerNumber;
        //layerMask = ~layerMask;

        coolDown = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerAttack>().cooldown;
        
        //
        currentColor = whiteColor;
        //
    }
    
    void Update()
    {
        //DotDetectionRayCast();
        CrosshairArmsController();
        DotDetectionCollider();
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (currentColor == invisible)
            {
                currentColor = whiteColor;
            }
            else
            {
                currentColor = invisible;
            }
        }

        lArmImg.color = Color.Lerp(lArmImg.color, currentColor, Time.deltaTime * 10);
        print(coolDown);
    }

    void CrosshairArmsController()
    {
        //poki co bierze do obu "armow" to samo canAttack, poniewaz nie ma rozroznienia na lewa i prawa
        canAttackLeft = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerAttack>().canAttack;
        canAttackRight = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerAttack>().canAttack;
        
        //lewa
        // if (canAttackLeft == true)
        // {
        //     lArmImg.color = whiteColor;
        // }
        // else
        // {
        //     lArmImg.color = grayColor;
        // }
        
        //prawa
        if (canAttackRight == true)
        {
            rArmImg.color = whiteColor;
        }
        else
        {
            rArmImg.color = grayColor;
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
