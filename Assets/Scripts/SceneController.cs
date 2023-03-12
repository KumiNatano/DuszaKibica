using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] int actuallSceneIndex = 0;
    [SerializeField] List<string> ScenesNames;
    [SerializeField] UpgradeController upgradeController;


    private static SceneController instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }


    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        upgradeController = GameObject.Find("UpgradeController").GetComponent<UpgradeController>();
    }

    public void goToNextScene()
    {
        upgradeController.goingToNextLevel();
        SceneManager.LoadScene(ScenesNames[actuallSceneIndex + 1]);
        actuallSceneIndex++;
    }
        
}
