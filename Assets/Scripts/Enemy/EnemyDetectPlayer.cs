using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyDetectPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float distanceBetween;

    private float distance;

    void Start() { 
    
        player = GameObject.FindGameObjectsWithTag("player")[0];
        this.gameObject.GetComponent<AIDestinationSetter>().target = player.transform;

    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position); //oblicza dystans miedzy tym obiektem, a obiektem gracza
        Vector2 direction = player.transform.position - transform.position; //I guess, ze odejmowanie dwoch wektorow da nam faktycznie kierunek gdzie sie poruszaja
        direction.Normalize(); //dajemy wartosc wektora na 1, gosciu z poradnika mowi, ze to dlatego zeby uniknac jakichs bugow z tym zwiazanych czy cos

        if (distance < distanceBetween) //scigaj gracza tylko jesli jest w odleglosci 4
        {
            this.gameObject.GetComponent<AIPath>().enabled = true;
        }
        else
        {
            this.gameObject.GetComponent<AIPath>().enabled = false;
        }
    }
    public float GetDistance()
    {
        return distance;
    }
    public GameObject GetPlayer()
    {
        return player;
    }


}
