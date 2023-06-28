using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public PauseManager pause;
    public PlayerDeathManager deathm;
    public FinalObject finalObject;
    void Update()
    {
        if (GameTips.isShowing)
        {
            if (GameTips.blockCursor)
            {
                Lock();
            }
            else
            {
                Unlock();
            }
        }
        else
        {
            if ((PauseManager.isPaused || deathm.isDead || finalObject.didPlayerWon))
            {
                Unlock();
            }
            else
            {
                Lock();
            }
        }
    }

    void Lock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Unlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
