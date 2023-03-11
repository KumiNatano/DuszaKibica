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
    public Camera TPPCamera;


    public bool isTPEnabled = false;
    Quaternion rotationBeforeSwitch;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 vector = new Vector3(0f, high , - 1f); // w tym miejscu zmieniamy Y by ustawic k¹t kamery
        transform.position = player.transform.position + vector;
        transform.LookAt(player);
        TPPCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            isTPEnabled = !isTPEnabled;
            MainCamera.enabled = !MainCamera.enabled;
            TPPCamera.enabled = !TPPCamera.enabled;
            player.gameObject.GetComponent<PlayerControlSystem>().enabled = !player.gameObject.GetComponent<PlayerControlSystem>().enabled;
            player.gameObject.GetComponent<RotateToPointer>().enabled = !player.gameObject.GetComponent<RotateToPointer>().enabled;
            player.gameObject.GetComponent<TPPPlayerMovement>().enabled = !player.gameObject.GetComponent<TPPPlayerMovement>().enabled;
        }

        if(isTPEnabled == true)
        {
            //TPCamera();
        }

        else
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

    void TPCamera()
    {
        Transform cameraTransform = this.transform;
        Transform playerTransform = player.transform;

        this.transform.rotation = Quaternion.Euler(playerTransform.rotation.x - 50, playerTransform.rotation.y, playerTransform.rotation.z);
        this.transform.position = new Vector3(playerTransform.position.x, player.transform.position.y - 5, playerTransform.position.z - 3);

}
    
}
