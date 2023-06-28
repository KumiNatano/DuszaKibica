using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsManager : MonoBehaviour
{
    //[SerializeField] ArenaManager arenaManager;
    [SerializeField] List<LampsInArea> areasLights;
    private void Start()
    {
        foreach(var lamps in areasLights)
        {
            lamps.setLightOff();
        }
        areasLights[0].setLightOn();
    }

    public void activateLightsInActualArea(int actualArena)
    {
        areasLights[actualArena].setLightOn();
    }
    public void deactivateLightsInPreviousArea(int actualArena)
    {
        areasLights[actualArena].setLightOff();
    }
}
