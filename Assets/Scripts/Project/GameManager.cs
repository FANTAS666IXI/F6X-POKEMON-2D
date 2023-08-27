using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Closing game...");
        Application.Quit();
    }
}