using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private GameObject attackArea = default;

    private bool isAttacking = false; //czy aktualnie jest w fazie ataku

    private float timeToAttack = 0.25f; //jak dlugo ma byc otwarte okienko czasowe w ktorym mozna atakowac (jak dlugo AttackArea bedzie dostepne)
    private float timer = 0f; //pomocniczy timer

    [SerializeField] public float cooldown = 2f;
    private bool canAttack = true;

    [SerializeField] private GameObject swish;

    void Start()
    {
        attackArea = transform.GetChild(0).gameObject; //przypisujemy pierwsze dziecko tego obiektu jako attackArea
        //animator = transform.GetChild(2).gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && canAttack == true && GameObject.FindWithTag("PauseManager").GetComponent<PauseManager>().isPaused == false) //jesli nacisniemy i mamy mozliwosc ataku i nie ma pauzy
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
                swish.SetActive(isAttacking);
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
        if(this.gameObject.GetComponent<StaminaSystem>().TakeAction(20) && this.gameObject.GetComponent<PlayerDeathManager>().isDead == false)
        {
            isAttacking = true;
            attackArea.SetActive(isAttacking);
            this.gameObject.GetComponent<PunchAudio>().PlaySwishSound(); //odtwarzanie dzwieku machniecia
            swish.SetActive(isAttacking);

            int animacjaAtaku = Random.Range(1, 3);

            if (animacjaAtaku == 1)
            {
                this.gameObject.GetComponent<PlayerAnimations>().setIsAttackingLeftTrue();
            }

            else if (animacjaAtaku == 2)
            {
                this.gameObject.GetComponent<PlayerAnimations>().setIsAttackingRightTrue();
            }

            StartCoroutine(turnOffAnimation());
        }
        
    }

    IEnumerator turnOffAnimation()
    {
        yield return new WaitForSeconds(0.311f); //idealnie dlugosc animacji uderzania, w przyszlosci lepiej wrzucic to do zmiennej jesli beda np animacje o roznej dlugosci
        this.gameObject.GetComponent<PlayerAnimations>().setIsAttackingLeftFalse();
        this.gameObject.GetComponent<PlayerAnimations>().setIsAttackingRightFalse();
    }

}
