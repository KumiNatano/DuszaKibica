using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehaviour : MonoBehaviour
{
    [SerializeField] public Vector3 destinationPosition;
    [SerializeField] public float knifeSpeed = 5f;
    [SerializeField] public int knifeDamage = 5;

    private void Update()
    {
        throwedKnife(destinationPosition);
    }

    void throwedKnife(Vector3 destinationPosition)
    {
        //if (Vector3.Distance(destinationPosition, this.transform.position) < 0.1)
        //{
        //    Destroy(this.gameObject);
        //}
        //else
        //{
            this.transform.position = Vector3.MoveTowards(this.transform.position, destinationPosition, knifeSpeed * Time.deltaTime);
        //}
    }
}
