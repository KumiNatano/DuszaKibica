using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] bool _isHit;
    [SerializeField] int damageValue;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] AudioClip[] hitSounds;
    [SerializeField] private bool _disableAttack;

    void Awake()
    {
        hitSounds = Resources.LoadAll<AudioClip>("Audio/hits");
        _disableAttack = false;
    }

    private void Start()
    {
        _isHit = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        
        if(collider.gameObject.tag == "player" & !_disableAttack)
        {
            if (!_isHit)
            {
                print("test");
                _isHit = true;
                Player.main.living.Hurt(damageValue);
                PlayAttackSound();
                
            }
        }
    }

    public bool IsHit()
    {
        return _isHit;
    }
    public void SetHit(bool hit)
    {
        _isHit = hit;
    }

    public void SetDamage(int newDamage)
    {
        damageValue = newDamage;
    }
    
    public void PlayAttackSound()
    {
        int soundToPlay = Random.Range(0, hitSounds.Length);
        audioSource.PlayOneShot(hitSounds[soundToPlay]);
    }

    public void TurnOffAttack()
    {
        _disableAttack = true;
    }
}
