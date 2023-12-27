using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{

    public List<Level> levels = new List<Level>();
    public Player player;
    Level currentLevel;

    int level = 1;

    private void Start()
    {
        UIManager.Instance.OpenMainMenuUI();
        LoadLevel();
    }

    public void LoadLevel()
    {
        LoadLevel(level);
        OnInit();
    }

    public void LoadLevel(int indexLevel)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levels[indexLevel - 1]);
    }

    public void OnInit()
    {
        player.transform.position = currentLevel.startPoint.position;
        player.OnInit();
    }


    public void OnStart()
    {
        GameManager.Instance.ChangeState(GameState.GamePlay);
    }

    public void OnFinish()
    {
        UIManager.Instance.OpenFinishUI();
        GameManager.Instance.ChangeState(GameState.Finish);
    }

    public void NextLevel() 
    {
        level++;
        LoadLevel();
    }
}
