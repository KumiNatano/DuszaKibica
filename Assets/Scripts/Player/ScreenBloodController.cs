using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScreenBloodController : MonoBehaviour
{
    [SerializeField] private Volume screenBlood;
    //private Vignette vg;
    [SerializeField] private bool isCorutineActuallyWorking = false;
    
    private void Start()
    {
        //screenBlood.profile.TryGet(out vg);
        screenBlood.enabled = true;
    }

    public IEnumerator StartBloodScreen(float startValue, float endValue, float duration)
    {

        float time = 0;

        while (time < duration)
        {
            float t = time / duration;
            float smoothedValue = Mathf.Lerp(startValue, endValue, t);

            screenBlood.weight = smoothedValue;

            time += Time.deltaTime;
        }
        
        time = 0;

        if (isCorutineActuallyWorking == false)
        {
            isCorutineActuallyWorking = true;
            while (time < duration)
            {
                float t = time / duration;
                float smoothedValue = Mathf.Lerp(screenBlood.weight, startValue, t);

                screenBlood.weight = smoothedValue;

                time += Time.deltaTime;
                isCorutineActuallyWorking = false;
                yield return null;
            }
        }
        

    }
}
