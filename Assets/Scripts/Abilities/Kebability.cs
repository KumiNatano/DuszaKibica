#define KEBABILITY_USEOBSOLETEHEALTH

using UnityEngine;

public class Kebability : BaseAbility
{
    public float healPoints => _healPoints;

    protected override void OnUseBegin()
    {
        player.attack.leftArm.Block();
        player.attack.rightArm.Block();
        player.attack.leftArm.Interrupt();
        player.attack.rightArm.Interrupt();

        player.viewmodel.Eat();
    }
    protected override void OnUseEnd()
    {
        player.attack.leftArm.Unblock();
        player.attack.rightArm.Unblock();
    }
    protected override void OnWorkBegin()
    {
#if KEBABILITY_USEOBSOLETEHEALTH
        var health = player.GetComponent<HealthSystem>();
        health.setImmortal(5, 15);
        health.Heal(Mathf.RoundToInt(healPoints));
#else
        var heal = player.GetComponent<LivingMixin>();
        heal.Heal(healPoints);
#endif
    }

    [SerializeField] float _healPoints = 50f;
}
