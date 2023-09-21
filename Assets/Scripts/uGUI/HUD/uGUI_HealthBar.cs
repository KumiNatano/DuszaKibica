using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class uGUI_HealthBar : MonoBehaviour
{
    LivingMixin living => Player.main.living;
    Slider slider;

    void UpdateValue(float oldp, float newp)
    {
        slider.maxValue = living.maxHealth;
        slider.value = newp;
    }

    void Awake()
    {
        slider = GetComponent<Slider>();
    }
    void Start()
    {
        float h = living.health;
        UpdateValue(h, h);
        living.onHealthChange += UpdateValue;
    }
}
