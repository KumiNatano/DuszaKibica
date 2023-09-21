using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    IEnumerator Start()
    {
        Game.activeSpawn = defaultSpawn;
        yield return null;
        Game.activeSpawn.SpawnPlayer();
    }

    [SerializeField] SpawnPoint defaultSpawn;
}
