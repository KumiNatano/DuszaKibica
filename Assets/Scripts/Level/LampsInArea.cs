using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampsInArea : MonoBehaviour
{
    [SerializeField] List<Light> sourcesOfLights;

    public void setLightOn()
    {
        foreach(Light light in sourcesOfLights)
        {
            light.enabled = true;
        }
    }

    public void setLightOff()
    {
        foreach (Light light in sourcesOfLights)
        {
            light.enabled = false;
        }
    }
}
