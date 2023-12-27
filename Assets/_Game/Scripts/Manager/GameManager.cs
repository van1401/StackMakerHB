using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState { MainMenu, GamePlay, Finish}

public class GameManager : Singleton<GameManager>
{

    private GameState state;
    private void Awake()
    {
        //setup tong quan game
        //set up data

        ChangeState(GameState.MainMenu);
    }


    public void ChangeState(GameState gameState)
    {
        state = gameState;
    }

    public bool IsState(GameState gameState)
    {
        return state == gameState;
    }


}
