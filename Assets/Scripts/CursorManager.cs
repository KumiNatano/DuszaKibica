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
        if (PauseManager.isPaused || deathm.isDead || finalObject.didPlayerWon){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
