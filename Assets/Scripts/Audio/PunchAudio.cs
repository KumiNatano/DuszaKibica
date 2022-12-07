using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAudio : MonoBehaviour
{
    [SerializeField] AudioClip[] hitSounds;
    AudioClip[] swishSounds;

    void Awake()
    {
        hitSounds = Resources.LoadAll<AudioClip>("Audio/hits");
        swishSounds = Resources.LoadAll<AudioClip>("Audio/swishes");
    }

    public void PlayAttackSound()
    {
        int soundToPlay = Random.Range(0, hitSounds.Length);
        this.GetComponent<AudioSource>().PlayOneShot(hitSounds[soundToPlay]);
    }

    public void PlaySwishSound()
    {
        int soundToPlay = Random.Range(0, swishSounds.Length);
        this.GetComponent<AudioSource>().PlayOneShot(swishSounds[soundToPlay]);
    }
}
