using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatistics : MonoBehaviour
{
    //4 g��wne kategorie statystyk: (wartosci wszystkich statystyk powinny by� tutaj zapisane a pozosta�e skrypty odczytywa�yby warto�ci z tego (wi�ksza czytelno��))
    //1. Obra�enia
    [SerializeField] private int damage;
    //2. Pr�dko�� ataku
    [SerializeField] private float cooldown;
    //3. Wytrzyma�o��
    [SerializeField] private int health;
    public EnemyHealthBar enemyHealthbar;
    public HealthSystem healthSystem;
    //4. Czujno�� (z jakiej odleg�o�ci nas wy�apuje np. mega si�acz z ma�ym rozumkiem kt�ry nie jest zbut czujny)
    [SerializeField] private float distanceBetween;

    //public int level; //mozna uzaleznic zmiane statystyk od tego ale to mo�e in future
    // Start is called before the first frame update
    void Start()
    {
        enemyHealthbar.SetHealth(health, healthSystem.getMaxHealthAmount());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LevelUp()
    {
        health = (int)(health * 1.4);
        damage = (int)(damage * 1.5);
        //cooldown i distanceBetween bym nie zmienia� przy levelowaniu przeciwnik�w bo to mo�e konfundowa� gracza
    }
}
