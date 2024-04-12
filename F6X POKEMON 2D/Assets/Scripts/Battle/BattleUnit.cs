using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [Header("Battle Unit Settings")]
    [SerializeField] bool isPlayerUnit;

    [Header("Battle Unit Parameters")]
    [SerializeField] PokemonBase _base;
    [SerializeField] int level;

    public Pokemon Pokemon { get; set; }

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

    public void SetUp()
    {
        Pokemon = new Pokemon(_base, level);
        if (isPlayerUnit)
        {
            ConsoleLog("Set Up Player " + Pokemon.Base.Name + " at Level " + Pokemon.Level + ".");
            GetComponent<Image>().sprite = Pokemon.Base.BackSprite;
        }
        else
        {
            ConsoleLog("Set Up Enemy " + Pokemon.Base.Name + " at Level " + Pokemon.Level + ".");
            GetComponent<Image>().sprite = Pokemon.Base.FrontSprite;
        }
    }

    private void ConsoleLog(string message = "Test", bool showFrame = false, int infoLevel = 0)
    {
        if (consoleLog)
            consoleLogSystemController.ConsoleLogSystem(message, logColor, showFrame, infoLevel);
    }
}