using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Color classColor;

    private void Start()
    {
        ConsoleLog("Starting Game...");
    }

    public void ExitGame()
    {
        ConsoleLog("Closing Game...");
        Application.Quit();
    }

    private void ConsoleLog(string message)
    {
        MainConsoleLog($"{message}", classColor);
    }

    public void MainConsoleLog(string message, Color classColor, int frame = 1)
    {
        StackTrace stackTrace = new StackTrace();
        StackFrame stackFrame = stackTrace.GetFrame(frame);
        string callingScript = stackFrame.GetMethod().DeclaringType.Name;
        string stringClassColor = ("#" + ColorUtility.ToHtmlStringRGBA(classColor));
        UnityEngine.Debug.Log($"<b>[<color={stringClassColor}>{callingScript}</color>]: {message}</b>");
    }
}