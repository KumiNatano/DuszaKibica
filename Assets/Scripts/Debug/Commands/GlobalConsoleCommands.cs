using UnityEditor;
using UnityEngine;
using VCUE;

public static class GlobalConsoleCommands
{
    [ConsoleCommand(Name: "fps")]
    public static void OnFps()
    {
        int fps = Mathf.RoundToInt(1.0f / Time.deltaTime);
        DebugConsole.Log($"{fps} FPS");
    }
    [ConsoleCommand(Name: "fps.limit")]
    public static void OnFpsLimit(string framerate)
    {
        int fps = int.Parse(framerate);
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = fps;
        DebugConsole.Log($"fps.limit \"{fps}\"");
    }
    [ConsoleCommand(Name: "version")]
    public static void OnVersion()
    {
        DebugConsole.Log($"\nGame Version: {Application.version}\nUnity Version: {Application.unityVersion}");
    }
    [ConsoleCommand(Name: "quit")]
    public static void OnQuit()
    {
        DebugConsole.Log("Quitting..");
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
