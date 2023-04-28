using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] bool enterNewArea;

    private void Start()
    {
        enterNewArea = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "player")
        {
            enterNewArea = true;
        }
    }
    public bool getEnterNewArea()
    {
        return enterNewArea;
    }
}
