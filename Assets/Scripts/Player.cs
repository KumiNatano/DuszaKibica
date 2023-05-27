using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    public PlayerCamera playerCamera;


    public T GetModule<T>() where T : PlayerModule
    {
        return (T)modules.First(x => x is T);
    }
    public bool TryGetModule<T>(out T module) where T : PlayerModule
    {
        T m = (T)modules.FirstOrDefault(x => x is T);
        if (m == null || m == default(T)){
            module = null;
            return false;
        }
        module = m;
        return true;
    }

    #region  Unity Callbacks
    private void Awake()
    {
        InitModules();
    }
    private void Update()
    {
        ModulesUpdate();
    }
    private void FixedUpdate()
    {
        ModulesFixedUpdate();
    }
    private void LateUpdate()
    {
        ModulesLateUpdate();
    }
    #endregion

    #region Modules
    private void InitModules()
    {
        // find a better way to add camera to modules list
        modules = GetComponentsInChildren<PlayerModule>();
        foreach (var mod in modules)
        {
            if (!mod.Init(this)){
                Debug.LogError($"Could not initialize {mod.GetType().Name} module!");
            }
        }
    }
    private void ModulesUpdate()
    {
        foreach (PlayerModule mod in modules)
        {
            mod.OnUpdate(Time.deltaTime);
        }
    }
    private void ModulesFixedUpdate()
    {
        foreach (PlayerModule mod in modules)
        {
            mod.OnFixedUpdate(Time.fixedDeltaTime);
        }
    }
    private void ModulesLateUpdate()
    {
        foreach (PlayerModule mod in modules)
        {
            mod.OnLateUpdate(Time.deltaTime);
        }
    }
    #endregion

    private PlayerModule[] modules; 
}
