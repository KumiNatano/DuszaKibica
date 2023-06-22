using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    public static bool isPaused = false;
    public static bool dirty = false;
    static Canvas pui;
    static Canvas hui;
    

    [SerializeField] Canvas PauseUI;
    [SerializeField] Canvas UI;

    private void Awake()
    {

        pui = PauseUI;
        hui = UI;
    }
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

    public static void PauseGame()
    {
        if (dirty)
        { return; }
        Time.timeScale = 0;
        hui.enabled = false;
        pui.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = !isPaused;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1;
        hui.enabled = true;
        pui.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = !isPaused;
    }
}
