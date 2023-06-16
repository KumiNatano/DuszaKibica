using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAbilities : PlayerModule
{
    public IReadOnlyCollection<BaseAbility> list => _list;


    public T GetAbility<T>() where T : BaseAbility
    {
        return (T)_list.First(x => x is T);
    }
    public bool TryGetAbility<T>(out T module) where T : BaseAbility
    {
        T m = (T)_list.FirstOrDefault(x => x is T);
        if (m == null || m == default(T))
        {
            module = null;
            return false;
        }
        module = m;
        return true;
    }

    public override void OnInit()
    {
        _list = GetComponentsInChildren<BaseAbility>();
        foreach (var ab in _list)
        {
            ab.Init(parent);
        }
    }
    public override void OnUpdate(float deltaTime)
    {
        BaseAbility target = null;
        bool flag1 = false;
        bool flag2 = true;
        foreach(BaseAbility ability in _list)
        {
            if (ability.inUse)
            {
                flag2 = false;
                return;
            }
            if (ability.idling && Input.GetButton(ability.keyName))
            {
                flag1 = true;
                target = ability;
            }
        }
        if (flag1 && flag2)
        {
            target.Use();
        }
    }

    BaseAbility[] _list;
}
