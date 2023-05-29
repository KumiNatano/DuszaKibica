using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    public bool isPaused = false;

    [SerializeField] Canvas PauseUI;
    [SerializeField] Canvas UI;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else if (!isPaused) { 
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        UI.enabled = false;
        PauseUI.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = !isPaused;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        UI.enabled = true;
        PauseUI.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = !isPaused;
    }
}
