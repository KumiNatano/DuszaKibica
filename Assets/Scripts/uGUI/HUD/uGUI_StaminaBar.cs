using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class uGUI_StaminaBar : MonoBehaviour
{
    PlayerStamina stamina => Player.main.stamina;
    Slider slider;

    void UpdateValue(float oldp, float newp)
    {
        slider.maxValue = stamina.maxPoints;
        slider.value = newp;
    }

    void Awake()
    {
        slider = GetComponent<Slider>();
    }
    void Start()
    {
        float points = stamina.points;
        UpdateValue(points, points);
        stamina.onPointsChange += UpdateValue;
    }
}
