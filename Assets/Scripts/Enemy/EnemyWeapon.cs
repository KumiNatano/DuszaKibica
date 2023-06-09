using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] bool _isHit;

    private void Start()
    {
        _isHit = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "player")
        {
            _isHit = true;
        }
        //else _isHit = false;
    }

    public bool IsHit()
    {
        return _isHit;
    }
    public void WasHit()
    {
        _isHit = false;
    }
}
