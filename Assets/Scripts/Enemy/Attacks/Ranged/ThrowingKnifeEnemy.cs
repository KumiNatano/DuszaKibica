using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnifeEnemy : MonoBehaviour
{
    [SerializeField] public Vector3 destinationPosition;
    [SerializeField] public float knifeSpeed = 5f;
    [SerializeField] public int knifeDamage = 5;

    bool reached = false;

    private void Update()
    {
        throwedKnife(destinationPosition);
    }

    void throwedKnife(Vector3 destinationPosition)
    {
        if (reached)
        {
            this.transform.position += transform.forward * Time.deltaTime * knifeSpeed;
        }
        else
        {
            if (Vector3.Distance(destinationPosition, this.transform.position) < 0.1)
            {
                reached = true;
            }
            else
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, destinationPosition, knifeSpeed * Time.deltaTime);
            }
        }



    }
}
