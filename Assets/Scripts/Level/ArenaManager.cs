using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    [SerializeField] List<AreaObjectives> areas;
    [SerializeField] int actualArena = -1;
    [SerializeField] bool IsBeetwenArenas;
    public bool isTestingMode = false; // funkcja do testowania poziom�w, u�ywana tylko do testowania konkretnych aren zamiast ca�ego poziomu
    public int whichArenaIsTesting;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        //areas[0].activateArena = true;
        actualArena = 0;
        IsBeetwenArenas = false;
        if (isTestingMode)
            testMode(whichArenaIsTesting);
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
                Debug.Log("Nie ma wi�cej poziom�w!");
            }
            else if (actualArena <= areas.Count)
            {
                if (areas[actualArena + 1].GetComponentInChildren<LevelTrigger>().getEnterNewArea())
                {
                    ++actualArena;
                    areas[actualArena].activateArena = true;
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
            areas[i].completeArena();
        player.transform.position = areas[whichArena].getTestArenaPosition();

    }
}
