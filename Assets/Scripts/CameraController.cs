using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    float high = -10;
    //x
    public float leftEgde = -16;
    public float rightEgde = 9;
    //y
    public float upperEgde= 6;
    public float lowerEgde = -6;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 vector = new Vector3(0f, -1f, high); // w tym miejscu zmieniamy Y by ustawic k¹t kamery
        transform.position = player.transform.position + vector;
        transform.LookAt(player);
    }

    // Update is called once per frame
    void Update()
    {
        followPlayer();
    }

    void followPlayer()
    {
        if (player.transform.position.x != transform.position.x || player.transform.position.y != transform.position.y)
        {
            Vector3 newPosition = new Vector3(player.transform.position.x, player.transform.position.y, high);
            if (player.transform.position.x <= leftEgde)
            {
                newPosition.x = leftEgde;
            }
            if (player.transform.position.x >= rightEgde)
            {
                newPosition.x = rightEgde;
            }

            if (player.transform.position.y >= upperEgde)
            {
                newPosition.y = upperEgde;
            }
            if (player.transform.position.y <= lowerEgde)
            {
                newPosition.y = lowerEgde;
            }
        
            transform.position = newPosition;
        }
    }
    
}
