using System;
using System.Linq;
using TMPro;
using UnityEngine;
using VCUE;

public class DebugConsoleGUI : MonoBehaviour
{
    public TMP_Text Title;
    public TMP_Text Logs;
    public TMP_InputField Input;


    private string SeverityToColor(LogSeverity severity)
    {
        switch (severity) 
        {
            case LogSeverity.Info:
                return "white";
            case LogSeverity.Debug:
                return "grey";
            case LogSeverity.Warning:
                return "yellow";
            case LogSeverity.Error:
                return "red";
            case LogSeverity.Critical:
                return "red";
            default:
                return "black";
        }

    }

    private void OnLog(LogMessage message)
    {
        Logs.text += $"<color={SeverityToColor(message.Severity)}>{message.ToString()}</color>\n";
    }
    private void OnSubmit(string data)
    {
        string[] words = Input.text.Split(' ');
        DebugConsole.InvokeCommand(words[0], words.Skip(1).ToArray());
        Input.text = "";
        Input.Select();
        Input.ActivateInputField();
    }

    private void Awake()
    {
        int c = DebugConsole.LoadAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        DebugConsole.OnLog += OnLog;
        DebugConsole.LogUnity = true;
        DebugConsole.Log($"Loaded {c} console commands.");

        Input.onSubmit.AddListener((string data) => { OnSubmit(data); });
    }
}
