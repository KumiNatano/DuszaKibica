using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehaviour : MonoBehaviour
{
    [SerializeField] public Vector3 destinationPosition;
    [SerializeField] public float knifeSpeed = 5f;
    [SerializeField] public int knifeDamage = 5;
    [SerializeField] public string targetTag = "enemy";

    bool reached = false;

    [SerializeField] Vector3 reachedPosition;


    private void Update()
    {
        throwedKnife(destinationPosition);
    }

    void throwedKnife(Vector3 destinationPosition)
    {
        if (reached)
        {
            if (Vector3.Distance(reachedPosition, this.transform.position) > 50) {
                Destroy(this.gameObject);
            }

            this.transform.position += transform.forward * Time.deltaTime * knifeSpeed;

        }
        else
        {
            if (Vector3.Distance(destinationPosition, this.transform.position) < 0.1)
            {
                reachedPosition = this.transform.position;
                reached = true;
            }
            else
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, destinationPosition, knifeSpeed * Time.deltaTime);
            }
        }



    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetTag)
        {
            other.GetComponent<HealthSystem>().TakeDamage(knifeDamage);
            //TakeDamage(other.GetComponent<KnifeBehaviour>().knifeDamage);
        }
    }
    public void setAll(Vector3 destinationPosition, float knifeSpeed, int knifeDamage, string targetTag)
    {
        this.destinationPosition = destinationPosition;
        this.knifeSpeed = knifeSpeed;
        this.knifeDamage = knifeDamage; 
        this.targetTag = targetTag;
    }
}
