using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPointer : MonoBehaviour
{
    [SerializeField] Camera fppCamera;
    [SerializeField] GameObject playerModel;


    
    // Czułość ruchu myszy
    public float mouseSensitivityX = 5f;
    public float mouseSensitivityY = 5f;

    // Transformacja ciała gracza
    public Transform playerBody;

    // Aktualna wartość rotacji kamery wokó osi X
    private float xRotation = 0f;

    private float yRotation = 0f;

    private void Start()
    {

        playerBody = this.transform;

    }

    void Update()
    {       
        if(this.gameObject.GetComponent<PlayerDeathManager>().isDead == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY;
        
        yRotation -= mouseX;
        xRotation = Mathf.Clamp(xRotation - mouseY, -90f, 90f);

        Quaternion cameraRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        fppCamera.transform.localRotation = cameraRotation;
    }
}
