using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class uGUI_StaminaBar : MonoBehaviour
{
    Slider slider;

    void UpdateValue(float oldp, float newp)
    {
        slider.maxValue = Player.main.stamina.maxPoints;
        slider.value = newp;
    }

    void Awake()
    {
        slider = GetComponent<Slider>();
    }
    void Start()
    {
        Player.main.stamina.onPointsChange += UpdateValue;
    }
}
