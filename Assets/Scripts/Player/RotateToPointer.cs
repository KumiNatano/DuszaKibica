using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPointer : MonoBehaviour
{
    [SerializeField] Camera fppCamera;
    [SerializeField] GameObject playerModel;

    // Czu³oœæ ruchu myszy
    public float mouseSensitivityX = 100f;
    public float mouseSensitivityY = 100f;

    // Transformacja cia³a gracza
    public Transform playerBody;

    // Aktualna wartoœæ rotacji kamery wokó³ osi X
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

        // Pobierz wartoœæ ruchu myszy po osi X i przemnó¿ przez czu³oœæ ruchu myszy
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;

        // Pobierz wartoœæ ruchu myszy po osi Y i przemnó¿ przez czu³oœæ ruchu myszy
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        // Dodaj wartoœæ ruchu myszy po osi X do aktualnej rotacji kamery wokó³ osi Y
        yRotation -= mouseX;

        // Odejmij wartoœæ ruchu myszy po osi Y od aktualnej wartoœci rotacji kamery wokó³ osi X
        xRotation -= mouseY;

        // Ogranicz wartoœæ rotacji kamery wokó³ osi X do zakresu od -90 do 90 stopni
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Utwórz nowy Quaternion na podstawie aktualnej rotacji kamery
        Quaternion cameraRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Obróæ cia³o gracza wokó³ osi Y zgodnie z ruchem myszy
        playerBody.Rotate(Vector3.up * mouseX);

        // Ustaw now¹ rotacjê kamery
        fppCamera.transform.localRotation = cameraRotation;
    }
}
