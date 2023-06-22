using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float hideDistance = 5f; // Odległość przy której noże w stronę przeciwnika znikają.

    private void SetIndicatorActive(bool isActive) {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(isActive);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var direction = target.position - transform.position;
        var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + 180;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up); // Vector3.up - wzdłuż osi y

        // Zniknięcie wskaźnika gdy jesteśmy blisko przeciwnika.
        if (direction.magnitude <= hideDistance) {
            SetIndicatorActive(false);
        } else {
            SetIndicatorActive(true);
        }

        // Usunięcie wskaźnika gdy przeciwnik śpi.
        if (target.GetComponent<HealthSystem>().CheckIfAlive() == false) {
            Destroy(gameObject);
        }
    }
}
