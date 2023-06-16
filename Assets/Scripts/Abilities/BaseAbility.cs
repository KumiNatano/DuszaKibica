using System;
using UnityEngine;

public abstract class BaseAbility : MonoBehaviour
{
    public string displayName => _displayName;
    public string keyName => _keyName;

    public AbilityState state => _state;
    public bool isBusy => !idling;
    public bool idling => state == AbilityState.Idle;
    public bool inUse => state == AbilityState.Use;
    public bool working => state == AbilityState.Work;
    public bool resting => state == AbilityState.Rest;

    // using
    public float useDuration => _useDuration;
    public float useTime => _useTime;
    public float useFraction => useTime / useDuration;
    // working
    public float workDuration => _workDuration;
    public float workTime => _workTime;
    public float workFraction => workTime / workDuration;
    // resting
    public float restDuration => _restDuration;
    public float restTime => _restTime;
    public float restFraction => restTime / restDuration;

    public event Action onUseBegin;
    public event Action onUseStay;
    public event Action onUseEnd;

    public event Action onWorkBegin;
    public event Action onWorkStay;
    public event Action onWorkEnd;

    public event Action onRestBegin;
    public event Action onRestStay;
    public event Action onRestEnd;

    public event Action<AbilityState> onStateUpdate;


    public bool Use()
    {
        if (idling)
        {
            ForceUse();
            return true;
        }
        return false;
    }
    public void ForceUse()
    {
        _useTime = 0;
        _workTime = 0;
        _restTime = 0;
        _state = AbilityState.Use;
        onStateUpdate?.Invoke(_state);
    }
    public void Init(Player p)
    {
        player = p;
    }

    protected virtual void OnUseBegin() { }
    protected virtual void OnUseStay() { }
    protected virtual void OnUseEnd() { }
    protected virtual void OnWorkBegin() { }
    protected virtual void OnWorkStay() { }
    protected virtual void OnWorkEnd() { }
    protected virtual void OnRestBegin() { }
    protected virtual void OnRestStay() { }
    protected virtual void OnRestEnd() { }


    void UseSeq()
    {
        if (_useTime > float.Epsilon)
        {
            if (_useTime < _useDuration) // stay
            {
                OnUseStay();
                onUseStay?.Invoke();
            }
            else // end
            {
                _useTime = 0;
                _state = AbilityState.Work;
                onStateUpdate?.Invoke(_state);
                OnUseEnd();
                onUseEnd?.Invoke();
            }
        }
        else // begin
        {
            OnUseBegin();
            onUseBegin?.Invoke();
        }
        _useTime += Time.fixedDeltaTime;
    }
    void WorkSeq()
    {
        if (_workTime > float.Epsilon)
        {
            if (_workTime < _workDuration) // stay
            {
                OnWorkStay();
                onWorkStay?.Invoke();
            }
            else // end
            {
                _workTime = 0;
                _state = AbilityState.Rest;
                onStateUpdate?.Invoke(_state);
                OnWorkEnd();
                onWorkEnd?.Invoke();
            }
        }
        else // begin
        {
            OnWorkBegin();
            onWorkBegin?.Invoke();
        }
        _workTime += Time.fixedDeltaTime;
    }
    void RestSeq()
    {
        if (_restTime > float.Epsilon)
        {
            if (_restTime < _restDuration) // stay
            {
                OnRestStay();
                onRestStay?.Invoke();
            }
            else // end
            {
                _restTime = 0;
                _state = AbilityState.Idle;
                onStateUpdate?.Invoke(_state);
                OnRestEnd();
                onRestEnd?.Invoke();
            }
        }
        else // begin
        {
            OnRestBegin();
            onRestBegin?.Invoke();
        }
        _restTime += Time.fixedDeltaTime;
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case AbilityState.Use:
                UseSeq();
                break;
            case AbilityState.Work:
                WorkSeq();
                break;
            case AbilityState.Rest:
                RestSeq();
                break;
        }
    }


    protected Player player { get; private set; }

    [SerializeField] string _displayName = "Invalid Ability";
    [SerializeField] string _keyName = "Use Invalid Ability";
    [Header("Use")]
    [SerializeField] float _useDuration = 1f;
    [SerializeField] float _useTime = 0f;
    [Header("Work")]
    [SerializeField] float _workDuration = 1f;
    [SerializeField] float _workTime = 0f;
    [Header("Rest")]
    [SerializeField] float _restDuration = 1f;
    [SerializeField] float _restTime = 0f;
    [Header("State")]
    [SerializeField] AbilityState _state;
}
public enum AbilityState
{
    Idle,
    Use,
    Work,
    Rest
}
