using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPointer : MonoBehaviour
{
    [SerializeField] Camera fppCamera;
    [SerializeField] GameObject playerModel;

    // Czu�o�� ruchu myszy
    public float mouseSensitivityX = 100f;
    public float mouseSensitivityY = 100f;

    // Transformacja cia�a gracza
    public Transform playerBody;

    // Aktualna warto�� rotacji kamery wok� osi X
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

        // Pobierz warto�� ruchu myszy po osi X i przemn� przez czu�o�� ruchu myszy
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;

        // Pobierz warto�� ruchu myszy po osi Y i przemn� przez czu�o�� ruchu myszy
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        // Dodaj warto�� ruchu myszy po osi X do aktualnej rotacji kamery wok� osi Y
        yRotation -= mouseX;

        // Odejmij warto�� ruchu myszy po osi Y od aktualnej warto�ci rotacji kamery wok� osi X
        xRotation -= mouseY;

        // Ogranicz warto�� rotacji kamery wok� osi X do zakresu od -90 do 90 stopni
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Utw�rz nowy Quaternion na podstawie aktualnej rotacji kamery
        Quaternion cameraRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Obr�� cia�o gracza wok� osi Y zgodnie z ruchem myszy
        playerBody.Rotate(Vector3.up * mouseX);

        // Ustaw now� rotacj� kamery
        fppCamera.transform.localRotation = cameraRotation;
    }
}
