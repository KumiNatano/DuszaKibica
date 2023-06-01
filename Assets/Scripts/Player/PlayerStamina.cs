using System;
using UnityEngine;

public class PlayerStamina : PlayerModule
{
    public float points = 100.0f;
    public float maxPoints = 100.0f;

    [Header("Regeneration")]
    public float regenPoints = 10;
    public float regenAfter = 3f;

    public const int regenRate = 16;
    public float regenRateRaw => 1.0f / regenRate;
    public float lastPointUse { get; private set; } = 0f;

    public event Action<float> onUse;
    public event Action<float> onRegenerate;
    public event Action<float, float> onPointsChange;


    public override void OnInit()
    {
        InvokeRepeating(nameof(RegenTick), 0f, regenRateRaw);
    }

    public bool UsePoints(float amount)
    {
        if (points < amount)
        {
            return false;
        }
        ForceUsePoints(amount);
        return false;
    }
    public void ForceUsePoints(float amount) 
    {
        lastPointUse = Time.time;
        float old = points;
        points -= amount;
        points = Mathf.Clamp(points, 0, maxPoints);
        if (old != points)
        {
            onPointsChange?.Invoke(old, points);
            onUse?.Invoke(old - points);
        }
    }
    public void Regenerate(float amount)
    {
        float old = points;
        points += amount;
        if (points > maxPoints)
        {
            points = maxPoints;
        }
        if (old != points)
        {
            onPointsChange?.Invoke(old, points);
            onRegenerate?.Invoke(points - old);
        }
    }

    void RegenTick()
    {
        float slu = Time.time - lastPointUse;
        if (slu < regenAfter)
        {
            return;
        }
        if (points < maxPoints)
        {
            Regenerate(regenPoints * regenRateRaw);
        }
    }
}
