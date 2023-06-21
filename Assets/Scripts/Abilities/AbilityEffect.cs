using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;

public class AbilityEffect : MonoBehaviour
{
    public Volume volume;
    public BaseAbility abilityRef;

    public float blendIn = 0.5f;
    public float blendOut = 0.5f;

    void OnEnable()
    {
        abilityRef.onWorkBegin += BlendIn;
        abilityRef.onWorkEnd += BlendOut;
    }

    void BlendIn()
    {
        StartCoroutine(BIS());
    }
    void BlendOut()
    {
        StartCoroutine(BOS());
    }

    IEnumerator BIS()
    {
        float t = 0;
        while (t < blendIn)
        {
            t += Time.deltaTime;
            volume.weight = Mathf.Clamp(t / blendIn, 0, 1);
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator BOS()
    {
        float t = 0;
        while (t < blendIn)
        {
            t += Time.deltaTime;
            volume.weight = 1 - t / blendIn;
            yield return new WaitForEndOfFrame();
        }
    }
}
