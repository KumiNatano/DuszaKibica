using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    [SerializeField] List<AreaObjectives> areas;
    [SerializeField] GameObject player;
    [SerializeField] int actualArena = -1;
    [SerializeField] bool IsBeetwenArenas;
    public bool isTestingMode = false; // funkcja do testowania poziomów, u¿ywana tylko do testowania konkretnych aren zamiast ca³ego poziomu
    public int whichArenaIsTesting;
    
    // Start is called before the first frame update
    void Awake()
    {
        //areas[0].activateArena = true;
        actualArena = 0;
        IsBeetwenArenas = false;
        if (isTestingMode)
        {
            testMode(whichArenaIsTesting);
            AstarPath.active.Scan();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (areas[0].GetComponentInChildren<LevelTrigger>().getEnterNewArea())
        {
            areas[actualArena].activateArena = true;
        }
        if (areas[actualArena].goToNextArena)
        {
            IsBeetwenArenas = true;
            if (actualArena == areas.Count - 1)
            {
                Debug.Log("Nie ma wiêcej poziomów!");
            }
            else if (actualArena <= areas.Count)
            {
                if (areas[actualArena + 1].GetComponentInChildren<LevelTrigger>().getEnterNewArea())
                {
                    ++actualArena;
                    areas[actualArena].activateArena = true;
                    AstarPath.active.Scan();
                }
            }
            else
            {
                Debug.Log("Coœ siê spierdoli³o!");
            }
        }
    }
    //funkcja pozwalaj¹ca testowaæ poziom od konkretnego poziomu 
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
