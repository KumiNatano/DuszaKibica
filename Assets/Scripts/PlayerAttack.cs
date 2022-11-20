using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private GameObject attackArea = default;

    private bool isAttacking = false; //czy aktualnie jest w fazie ataku

    private float timeToAttack = 0.25f; //jak dlugo ma byc otwarte okienko czasowe w ktorym mozna atakowac (jak dlugo AttackArea bedzie dostepne)
    private float timer = 0f; //pomocniczy timer

    [SerializeField] private float cooldown = 2f;
    private bool canAttack = true;

    void Start()
    {
        attackArea = transform.GetChild(0).gameObject; //przypisujemy pierwsze dziecko tego obiektu jako attackArea
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && canAttack == true) //jesli nacisniemy i mamy mozliwosc ataku
        {
            Attacking(); //to zaczynamy atak
            StartCoroutine(AttackCoroutine()); //i korutyne
        }

        if (isAttacking) //jesli jestesmy w trakcie ataku
        {
            timer += Time.deltaTime;

            if(timer >= timeToAttack) //jesli sie skonczy okienko czasowe na atak to wylacza nam obiekt dziecka
            {
                timer = 0;
                isAttacking = false;
                attackArea.SetActive(isAttacking);
            }
        }
    }

    IEnumerator AttackCoroutine()
    {
        canAttack = false; //zmienia, ze nie mozemy zaatakowac
        yield return new WaitForSeconds(cooldown); //czekaj "cooldown" sekund zanim wykona instrukcje ponizej
        canAttack = true; //a po czasie juz mozna
    }


    private void Attacking()
    {
        isAttacking = true;
        attackArea.SetActive(isAttacking);
    }

}
