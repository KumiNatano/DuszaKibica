using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerViewmodel : PlayerModule
{
    PlayerAttack attack;


    public override void OnInit()
    {
        animator = GetComponent<Animator>();
        attack = parent.GetModule<PlayerAttack>();

        attack.leftArm.onAttackBegin += PunchLeft;
        attack.rightArm.onAttackBegin += PunchRight;
    }

    public void PunchRight() => Punch(true, attack.rightArm.attackDuration);
    public void PunchLeft() => Punch(false, attack.leftArm.attackDuration);
    public void Punch(bool right, float duration)
    {
        string hand = "Punch " + (right ? "Right" : "Left");
        duration = 1f / duration;
        animator.SetFloat(hand + " Speed", duration);
        animator.SetTrigger(hand);
    }
    public void Drink()
    {
        animator.SetTrigger("Drink");
    }
    public void Eat()
    {
        animator.SetTrigger("Eat");
    }
    public void ThrowKnife()
    {
        animator.SetTrigger("Throw Knife");
    }


    Animator animator;
}
