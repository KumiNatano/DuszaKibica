using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    public Vector3 offset = new Vector3(0f, 1.4f, 1f);
    
    private bool isEnemyDetected = false;
    public bool GetIsEnemyDetected()
    {
        return isEnemyDetected;
    }
    
    
    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("enemy"))
        {
            isEnemyDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isEnemyDetected = false;
    }

    private void FixedUpdate()
    {
        GetComponent<SphereCollider>().center = Quaternion.Euler(0, Player.GetComponent<PlayerCameraController>().playerCamera.viewAngles.y, 0) * offset;
    }
}
