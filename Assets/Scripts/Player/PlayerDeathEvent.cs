using System.Collections;
using UnityEngine;

public class PlayerDeathEvent : MonoBehaviour
{
    public float preloadTime = 5f;
    public GameObject deathScreen;

    void Start()
    {
        Player.main.living.onDeath += OnPlayerDeath;
    }

    void OnPlayerDeath()
    {
        StartCoroutine(PlayerDeathCoroutine(Player.main));
    }
    IEnumerator PlayerDeathCoroutine(Player p)
    {
        deathScreen.SetActive(true);

        yield return new WaitForSeconds(preloadTime);

        deathScreen.SetActive(false);
        Game.activeSpawn.SpawnPlayer();
        GameManager.RespawnEnemies();
    }
}
