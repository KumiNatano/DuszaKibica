using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigGuyChargeWeapon : MonoBehaviour
{
    public bool isInTrigger = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("player"))
        {
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isInTrigger = false;
    }
}