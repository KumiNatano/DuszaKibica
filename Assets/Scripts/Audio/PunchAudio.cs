using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PunchAudio : MonoBehaviour
{
    [SerializeField] AudioClip[] hitSounds;
    AudioClip[] swishSounds;

    [SerializeField] private GameObject enemyDetector;
    [SerializeField] AudioSource aSource;
    
    PlayerAttack attack;
    
    void Awake()
    {
        hitSounds = Resources.LoadAll<AudioClip>("Audio/hits");
        swishSounds = Resources.LoadAll<AudioClip>("Audio/swishes");
    }

    private void Start()
    {
        if (this.gameObject.tag == "player")
        {
            attack = GetComponent<PlayerAttack>();
            attack.leftArm.onAttackBegin += PlaySound;
            attack.rightArm.onAttackBegin += PlaySound; 
        }
    }

    void PlaySound()
    {
        bool isEnemyDetected = enemyDetector.GetComponent<EnemyDetector>().GetIsEnemyDetected();
        if (isEnemyDetected == true)
        {
            int soundToPlay = Random.Range(0, hitSounds.Length);
            aSource.PlayOneShot(hitSounds[soundToPlay]);
        }
        else
        {
            int soundToPlay = Random.Range(0, swishSounds.Length);
            aSource.PlayOneShot(swishSounds[soundToPlay]);
        }
        
    }

    public void PlayAttackSound()
    {
        int soundToPlay = Random.Range(0, hitSounds.Length);
        aSource.PlayOneShot(hitSounds[soundToPlay]);
    }

    public void PlaySwishSound()
    {
        int soundToPlay = Random.Range(0, swishSounds.Length);
        aSource.PlayOneShot(swishSounds[soundToPlay]);
    }
}
