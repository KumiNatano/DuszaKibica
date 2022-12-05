using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatistics : MonoBehaviour
{
    //4 g³ówne kategorie statystyk: (wartosci wszystkich statystyk powinny byæ tutaj zapisane a pozosta³e skrypty odczytywa³yby wartoœci z tego (wiêksza czytelnoœæ))
    //1. Obra¿enia
    [SerializeField] private int damage;
    //2. Prêdkoœæ ataku
    [SerializeField] private float cooldown;
    //3. Wytrzyma³oœæ
    [SerializeField] private int health;
    //4. Czujnoœæ (z jakiej odleg³oœci nas wy³apuje np. mega si³acz z ma³ym rozumkiem który nie jest zbut czujny)
    [SerializeField] private float distanceBetween;

    //public int level; //mozna uzaleznic zmiane statystyk od tego ale to mo¿e in future
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LevelUp()
    {
        health = (int)(health * 1.4);
        damage = (int)(damage * 1.5);
        //cooldown i distanceBetween bym nie zmienia³ przy levelowaniu przeciwników bo to mo¿e konfundowaæ gracza
    }
}
