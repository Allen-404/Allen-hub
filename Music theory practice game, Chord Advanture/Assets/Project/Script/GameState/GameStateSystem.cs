using UnityEngine;
using System.Collections;

public class GameStateSystem : MonoBehaviour
{
    public static GameStateSystem instance;

    public GameState state;
    public int subIndex;

    private void Awake()
    {
        instance = this;
    }

    public void GoNextState()
    {
        switch (state)
        {
            case GameState.None:
                break;
            case GameState.ShowUp:
                StartState(GameState.Listen_All);
                break;
            case GameState.Listen_All:
                StartState(GameState.Input_All);
                break;
            case GameState.Input_All:
                break;
            case GameState.Validation://slow learn
                break;
            case GameState.ShowResult:
                break;
        }
    }

    public void StartState(GameState newState)
    {
        state = newState;
        switch (state)
        {
            case GameState.None:
                break;
            case GameState.ShowUp:
                GameSystem.instance.Start_Showup();
                break;
            case GameState.Listen_All:
                GameSystem.instance.Start_Listen_All();
                break;
            case GameState.Input_All:
                GameSystem.instance.Start_Input_All();
                break;
            case GameState.Validation:
                break;
            case GameState.ShowResult:
                break;
        }
    }
}