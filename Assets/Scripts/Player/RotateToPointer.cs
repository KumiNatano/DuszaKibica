using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPointer : MonoBehaviour
{

    [SerializeField] float baseRotation = 0; //pozwala okreslic poczatkowe ustawienie rotacji 
    private bool isTopDownEnabled = false; // czy wlaczony jest widok top-down
    private int cameraMode = 3;
    [SerializeField] Camera tppCamera;
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


        isTopDownEnabled = this.gameObject.GetComponent<PerspectiveController>().isTopDownEnabled; // pobieramy perspektywe z kontrolera perspektywy

        cameraMode = this.gameObject.GetComponent<PerspectiveController>().cameraMode;

        playerBody = this.transform;

        if (isTopDownEnabled == false)
        {
            // Zablokuj kursor w �rodku ekranu
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

    void Update()
    {
        if(isTopDownEnabled == true) 
        {

            Vector3 ObjectPositionOnScreen = Camera.main.WorldToViewportPoint(transform.position); //pozycja obiektu na ekranie (inna niz pozycja w swiecie gry)
            Vector3 mousePositionOnScreen = (Vector3)Camera.main.ScreenToViewportPoint(Input.mousePosition); //pozycja myszki na ekranie (inna niz pozycja w swiecie gry)

            float AngleBetween = Mathf.Atan2(ObjectPositionOnScreen.y - mousePositionOnScreen.y, ObjectPositionOnScreen.x - mousePositionOnScreen.x) * Mathf.Rad2Deg; //funckja zwracajaca kat pomiedzy przemnozona przez radiany na stopnie

            this.transform.rotation = Quaternion.Euler(new Vector3(0f, -AngleBetween - baseRotation, 0f));

        }
        else
        {
            if (cameraMode == 3) {

                // Pobierz warto�� ruchu myszy po osi X i przemn� przez czu�o�� ruchu myszy
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;


                // Pobierz warto�� ruchu myszy po osi Y i przemn� przez czu�o�� ruchu myszy
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

                // Odejmij warto�� ruchu myszy po osi Y od aktualnej warto�ci rotacji kamery wok� osi X
                xRotation -= mouseY;

                // Ogranicz warto�� rotacji kamery wok� osi X do zakresu od -90 do 90 stopni
                xRotation = Mathf.Clamp(xRotation, -15f, 30f);

                //dlaczego po prostu nie obrocic playerBody, ktore zawiera w sobie kamere?
                //otoz nie dziala i nie mam pojecia dlaczego, postac blokuje sie w jednej pozycji obrotu
                //wlasnie dlatego jest takie obejscie, ale I guess, ze ma sens, poniewaz w przyszlosci 
                //raczej bedziemy chcieli zeby sama glowa patrzyla w gore
                tppCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerModel.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);


                // Obr�� cia�o gracza za pomoc� wektora Vector3.up (o� Y) przemno�onego przez warto�� ruchu myszy po osi X
                playerBody.Rotate(Vector3.up * mouseX);

            }

            else if(cameraMode == 1)
            {

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
                Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0f);

                // Ustaw now� rotacj� kamery
                tppCamera.transform.localRotation = rotation;

                // Obr�� cia�o gracza wok� osi Y zgodnie z ruchem myszy
                playerBody.transform.Rotate(Vector3.up * mouseX);
               

            }


        }
    }
}
