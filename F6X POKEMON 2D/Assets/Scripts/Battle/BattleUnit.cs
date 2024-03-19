using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] PokemonBase _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;

    public Pokemon Pokemon { get; set; }

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

    private void ConsoleLog(string message)
    {
        if (consoleLog)
            gameController.ConsoleLogSystem($"{message}", classColor);
    }
}