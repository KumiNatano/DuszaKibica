using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    float high = -10;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 vector = new Vector3(0f, 0f, high);
        transform.position = player.transform.position + vector;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x != transform.position.x || player.transform.position.y != transform.position.y) 
        {
            Vector3 vector1 = new Vector3(player.transform.position.x, player.transform.position.y, high);
            transform.position = vector1;
        }
        transform.LookAt(player);
    }
}
