using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] bool _isHit;
    [SerializeField] int damageValue;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] AudioClip[] hitSounds;

    void Awake()
    {
        hitSounds = Resources.LoadAll<AudioClip>("Audio/hits");
    }

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
    public void WasHit()
    {
        _isHit = false;
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

}
