using UnityEngine;

public class ActBillboard : MonoBehaviour
{
    public Vector3 offset = Vector3.zero;
    public bool lockX = false;
    public bool lockY = false;
    public bool lockZ = false;

    void LateUpdate()
    {
        Vector3 camPos = Camera.main.transform.position;

        Vector3 rot = Quaternion.LookRotation(camPos - transform.position).eulerAngles + offset;
        if (lockX)
        {
            rot.x = 0;
        }
        if (lockY)
        {
            rot.y = 0;
        }
        if (lockZ)
        {
            rot.z = 0;
        }
        transform.eulerAngles = rot;
    }
}
