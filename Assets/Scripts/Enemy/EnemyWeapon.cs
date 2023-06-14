using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] bool _isHit;
    [SerializeField] int damageValue;

    private void Start()
    {
        _isHit = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "player")
        {
            if (!_isHit)
            {
                _isHit = true;
                HealthSystem health = collider.GetComponent<HealthSystem>(); //bierzemy system zycia
                health.TakeDamage(damageValue);
            }
        }
    }

    public bool IsHit()
    {
        return _isHit;
    }
    public void WasHit()
    {
        _isHit = false;
    }

    public void SetDamage(int newDamage)
    {
        damageValue = newDamage;
    }

}
