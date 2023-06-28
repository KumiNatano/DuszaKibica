using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightMusic : MonoBehaviour
{
    public bool isPlaying { get; private set; }

    public void Play()
    {
        if (isPlaying)
        {
            return;
        }
        StartCoroutine(PlaySeq());
    }
    public void Stop()
    {
        if (!isPlaying)
        {
            return;
        }
        source.Stop();
    }
    void Start()
    {
        Play();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("playedintro", 0);
    }

    IEnumerator PlaySeq()
    {
        if (PlayerPrefs.GetInt("playedintro") == 0)
        {
            source.Stop();
            source.loop = false;
            source.clip = introClip;
            source.Play();
            yield return new WaitUntil(() => source.time > (source.clip.length - 0.1f));
            PlayerPrefs.SetInt("playedintro", 1);
            yield return null;
        }
        source.Stop();
        source.loop = true;
        source.clip = loopClip;
        source.Play();
    }

    [SerializeField] AudioClip introClip;
    [SerializeField] AudioClip loopClip;
    [SerializeField] AudioSource source;
}
