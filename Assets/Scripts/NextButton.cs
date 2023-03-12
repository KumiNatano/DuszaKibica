using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    [SerializeField] SceneController sceneController;

    void Start()
    {
        sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        this.gameObject.GetComponent<Button>().onClick.AddListener(sceneController.goToNextScene);
    }

    
}
