using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    float high = 10;
    //x
    public float leftEgde = -16;
    public float rightEgde = 9;
    //y
    public float upperEgde= 6;
    public float lowerEgde = -6;

    public Camera MainCamera;

    Quaternion rotationBeforeSwitch;

    private PerspectiveController perspectiveController;

    // Start is called before the first frame update
    void Start()
    {
        perspectiveController = GameObject.Find("Player").GetComponent<PerspectiveController>(); // pobieramy perspektywe z kontrolera perspektywy

        Vector3 vector = new Vector3(0f, high , - 1f); // w tym miejscu zmieniamy Y by ustawic k�t kamery
        transform.position = player.transform.position + vector;
        transform.LookAt(player);
    }

    // Update is called once per frame
    void Update()
    {
        if(perspectiveController.cameraMode == 2)
        {
            followPlayer();
        }

    }

    void followPlayer()
    {
        if (player.transform.position.x != transform.position.x || player.transform.position.z != transform.position.z)
        {
            Vector3 newPosition = new Vector3(player.transform.position.x, high, player.transform.position.z);
            /*
            if (player.transform.position.x <= leftEgde)
            {
                newPosition.x = leftEgde;
            }
            if (player.transform.position.x >= rightEgde)
            {
                newPosition.x = rightEgde;
            }

            if (player.transform.position.z >= upperEgde)
            {
                newPosition.y = upperEgde;
            }
            if (player.transform.position.z <= lowerEgde)
            {
                newPosition.y = lowerEgde;
            }
            */
            transform.position = newPosition;
        }
    }
    
}
