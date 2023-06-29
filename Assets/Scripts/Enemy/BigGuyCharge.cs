using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class BigGuyCharge : MonoBehaviour
{
    private AIPath mainAIScript;
    private AIDestinationSetter destinationSetter;
    private bool isInCharge = false;
    private bool isInDestination = false;
    private bool chargeReady = true;
    private bool isPositionKnown = false;
    private GameObject player;
    private Vector3 chargePosition;

    private bool wasDamaged = false;

    [SerializeField] private float chargeDistance;
    [SerializeField] private float waitingUntillChargeTime;
    [SerializeField] private float tiredTime;
    [SerializeField] private int specialDamage;
    [SerializeField] private int chargeSpeed;
    
    [SerializeField] private EnemyAttack attackScript;

    [SerializeField] private Animator animator;

    [SerializeField] private SphereCollider attackArea;

    [SerializeField] private int chargeMultiplier = 3;

    private Vector3 transformForwardOnce;
    

    void Start()
    {
        mainAIScript = GetComponent<AIPath>();
        player = GameObject.FindWithTag("player");
    }
    
    void Update()
    {
        if (isInCharge == false && isInDestination == false)
        {
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
            if (attackArea.GetComponent<BigGuyChargeWeapon>().isInTrigger == true && wasDamaged == false && GameObject.FindWithTag("player").GetComponent<HealthSystem>().isImmortal == false)
            {
                GameObject.FindWithTag("player").GetComponent<HealthSystem>().TakeDamage(specialDamage);
                wasDamaged = true;
            }

            animator.SetBool("isPreparingCharge", false);
            animator.SetBool("isInCharge", true);
            if (Vector3.Distance(this.transform.position, chargePosition /* + transformForwardOnce * chargeMultiplier*/) < 0.75f)
            {
                isInDestination = true;
            }
            this.transform.position = Vector3.MoveTowards(this.transform.position, chargePosition /*+ transformForwardOnce * chargeMultiplier*/, chargeSpeed * Time.deltaTime);
        }
        else if (isInCharge == true && isInDestination == true)
        {
            animator.SetBool("isInCharge", false);
            attackScript.enabled = false;
            mainAIScript.canMove = false;
            StartCoroutine(TiredMechanism());
        }
    }

    IEnumerator TiredMechanism()
    {
        yield return new WaitForSeconds(tiredTime);
        isInDestination = false;
        isInCharge = false;
        mainAIScript.canMove = true;
        isPositionKnown = false;
        attackScript.enabled = true;
        mainAIScript.rotationSpeed = 360;
        wasDamaged = false;
    }
    
    IEnumerator ChargeMechanism()
    {
        animator.SetBool("isPreparingCharge", true);
        mainAIScript.canMove = false;
        attackScript.enabled = false;
        mainAIScript.rotationSpeed = 0;
        transformForwardOnce = transform.forward;
        yield return new WaitForSeconds(waitingUntillChargeTime);
        isInCharge = true;
    }
}
