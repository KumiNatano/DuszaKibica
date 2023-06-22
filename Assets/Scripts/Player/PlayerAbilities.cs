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
    public override void OnLateUpdate(float deltaTime)
    {
        foreach(BaseAbility ability in _list)
        {
            if (ability.idling && Input.GetButton(ability.keyName))
            {
                ability.Use();
            }
        }
    }

    BaseAbility[] _list;
}
