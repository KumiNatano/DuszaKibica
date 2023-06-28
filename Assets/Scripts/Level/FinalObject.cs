using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class FinalObject : MonoBehaviour
{
    [SerializeField] private EnemyQueueManager enemyQueueManager;
    
    string actuallScene;
    [SerializeField] GameObject winImage;
    [SerializeField] GameObject PlayerHPBar;
    [SerializeField] GameObject PlayerStaminaBar;
    public bool didPlayerWon = false;
    
    private void Start()
    {
        actuallScene = SceneManager.GetActiveScene().name;
    }
    public void PlayerWon()
    {
        Cursor.lockState = CursorLockMode.None;
        winImage.SetActive(true);
        Time.timeScale = 0;
        PlayerHPBar.SetActive(false);
        PlayerStaminaBar.SetActive(false);
    }

    public void ClickingRestartButton()
    {
        SceneManager.LoadScene(actuallScene);
        Time.timeScale = 1;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("player") && enemyQueueManager.enemies.Count == 0 && didPlayerWon == false)
        {
            didPlayerWon = true;
            PlayerWon();
        }
    }
}
