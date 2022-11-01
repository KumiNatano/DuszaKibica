using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private float distanceBetween;

    private float distance;

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position); //oblicza dystans miedzy tym obiektem, a obiektem gracza
        Vector2 direction = player.transform.position - transform.position; //I guess, ze odejmowanie dwoch wektorow da nam faktycznie kierunek gdzie sie poruszaja
        direction.Normalize(); //dajemy wartosc wektora na 1, gosciu z poradnika mowi, ze to dlatego zeby uniknac jakichs bugow z tym zwiazanych czy cos
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //kat w radianach miedzy zwrotem y i x przemnozony przez funkcje zamieniajaca radiany na syopnie

        if(distance < distanceBetween) //scigaj gracza tylko jesli jest w odleglosci 4
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime); //co klatke nasz obiekt idzie do gracza z predkoscia speed
            transform.rotation = Quaternion.Euler(Vector3.forward * angle); //tutaj juz ustawiamy obrot przeciwnika na angle
        }
    }
}
