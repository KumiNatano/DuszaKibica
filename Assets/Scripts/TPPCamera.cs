using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPPCamera : MonoBehaviour
{
    public Vector2 turn;

    private void Update()
    {
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);
    }
}
