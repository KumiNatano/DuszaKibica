using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class BigGuyCharge : MonoBehaviour
{
    private AIPath mainAIScript;
    private AIDestinationSetter destinationSetter;
    public bool isInCharge = false;
    public bool isInDestination = false;
    private bool chargeReady = true;
    private bool isPositionKnown = false;
    private GameObject player;
    public Vector3 chargePosition;
    public int oldDamage;

    [SerializeField] private float chargeDistance;
    [SerializeField] private float waitingUntillChargeTime;
    [SerializeField] private float tiredTime;
    [SerializeField] private int specialDamage;
    
    [SerializeField] private EnemyAttack attackScript;

    void Start()
    {
        mainAIScript = GetComponent<AIPath>();
        player = GameObject.FindWithTag("player");
        oldDamage = attackScript.damage;
    }
    
    void Update()
    {
        if (isInCharge == false && isInDestination == false)
        {
            attackScript.damage = oldDamage;
            if (mainAIScript.remainingDistance < chargeDistance && mainAIScript.remainingDistance > chargeDistance - 0.1)
            {
                if (isPositionKnown == false)
                {
                    chargePosition = player.transform.position;
                    isPositionKnown = true;
                }
                else
                {
                    StartCoroutine(ChargeMechanism());
                }
            }
        }
        else if (isInCharge == true && isInDestination == false)
        {
            if (Vector3.Distance(this.transform.position, chargePosition) < 0.75f)
            {
                isInDestination = true;
            }
            this.transform.position = Vector3.MoveTowards(this.transform.position, chargePosition, 9 * Time.deltaTime);
        }
        else if (isInCharge == true && isInDestination == true)
        {
            attackScript.damage = oldDamage;
            StartCoroutine(TiredMechanism());
        }
    }

    IEnumerator TiredMechanism()
    {
        attackScript.enabled = false;
        yield return new WaitForSeconds(tiredTime);
        isInDestination = false;
        isInCharge = false;
        mainAIScript.canMove = true;
        isPositionKnown = false;
    }
    
    IEnumerator ChargeMechanism()
    {
        mainAIScript.canMove = false;
        attackScript.enabled = false;
        yield return new WaitForSeconds(waitingUntillChargeTime);
        attackScript.enabled = true;
        attackScript.damage = specialDamage;
        isInCharge = true;
    }
}
