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
        health.setImmortal(workDuration * 2, 0);
        health.Heal(Mathf.RoundToInt(healPoints));
#else
        player.living.Heal(healPoints);
#endif
    }

    [SerializeField] float _healPoints = 50f;
}
