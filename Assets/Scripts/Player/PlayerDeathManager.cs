using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerDeathManager : MonoBehaviour
{
    string actuallScene;
    [SerializeField] GameObject restartImage;
    [SerializeField] GameObject PlayerHPBar;
    [SerializeField] GameObject PlayerStaminaBar;

    private void Start()
    {
         actuallScene = SceneManager.GetActiveScene().name;
    }
    public void PlayerDeath()
    {
        restartImage.SetActive(true);
        Time.timeScale = 0;
        PlayerHPBar.SetActive(false);
        PlayerStaminaBar.SetActive(false);
    }

    public void ClickingRestartButton()
    {
        Debug.Log("tes");
        SceneManager.LoadScene(actuallScene);
        Time.timeScale = 1;
    }
}