using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAnimationBehaviour : MonoBehaviour
{
    void Update()
    {
        if (transform.hasChanged)
        {
            this.gameObject.GetComponent<EnemyAnimations>().setIsWalkingTrue();
            transform.hasChanged = false;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "player")
        {
            this.gameObject.GetComponent<EnemyAnimations>().setIsWalkingFalse();
        }
    }
}