using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAnimationBehaviour : MonoBehaviour
{
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindWithTag("player");
    }

    void Update()
    {
        if(Vector3.Distance(this.transform.position, player.GetComponent<Transform>().position) < 2)
        {
            this.gameObject.GetComponent<EnemyAnimationsAndModel>().setIsWalkingFalse();
        }
        else if (transform.hasChanged)
        {
            this.gameObject.GetComponent<EnemyAnimationsAndModel>().setIsWalkingTrue();
            transform.hasChanged = false;
        }
        else if (!transform.hasChanged)
        {
            this.gameObject.GetComponent<EnemyAnimationsAndModel>().setIsWalkingFalse();
        }
    }
}
