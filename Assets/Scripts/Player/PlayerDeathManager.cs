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
    public bool isDead = false;

    private void Start()
    {
         actuallScene = SceneManager.GetActiveScene().name;
    }
    public void PlayerDeath()
    {
        isDead = true;
        Cursor.lockState = CursorLockMode.None;
        restartImage.SetActive(true);
        Time.timeScale = 0;
        PlayerHPBar.SetActive(false);
        PlayerStaminaBar.SetActive(false);
        //Destroy(playerModel);
    }

    public void ClickingRestartButton()
    {
        isDead = false;
        SceneManager.LoadScene(actuallScene);
        Time.timeScale = 1;
    }
}
