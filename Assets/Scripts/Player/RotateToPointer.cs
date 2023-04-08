using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPointer : MonoBehaviour
{

    [SerializeField] float baseRotation = 0; //pozwala okreslic poczatkowe ustawienie rotacji 

    private PerspectiveController perspectiveController;

    [SerializeField] Camera tppCamera;
    [SerializeField] Camera fppCamera;
    [SerializeField] GameObject playerModel;
    [SerializeField] GameObject lepetyna;

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

        perspectiveController = this.gameObject.GetComponent<PerspectiveController>();

        playerBody = this.transform;

    }

    void Update()
    {
        if (perspectiveController.cameraMode == 2)
        {
            Cursor.lockState = CursorLockMode.None;
            Vector3 ObjectPositionOnScreen = Camera.main.WorldToViewportPoint(transform.position); //pozycja obiektu na ekranie (inna niz pozycja w swiecie gry)
            Vector3 mousePositionOnScreen = (Vector3)Camera.main.ScreenToViewportPoint(Input.mousePosition); //pozycja myszki na ekranie (inna niz pozycja w swiecie gry)

            float AngleBetween = Mathf.Atan2(ObjectPositionOnScreen.y - mousePositionOnScreen.y, ObjectPositionOnScreen.x - mousePositionOnScreen.x) * Mathf.Rad2Deg; //funckja zwracajaca kat pomiedzy przemnozona przez radiany na stopnie

            this.transform.rotation = Quaternion.Euler(new Vector3(0f, -AngleBetween - baseRotation, 0f));

        }


        else if (perspectiveController.cameraMode == 3)
        {
            Cursor.lockState = CursorLockMode.Locked;
            // Pobierz warto�� ruchu myszy po osi X i przemn� przez czu�o�� ruchu myszy
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;


            // Pobierz warto�� ruchu myszy po osi Y i przemn� przez czu�o�� ruchu myszy
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

            // Odejmij warto�� ruchu myszy po osi Y od aktualnej warto�ci rotacji kamery wok� osi X
            xRotation -= mouseY;

            // Ogranicz warto�� rotacji kamery wok� osi X do zakresu od -90 do 90 stopni
            xRotation = Mathf.Clamp(xRotation, -15f, 30f);

            tppCamera.transform.LookAt(lepetyna.transform);
            //tppCamera.transform.position += new Vector3(0f, xRotation, 0f);


            // Obr�� cia�o gracza za pomoc� wektora Vector3.up (o� Y) przemno�onego przez warto�� ruchu myszy po osi X
            playerBody.Rotate(Vector3.up * mouseX);

        }

        else if (perspectiveController.cameraMode == 1)
        {
            Cursor.lockState = CursorLockMode.Locked;
            // Pobierz warto�� ruchu myszy po osi X i przemn� przez czu�o�� ruchu myszy
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;

            // Pobierz warto�� ruchu myszy po osi Y i przemn� przez czu�o�� ruchu myszy
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

            // Dodaj warto�� ruchu myszy po osi X do aktualnej rotacji kamery wok� osi Y
            yRotation += mouseX;

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
}
