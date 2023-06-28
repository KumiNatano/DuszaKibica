using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    [SerializeField] LightsManager lightsManager;
    [SerializeField] List<AreaObjectives> areas;
    [SerializeField] GameObject player;
    [SerializeField] int actualArena = -1;
    [SerializeField] bool IsBeetwenArenas;
    public bool isTestingMode = false; // funkcja do testowania poziom�w, u�ywana tylko do testowania konkretnych aren zamiast ca�ego poziomu
    public int whichArenaIsTesting;
    private bool isLightBetweenArenas;

    // Start is called before the first frame update
    void Awake()
    {
        //areas[0].activateArena = true;
        actualArena = 0;
        IsBeetwenArenas = false;
        isLightBetweenArenas = false;
        if (isTestingMode)
        {
            testMode(whichArenaIsTesting);
            AstarPath.active.Scan();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLightBetweenArenas & IsBeetwenArenas)
        {
            isLightBetweenArenas = true;
            lightsManager.activateLightsInActualArea(actualArena * 2 + 1);
            lightsManager.activateLightsInActualArea(actualArena * 2 + 2);

        }
        

        if (areas[0].GetComponentInChildren<LevelTrigger>().getEnterNewArea())
        {
            areas[actualArena].activateArena = true;
            IsBeetwenArenas = false;
        }
        if (areas[actualArena].goToNextArena)
        {
            
            IsBeetwenArenas = true;
            isLightBetweenArenas = false;
            if (actualArena == areas.Count - 1)
            {
                Debug.Log("Nie ma wi�cej poziom�w!");
            }
            else if (actualArena <= areas.Count)
            {
                if (areas[actualArena + 1].GetComponentInChildren<LevelTrigger>().getEnterNewArea())
                {
                    ++actualArena;
                    IsBeetwenArenas = false;
                    areas[actualArena].activateArena = true;
                    AstarPath.active.Scan();
                    lightsManager.deactivateLightsInPreviousArea(actualArena * 2 - 2);
                }
            }
            else
            {
                Debug.Log("Co� si� spierdoli�o!");
            }
        }
    }
    //funkcja pozwalaj�ca testowa� poziom od konkretnego poziomu 
    public void testMode(int whichArena)
    {
        actualArena = whichArena;
        for (int i = 0; i < whichArena; i++)
        {
            areas[i].setArenaIsCompleted();

        }
            
        player.transform.position = areas[whichArena].getTestArenaPosition() + new Vector3(0f, -1.88f, 0f);
        areas[actualArena].activateArena = true;
        areas[actualArena].startArena();
    }
}
