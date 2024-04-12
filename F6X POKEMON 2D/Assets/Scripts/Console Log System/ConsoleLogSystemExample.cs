using UnityEngine;

public class ConsoleLogSystemExample : MonoBehaviour
{
    // Set Console Log Settings Public && Declare Console Log System Controller
    [Header("Console Log Settings")]
    public bool consoleLog;
    public Color logColor;
    private ConsoleLogSystemController consoleLogSystemController;

    private void Awake()
    {
        // Reference Console Log System Controller By Tag
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Use Console Log ()
            ConsoleLog();
            ConsoleLog("Message 1");
            ConsoleLog("Message 2", false);
            ConsoleLog("Message 3", true);
            ConsoleLog("Message 4", false, 0);
            ConsoleLog("Message 5", true, 0);
            ConsoleLog("Message 6", false, 1);
            ConsoleLog("Message 7", true, 1);
            ConsoleLog("Message 8", false, 2);
            ConsoleLog("Message 9", true, 2);
            ConsoleLog("Message 10", false, 3);
            ConsoleLog("Message 11", true, 3);
            ConsoleLog("Message 12", false, -1);
            ConsoleLog("Message 13", true, -1);
        }
    }

    // Create Console Log ()
    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}