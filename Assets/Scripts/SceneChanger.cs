using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] SceneController controller;

    private void Start()
    {
        controller = GameObject.Find("SceneController").GetComponent<SceneController>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        

        if (collision.gameObject.name != "Player")
        {
            return;
        }
        else
        {
            controller.goToNextScene();
        }
    }

}
