using System;
using UnityEngine;

public class LivingMixin : MonoBehaviour
{
    public float health
    {
        get => _health;
        set
        {
            SetHealth(value);
        }
    }
    public float maxHealth
    {
        get => _maxHealth;
        set
        {
            SetMaxHealth(value);
        }
    }

    public bool canHurt = true;
    public bool canHeal = true;

    public bool isAlive => _health >= float.Epsilon;
    public bool isFullHealth => _health >= _maxHealth;

    public event Action<float, float> onHealthChange;
    public event Action<float> onHeal;
    public event Action<float> onHurt;
    public event Action onFullHealth;
    public event Action onRevive;
    public event Action onDeath;


    [SerializeField] float _health = 100f;
    [SerializeField] float _maxHealth = 100f;


    public void SetHealth(float newHealth)
    {
        if (_health != newHealth)
        {
            if (!isAlive)
            {
                Revive(newHealth);
            }
            else
            {
                if (newHealth > _health)
                {
                    ForceHeal(newHealth - _health);
                }
                else
                {
                    ForceHurt(_health - newHealth);
                }
            }
        }
    }
    public void SetMaxHealth(float newMaxHealth) 
    {
        // TODO: Pomyslec czy zostawic jak jest czy rozwinac
        _maxHealth = newMaxHealth;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }
    public void Hurt(float damage)
    {
        if (canHurt)
        {
            ForceHurt(damage);
        }
    }
    public void Heal(float bonus)
    {
        if (canHeal)
        {
            ForceHeal(bonus);
        }
    }
    public void ForceHurt(float damage)
    {
        if (!isAlive)
        {
            return;
        }
        float oldh = _health;
        float newh = _health - damage;
        bool klm = newh < float.Epsilon;
        if (klm)
        {
            newh = 0;
        }
        if (newh < oldh)
        {
            _health = newh;
            onHealthChange?.Invoke(oldh, newh);
            if (klm)
            {
                damage = oldh;
                onDeath?.Invoke();
            }
            onHurt?.Invoke(damage);
        }

    }
    public void ForceHeal(float bonus)
    {
        if (!isAlive)
        {
            return;
        }
        float oldh = _health;
        float newh = _health + bonus;
        bool rmh = newh > _maxHealth;
        if (rmh)
        {
            newh = _maxHealth;
        }
        if (newh > oldh)
        {
            _health = newh;
            onHealthChange?.Invoke(oldh, newh);
            if (rmh)
            {
                bonus = newh - oldh;
                onFullHealth?.Invoke();
            }
            onHeal?.Invoke(bonus);
        }
    }

    public void HealMax()
    {
        if (!isFullHealth && isAlive)
        {
            float oldh = _health;
            _health = _maxHealth;
            onHealthChange?.Invoke(oldh, _maxHealth);
            onFullHealth?.Invoke();
            onHeal?.Invoke(_maxHealth - oldh);
        }
    }
    public void Kill()
    {
        if (isAlive)
        {
            float oldh = _health;
            _health = 0;
            onHealthChange?.Invoke(oldh, 0);
            onDeath?.Invoke();
            onHurt?.Invoke(oldh);
        }
    }
    public void Revive(float startHealth = -1)
    {
        if (!isAlive)
        {
            if (startHealth > _maxHealth || startHealth < float.Epsilon)
            {
                startHealth = _maxHealth;
            }
            _health = startHealth;
            onHealthChange?.Invoke(0, _health);
            onRevive?.Invoke();
            onHeal?.Invoke(_health);
        }
    }
}
