using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    [SerializeField] List<AreaObjectives> areas;
    [SerializeField] int actualArena = 0;
    [SerializeField] bool IsBeetwenArenas;
    // Start is called before the first frame update
    void Awake()
    {
        areas[0].activateArena = true;
        actualArena = 0;
        IsBeetwenArenas = false;
    }

    // Update is called once per frame
    void Update()
    {
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
                }
            }
            else
            {
                Debug.Log("Coœ siê spierdoli³o!");
            }
        }
    }
}
