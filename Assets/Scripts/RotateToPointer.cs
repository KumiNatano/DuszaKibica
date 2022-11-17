using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPointer : MonoBehaviour
{

    [SerializeField] float baseRotation = 0; //pozwala okreslic poczatkowe ustawienie rotacji 

    void Update()
    {
        Vector2 ObjectPositionOnScreen = Camera.main.WorldToViewportPoint(transform.position); //pozycja obiektu na ekranie (inna niz pozycja w swiecie gry)
        Vector2 mousePositionOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition); //pozycja myszki na ekranie (inna niz pozycja w swiecie gry)

        float AngleBetween = Mathf.Atan2(ObjectPositionOnScreen.y - mousePositionOnScreen.y, ObjectPositionOnScreen.x - mousePositionOnScreen.x) * Mathf.Rad2Deg; //funckja zwracajaca kat pomiedzy przemnozona przez radiany na stopnie

        this.transform.rotation = Quaternion.Euler(new Vector3 (0f, 0f, AngleBetween + baseRotation));
    }
}
