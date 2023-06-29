using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public static ArenaManager main;

    public LightsManager lightsManager;
    public List<AreaObjectives> areas;
    [SerializeField] GameObject player;
    public int actualArena = -1;
    [SerializeField] bool IsBeetwenArenas;
    public TargetController targetIndicatorArena;
    public bool isTestingMode = false; // funkcja do testowania poziomow, uzywana tylko do testowania konkretnych aren zamiast calego poziomu
    public int whichArenaIsTesting;
    private bool isLightBetweenArenas;
    [HideInInspector] TargetController arenaIndicator;
 
    // Start is called before the first frame update
    void Awake()
    {
        main = this;
        //areas[0].activateArena = true;
        actualArena = 0;

        // Ustawienie wskaźnika na pierwszą arenę.
        arenaIndicator = Instantiate(targetIndicatorArena, player.transform);
        arenaIndicator.GetComponent<TargetController>().target = areas[0].GetLevelTrigger();

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
            // Schowanie wskaźnika na pierwszą arenę.
            arenaIndicator.SetIndicatorRemainsHidden(true);

            IsBeetwenArenas = false;
        }

        if (areas[actualArena].goToNextArena)
        {

            // Ustawienie wskaźnika na następną arenę.
            arenaIndicator.GetComponent<TargetController>().target = areas[actualArena + 1].GetLevelTrigger();
            arenaIndicator.SetIndicatorRemainsHidden(false);
            
            IsBeetwenArenas = true;
            isLightBetweenArenas = false;

            if (actualArena == areas.Count - 1)
            {
                Debug.Log("Nie ma wiecej poziomow!");
            }
            else if (actualArena < areas.Count)
            {
                if (areas[actualArena + 1].GetComponentInChildren<LevelTrigger>().getEnterNewArea())
                {
                    // Schowanie wskaźnika na następną arenę.
                    arenaIndicator.SetIndicatorRemainsHidden(true);
                    
                    ++actualArena;
                    IsBeetwenArenas = false;
                    areas[actualArena].activateArena = true;
                    AstarPath.active.Scan();
                    lightsManager.deactivateLightsInPreviousArea(actualArena * 2 - 2);
                    areas[actualArena-1].disableBorders();
                }
            }
            else
            {
                Debug.Log("Cos sie spierdolilo!");
            }
        }
    }
    //funkcja pozwalajaca testowac poziom od konkretnego poziomu 
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
