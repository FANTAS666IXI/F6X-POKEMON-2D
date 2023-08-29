using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        ConsoleLog("Starting Game...");
    }

    public void ExitGame()
    {
        ConsoleLog("Closing Game...");
        Application.Quit();
    }

    public void ConsoleLog(string message, int frame = 1)
    {
        StackTrace stackTrace = new StackTrace();
        StackFrame stackFrame = stackTrace.GetFrame(frame);
        string callingScript = stackFrame.GetMethod().DeclaringType.Name;
        UnityEngine.Debug.Log("[" + callingScript + "]: " + message);
    }
}