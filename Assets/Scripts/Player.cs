using UnityEngine;
using System.Linq;

public class Player : Entity
{
    public PlayerCamera playerCamera;

    public PlayerCameraController cameraController;
    public PlayerController controller;

    public CharacterController characterController;

    public float normalHeight = 1.85f;
    public float duckHeight = 0.9f;
    public float eyesHeight = 0.1f;

    public float height => characterController.height;


    public void SetHeight(float newHeight){
        characterController.height = newHeight;
        Vector3 center = characterController.center;
        center.y = newHeight*0.5f;
        characterController.center = center;
    }

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

        controller = GetModule<PlayerController>();
        cameraController = GetModule<PlayerCameraController>();
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
