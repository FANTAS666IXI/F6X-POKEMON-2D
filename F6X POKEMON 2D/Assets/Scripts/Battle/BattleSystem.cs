using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [Header("Battle System Parameters")]
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud enemyHud;

    [Header("Console Log Settings")]
    public bool consoleLog;
    public Color logColor;
    private ConsoleLogSystemController consoleLogSystemController;

    private void Awake()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        consoleLogSystemController = GameObject.FindGameObjectWithTag("ConsoleLogSystem").GetComponent<ConsoleLogSystemController>();
    }

    private void Start()
    {
        SetUpBattle();
    }

    public void SetUpBattle()
    {
        ConsoleLog("Set Up Battle", true);
        playerUnit.SetUp();
        playerHud.SetData(playerUnit.Pokemon);
        enemyUnit.SetUp();
        enemyHud.SetData(enemyUnit.Pokemon);
    }

    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}