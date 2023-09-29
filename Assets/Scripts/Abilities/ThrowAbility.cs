using UnityEngine;

public class ThrowAbility : BaseAbility
{
    public float flySpeed => _flySpeed;
    public float hitDamage => _hitDamage;

    protected override void OnUseBegin()
    {
        player.attack.rightArm.Block();
        player.attack.rightArm.Interrupt();
        player.viewmodel.ThrowKnife();
    }
    protected override void OnUseEnd()
    {
        player.attack.rightArm.Unblock();
    }
    protected override void OnWorkBegin()
    {
        var camera = player.playerCamera;
        GameObject knife = Instantiate(_throwablePrefab, _startPoint.position, camera.viewRotation * Quaternion.Euler(-90, 90, 0));

        var behaviour = knife.GetComponent<KnifeBehaviour>();
        behaviour.SetDirection(player.viewForward);
        behaviour.flySpeed = flySpeed;
        behaviour.hitDamage = hitDamage;
        audioSource.PlayOneShot(kniveThrowSound);
    }

    [SerializeField] float _flySpeed = 15f;
    [SerializeField] float _hitDamage = 25f;
    [SerializeField] GameObject _throwablePrefab;
    [SerializeField] Transform _startPoint;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip kniveThrowSound;
}
