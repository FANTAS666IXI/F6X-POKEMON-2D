using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class ConsoleLogSystemController : MonoBehaviour
{
    [Header("Console Log System Settings")]
    public bool consoleLog;
    public bool consoleLogSystem;
    public Color logColor;
    private int currentFrame;
    private string logFilePath;
    private readonly string infoLevelException = "Info Level Must Be Between 0 And 2";

    private void Awake()
    {
        InitializeLogFilePath();
        ConsoleLog("Console Log System Ready!", true);
    }

    private void InitializeLogFilePath()
    {
        logFilePath = Path.Combine(Path.GetDirectoryName(Application.dataPath), "ConsoleLogSystem");
        if (!Directory.Exists(logFilePath))
            Directory.CreateDirectory(logFilePath);
        logFilePath = Path.Combine(logFilePath, "Log-" + DateTime.Now.ToString("yy-MM-dd-HH-mm") + ".log");
        using (StreamWriter writer = new StreamWriter(logFilePath))
        {
            writer.WriteLine("--- Console Log System Log ---");
        }
    }

    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }

    public void ConsoleLogSystem(string message, Color logColor, bool showFrame, int infoLevel, int traceFrame = 1)
    {
        if (consoleLogSystem)
        {
            currentFrame = Time.frameCount;
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(traceFrame);
            string callingScript = stackFrame.GetMethod().DeclaringType.Name;
            logColor.a = 1;
            string stringLogColor = "#" + ColorUtility.ToHtmlStringRGBA(logColor);
            string logMessage = $"<b>[<color={stringLogColor}>{callingScript}</color>]: ";
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                if (-1 < infoLevel && infoLevel < 3)
                {
                    logMessage += showFrame ? $"FRM ({currentFrame}) " : "";
                    logMessage += $"{message}</b>\n";
                    writer.WriteLine("[" + callingScript + "] FRM (" + currentFrame + ") " + message);
                }
                else
                {
                    logMessage += $"FRM: {currentFrame} ";
                    logMessage += $"{infoLevelException}</b>\n";
                    writer.WriteLine("[" + callingScript + "] FRM (" + currentFrame + ") " + infoLevelException);
                }
            }
            switch (infoLevel)
            {
                case 0:
                    UnityEngine.Debug.Log(logMessage);
                    break;
                case 1:
                    UnityEngine.Debug.LogWarning(logMessage);
                    break;
                case 2:
                    UnityEngine.Debug.LogError(logMessage);
                    break;
                default:
                    UnityEngine.Debug.LogError(logMessage);
                    break;
            }
        }
    }
}