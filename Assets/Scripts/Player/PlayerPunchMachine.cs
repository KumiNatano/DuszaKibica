#define PLAYERPUNCHMACHINE_USEOBSOLETEHEALTH

using System;
using UnityEngine;

public class PlayerPunchMachine : MonoBehaviour
{
    public string keyName => _keyName;

    public float attackDamage => _attackDamage;
    public float attackDuration => _attackDuration;
    public float restDuration => _restDuration;

    public Vector3 hitboxSize => _hbSize;
    public Vector3 hitboxOffset => _hbOffset;
    public LayerMask hitboxLayers => _hbLayers;

    public PunchMachineState state => _state;
    public bool isIdling => _state == PunchMachineState.Idle;
    public bool isAttacking => _state == PunchMachineState.Attack;
    public bool isResting => _state == PunchMachineState.Rest;

    // attack events
    public event Action onAttackBegin;
    public event Action onAttackStay;
    public event Action onAttackEnd;
    // rest events
    public event Action onRestBegin;
    public event Action onRestStay;
    public event Action onRestEnd;


    public void Init(Player player)
    {
        playerRef = player;
    }
    public void Attack()
    {
        if (isIdling)
        {
            ForceAttack();
        }
    }
    public void ForceAttack()
    {
        _state = PunchMachineState.Attack;
    }
    public void Interrupt()
    {
        if (isAttacking)
        {
            wasInterrupted = true;
            _attackTime = _attackDuration;
        }
    }
    public void SkipAttack()
    {
        if (isAttacking)
        {
            _attackTime = _attackDuration;
        }
    }
    public void Rest()
    {
        if (isResting)
        {
            _restTime = _restDuration;
        }
    }

    void AttackSeq()
    {
        if (_attackTime > float.Epsilon)
        {
            if (_attackTime < _attackDuration) // stay
            {
                onAttackStay?.Invoke();
            }
            else // end
            {
                if (!wasInterrupted)
                {
                    FindAndHit();
                }
                wasInterrupted = false;
                _attackTime = 0;
                _state = PunchMachineState.Rest;
                onAttackEnd?.Invoke();
            }
        }
        else // begin
        {
            onAttackBegin?.Invoke();
        }
        _attackTime += Time.fixedDeltaTime;
    }
    void RestSeq()
    {
        if (_restTime > float.Epsilon)
        {
            if (_restTime < _restDuration) // stay
            {
                onRestStay?.Invoke();
            }
            else // end
            {
                _restTime = 0;
                _state = PunchMachineState.Idle;
                onRestEnd?.Invoke();
            }
        }
        else // begin
        {
            onAttackBegin?.Invoke();
        }
        _restTime += Time.fixedDeltaTime;
    }
    void FindAndHit()
    {
        Quaternion co = playerRef.viewRotation;
        Vector3 wp = playerRef.playerCamera.position + co * _hbOffset;
        Vector3 he = _hbSize * 0.5f;
        int c = Physics.OverlapBoxNonAlloc(center: wp, halfExtents: he, results: hitResults, orientation: co, _hbLayers);
        for (int i = 0; i < c; i++)
        {
            Collider col = hitResults[i];
            if (col.CompareTag("player"))
            {
                continue;
            }
#if PLAYERPUNCHMACHINE_USEOBSOLETEHEALTH
            HealthSystem health;
#else
            LivingMixin health;
#endif       
            if (col.TryGetComponent(out health))
            {
#if PLAYERPUNCHMACHINE_USEOBSOLETEHEALTH
                health.TakeDamage(Mathf.CeilToInt(_attackDamage));
#else
                health.Hurt(_attackDamage);
#endif       
            }
        }
    }

    void FixedUpdate()
    {
        if (isAttacking)
        {
            AttackSeq();
        }
        else if (isResting)
        {
            RestSeq();
        }
    }


    Player playerRef;
    Collider[] hitResults = new Collider[16];
    bool wasInterrupted = false;

    [Header("Input")]
    [SerializeField] string _keyName = "Fire1";
    [Header("Attack")]
    [SerializeField] float _attackDamage = 25f;
    [SerializeField] float _attackTime = 0f;
    [SerializeField] float _attackDuration = 0.5f;
    [SerializeField] float _restTime = 0f;
    [SerializeField] float _restDuration = 0.5f;
    [Header("Hitbox Properties")]
    [SerializeField] Vector3 _hbSize = Vector3.one;
    [SerializeField] Vector3 _hbOffset = Vector3.zero;
    [SerializeField] LayerMask _hbLayers = Physics.DefaultRaycastLayers;
    [Header("State")]
    [SerializeField] PunchMachineState _state = PunchMachineState.Idle;
}
public enum PunchMachineState
{
    Idle,
    Attack,
    Rest
}