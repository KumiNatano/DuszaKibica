using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyQueueManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> enemies;
    private GameObject player;

    [SerializeField] private float distanceToStay;
    [SerializeField] private int howMuchAtOnce;
    
    void Start()
    {
        player = GameObject.FindWithTag("player");

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("enemy").Length; i++)
        {
            enemies.Add(GameObject.FindGameObjectsWithTag("enemy")[i]);
        }

    }
    
    void Update()
    {
        if (Time.frameCount % 60 == 0)
        {
            scanLookingForEnemies();
            checkOrder();
            disableOtherEnemies();
        }
    }

    void checkOrder()
    {
        Vector3 playerposition = player.GetComponent<Transform>().position;
        
        for (int i = 0; i < enemies.Count - 1; i++)
        {
            if (Vector3.Distance(playerposition, enemies[i].GetComponent<Transform>().position) >
                Vector3.Distance(playerposition, enemies[i + 1].GetComponent<Transform>().position))
            {
                (enemies[i], enemies[i + 1]) = (enemies[i + 1], enemies[i]);
                i = 0;
            }
        }
    }

    public void scanLookingForEnemies()
    {
        GameObject[] tempEnemies;
        tempEnemies = GameObject.FindGameObjectsWithTag("enemy");
        List<GameObject> tempNewList = new List<GameObject>();
        
        for (int i = 0; i < tempEnemies.Length; i++)
        {
            if (tempEnemies[i].GetComponent<BoxCollider>().enabled)
            {
                tempNewList.Add(tempEnemies[i]);
            }
        }

        if (tempNewList.Count != enemies.Count)
        {
            enemies = tempNewList;
        }
    }

    public void disableOtherEnemies()
    {
        Vector3 playerPosition = player.transform.position;

        for (int i = 0; i < howMuchAtOnce; i++)
        {
            enemies[i].GetComponent<AIPath>().canMove = true;
        }
        
        for (int i = howMuchAtOnce; i < enemies.Count; i++)
        {
            AIPath aiPath = enemies[i].GetComponent<AIPath>();

            if (Vector3.Distance(playerPosition, enemies[i].transform.position) < distanceToStay)
            {
                aiPath.canMove = false;
            }
            else
            {
                aiPath.canMove = true;
            }
        }
    }

}
