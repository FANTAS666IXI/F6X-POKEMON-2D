using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud enemyHud;

    public Color classColor;
    public bool consoleLog;
    private GameController gameController;

    private void Awake()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        SetUpBattle();
    }

    public void SetUpBattle()
    {
        ConsoleLog("Set Up Battle");
        playerUnit.SetUp();
        playerHud.SetData(playerUnit.Pokemon);
        enemyUnit.SetUp();
        enemyHud.SetData(enemyUnit.Pokemon);
    }

    private void ConsoleLog(string message)
    {
        if (consoleLog)
            gameController.ConsoleLogSystem($"{message}", classColor);
    }
}