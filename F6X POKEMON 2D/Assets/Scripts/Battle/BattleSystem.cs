using UnityEngine;
using System.Collections;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy }

public class BattleSystem : MonoBehaviour
{
    [Header("Battle System Parameters")]
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    private BattleState state;
    private int currentAction;
    private int currentMove;

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
        StartCoroutine(SetUpBattle());
    }

    public IEnumerator SetUpBattle()
    {
        ConsoleLog("Set Up Battle", true);
        playerUnit.SetUp();
        playerHud.SetData(playerUnit.Pokemon);
        enemyUnit.SetUp();
        enemyHud.SetData(enemyUnit.Pokemon);
        dialogBox.SetMoveNames(playerUnit.Pokemon.Moves);
        yield return dialogBox.TypeDialog($"¡Un {enemyUnit.Pokemon.Base.Name} salvaje!");
        yield return new WaitForSeconds(1f);
        yield return dialogBox.TypeDialog($"¡Adelante, {playerUnit.Pokemon.Base.Name}!");
        yield return new WaitForSeconds(1f);
        PlayerAction();
    }

    private void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog($"¿Qué debería hacer {playerUnit.Pokemon.Base.Name}?"));
        dialogBox.EnableActionSelector(true);
    }

    private void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    private void Update()
    {
        if (state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
        else if (state == BattleState.PlayerMove)
        {
            HandleMoveSelection();
        }
    }

    private void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentAction == 0 || currentAction == 2)
                ++currentAction;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentAction == 1 || currentAction == 3)
                --currentAction;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentAction < 2)
                currentAction += 2;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentAction > 1)
                currentAction -= 2;
        }
        dialogBox.UpdateActionSelection(currentAction);
        if (Input.GetKeyDown(KeyCode.J))
        {
            switch (currentAction)
            {
                case 0:
                    ConsoleLog("LUCHAR.");
                    PlayerMove();
                    break;

                case 1:
                    ConsoleLog("POKÉMON.");
                    break;

                case 2:
                    ConsoleLog("MOCHILA.");
                    break;

                case 3:
                    ConsoleLog("HUIR.");
                    break;

                default:
                    ConsoleLog("!!Action Selected Unexpected¡¡", true, 2);
                    break;
            }
        }
    }

    private void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentMove < playerUnit.Pokemon.Moves.Count - 1)
                ++currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentMove > 0)
                --currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentMove < playerUnit.Pokemon.Moves.Count - 2)
                currentMove += 2;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentMove > 1)
                currentMove -= 2;
        }
        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Pokemon.Moves[currentMove]);
    }

    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}