#define PLAYERPUNCHMACHINE_USEOBSOLETEHEALTH

using System;
using UnityEngine;

public class PlayerPunchMachine : MonoBehaviour
{
    public string keyName => _keyName;

    public float punchCount => _punchCount;
    public float attackDamage => _attackDamage;
    public float attackDuration => _attackDuration * _speedMultiplier;
    public float attackTime => _attackTime;
    public float attackFraction => attackTime / attackDuration;
    public float restDuration => _restDuration * _speedMultiplier;
    public float restTime => _restTime;
    public float restFraction => restTime / restDuration;
    public float speed 
    {
        get => GetSpeed();
        set => SetSpeed(value);
    }

    public Vector3 hitboxSize => _hbSize;
    public Vector3 hitboxOffset => _hbOffset;
    public LayerMask hitboxLayers => _hbLayers;

    public PunchMachineState state => _state;
    public bool isBlocked
    {
        get => _isBlocked;
        set
        {
            if (value)
            {
                Block();
            }
            else
            {
                Unblock();
            }
        }
    }

    public bool isIdling => _state == PunchMachineState.Idle;
    public bool isAttacking => _state == PunchMachineState.Attack;
    public bool isResting => _state == PunchMachineState.Rest;

    public event Action<PunchMachineState> onStateUpdate;
    // attack events
    public event Action onAttackBegin;
    public event Action onAttackStay;
    public event Action<bool> onAttackEnd;
    // rest events
    public event Action onRestBegin;
    public event Action onRestStay;
    public event Action onRestEnd;


    public void Init(Player player)
    {
        playerRef = player;
    }
    public bool Attack()
    {
        if (isIdling && !isBlocked)
        {
            ForceAttack();
            return true;
        }
        return false;
    }
    public void ForceAttack()
    {
        _attackTime = 0;
        _restTime = 0;
        _state = PunchMachineState.Attack;
        onStateUpdate?.Invoke(_state);
        _punchCount++;
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
    public void Block()
    {
        _isBlocked = true;
    }
    public void Unblock()
    {
        _isBlocked = false;
    }
    public float GetSpeed()
    {
        return _speedMultiplier;
    }
    public float SetSpeed(float value)
    {
        if (value != 0)
        {
            _speedMultiplier = Mathf.Abs(value);
        }
        else
        {
            _speedMultiplier = 1f;
        }
        return _speedMultiplier;
    }

    void AttackSeq()
    {
        if (_attackTime > float.Epsilon)
        {
            if (_attackTime < attackDuration) // stay
            {
                onAttackStay?.Invoke();
            }
            else // end
            {
                bool r = false;
                if (!wasInterrupted)
                {
                    r = FindAndHit();
                }
                wasInterrupted = false;
                _attackTime = 0;
                _state = PunchMachineState.Rest;
                onStateUpdate?.Invoke(_state);
                onAttackEnd?.Invoke(r);
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
            if (_restTime < restDuration) // stay
            {
                onRestStay?.Invoke();
            }
            else // end
            {
                _restTime = 0;
                _state = PunchMachineState.Idle;
                onStateUpdate?.Invoke(_state);
                onRestEnd?.Invoke();
            }
        }
        else // begin
        {
            onRestBegin?.Invoke();
        }
        _restTime += Time.fixedDeltaTime;
    }
    bool FindAndHit()
    {
        Quaternion co = playerRef.viewRotation;
        Vector3 wp = playerRef.playerCamera.position + co * _hbOffset;
        Vector3 he = _hbSize * 0.5f;
        int c = Physics.OverlapBoxNonAlloc(center: wp, halfExtents: he, results: hitResults, orientation: co, _hbLayers);
        bool fond = false;
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
                fond = true;
#if PLAYERPUNCHMACHINE_USEOBSOLETEHEALTH
                health.TakeDamage(Mathf.CeilToInt(_attackDamage));
#else
                health.Hurt(_attackDamage);
#endif       
            }
        }
        return fond;
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
    [SerializeField] float _speedMultiplier = 1;
    [Header("Hitbox Properties")]
    [SerializeField] Vector3 _hbSize = Vector3.one + Vector3.forward;
    [SerializeField] Vector3 _hbOffset = Vector3.forward;
    [SerializeField] LayerMask _hbLayers = Physics.DefaultRaycastLayers;
    [Header("State")]
    [SerializeField] float _punchCount = 0;
    [SerializeField] bool _isBlocked;
    [SerializeField] PunchMachineState _state = PunchMachineState.Idle;
}
public enum PunchMachineState
{
    Idle,
    Attack,
    Rest
}