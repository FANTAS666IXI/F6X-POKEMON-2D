using UnityEngine;
using System.Diagnostics;

public class GameController : MonoBehaviour
{
    [Header("Game Controller Settings")]
    private bool developerMode;

    [Header("Console Log Settings")]
    public bool consoleLog;
    public Color logColor;
    private ConsoleLogSystemController consoleLogSystemController;

    private void Awake()
    {
        InitializeVariables();
        ConsoleLog("Starting Game...", true);
        EnableDeveloperMode();
        LockCursor();
    }

    private void InitializeVariables()
    {
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();
    }

    private void EnableDeveloperMode()
    {
        developerMode = true;
        ConsoleLog("Developer Mode Enabled.", true);
    }

    private void DisableDeveloperMode()
    {
        developerMode = false;
        ConsoleLog("Developer Mode Disabled.", true);
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        DeveloperMode();
        ExitGame();
    }

    private void DeveloperMode()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            if (!developerMode)
                EnableDeveloperMode();
            else
                DisableDeveloperMode();
        }
    }

    private void ExitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ConsoleLog("Closing Game...", true);
            Application.Quit();
        }
    }

    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}