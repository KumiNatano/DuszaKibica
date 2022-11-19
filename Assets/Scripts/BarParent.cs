using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarParent : MonoBehaviour
{
    public Slider slider;
    public void Change(int change)
    {
        slider.value = change;
    }
    public void SetMax(int maxAmount)
    {
        slider.maxValue = maxAmount;
        Change(maxAmount);
    }
}
