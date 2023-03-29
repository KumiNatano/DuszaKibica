using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    [SerializeField] private int damage = 3;
    [SerializeField] private float cooldown = 10000000f;
    [SerializeField] GameObject parent;
    [SerializeField] string objectTagToFind = "player";
    [SerializeField] int attackDistance = 7;
    [SerializeField] Transform player;
    [SerializeField] GameObject enemyKnifePrefab;
    float knifeSpeed=9f;
    int knifeDamage=10;



    private bool canAttack = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(objectTagToFind).transform;
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < attackDistance&&canAttack)
        {
            //GameObject knife = Instantiate(enemyKnifePrefab, new Vector3(this.transform.position.x, 1, this.transform.position.z), this.transform.rotation);
            throwKnife();
            StartCoroutine(AttackCoroutine());
        }
    }
    IEnumerator turnOffAnimation()
    {
        yield return new WaitForSeconds(0.467f); //idealnie dlugosc animacji uderzania, w przyszlosci lepiej wrzucic to do zmiennej jesli beda np animacje o roznej dlugosci
        parent.GetComponent<EnemyAnimations>().setIsAttackingLeftFalse();
        parent.GetComponent<EnemyAnimations>().setIsAttackingRightFalse();
    }

    IEnumerator AttackCoroutine()
    {
        canAttack = false; //zmienia, ze nie mozemy zaatakowac
        yield return new WaitForSeconds(cooldown); //czekaj "cooldown" sekund zanim wykona instrukcje ponizej
        canAttack = true; //a po czasie juz mozna
    }
    void throwKnife()
    {
        //knifeImage.GetComponent<RawImage>().texture = abilities.GetComponent<AbilitiesUI>().knifeTextures[0]; //ustawiamy obrazek na wyszarzony

        //player = GameObject.FindGameObjectWithTag("player").transform;

        GameObject knife = Instantiate(enemyKnifePrefab, new Vector3(this.transform.position.x, 1, this.transform.position.z), this.transform.rotation);
        Vector3 destination= new Vector3(player.position.x * 1, 1, player.position.z * 1);
        knife.GetComponent<KnifeBehaviour>().setAll(destination, knifeSpeed, knifeDamage, "player");
        //knife.GetComponent<ThrowingKnifeEnemy>().destinationPosition = new Vector3(player.position.x * 1, 1, player.position.z * 1);
        //knife.GetComponent<ThrowingKnifeEnemy>().knifeSpeed = knifeSpeed;
        //knife.GetComponent<KnifeBehaviour>().knifeDamage = knifeDamage;

    }


}
