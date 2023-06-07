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
    [SerializeField] private Color enemyUndetectedColor;
    [SerializeField] private Color enemyDetectedColor;
    
    //[SerializeField] private float distanceToLightDot = 2.50f;

    //[SerializeField] int layerNumber;
    //int layerMask;

    private GameObject detectionCollider;
    
    private void Start()
    {
        detectionCollider = GameObject.FindGameObjectWithTag("EnemyDetector");
        //layerMask = 1 << layerNumber;
        //layerMask = ~layerMask;
    }

    void Update()
    {
        //DotDetectionRayCast();
        DotDetectionCollider();
    }

    void DotDetectionCollider()
    {
        if (detectionCollider.GetComponent<EnemyDetector>().GetIsEnemyDetected())
        {
            dotImg.color = enemyDetectedColor;
        }
        else
        {
            dotImg.color = enemyUndetectedColor;
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
