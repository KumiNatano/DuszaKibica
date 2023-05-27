using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete]
public class RotateToPointer : MonoBehaviour
{
    // Referencja do kamery gracza
    public Transform playerCamera;
    // Transformacja ciała gracza
    public Transform playerBody;

    // Aktualna wartość rotacji kamery
    public Vector3 viewAngles = Vector3.zero;
    // Czułość ruchu myszy
    public Vector2 sensitivity = new Vector2(5f, 5f);
    // Krzywa reprezentująca zmianę w przyśpieszniu myszy w zalezności od długości delty ruchu myszy
    public AnimationCurve accelerationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

    public bool accelerate = true;


    void Update()
    {
        // wyjebać to trzeba stąd lol
        if(this.gameObject.GetComponent<PlayerDeathManager>().isDead == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        Vector2 input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (accelerate){
            input *= accelerationCurve.Evaluate(input.magnitude);
        }
        input *= sensitivity;
        
        viewAngles.y += input.x;
        viewAngles.x = Mathf.Clamp(viewAngles.x - input.y, -90f, 90f);

        playerBody.localEulerAngles = Vector3.up * viewAngles.y;
        playerCamera.localEulerAngles = Vector3.right * viewAngles.x + Vector3.forward * viewAngles.z;
    }


    [Obsolete("Use sensitivity field instead.")]
    public float mouseSensitivityX => sensitivity.x;
    [Obsolete("Use sensitivity field instead.")]
    public float mouseSensitivityY => sensitivity.y;

    [Obsolete("Use viewAngles field instead.")]
    float xRotation => viewAngles.x;
    [Obsolete("Use viewAngles field instead.")]
    float yRotation => viewAngles.y;
    [Obsolete]
    GameObject playerModel => throw new NotImplementedException();
    [Obsolete("Use playerCamera field instead.")]
    Camera fppCamera => throw new NotImplementedException();
}
