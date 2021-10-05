using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameState State;
    
    public static GameManager instace;
    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        instace = this;
    }
    private void Start()
    {
        UpdateGameState(GameState.Onhold);
        
    }
    //Where we manage gamestates
    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.Onhold:
                break;                
            case GameState.IsStarted:
                GameUI.instance.levelProgressSlider.maxValue = PathCreator.instance.objectCount;
                GameUI.instance.UpdateScoreText();
                PlayerController.instance.StartMovement();
                break;
            case GameState.LevelComplete:
                PlayerScore.instance.SetHighScore();
                GameUI.instance.UpdateHighScoreText();
                GameUI.instance.IncremenetLevel();
                break;
            case GameState.LevelFail:
                Camera.main.transform.parent = null;
                PlayerScore.instance.SetHighScore();
                GameUI.instance.UpdateHighScoreText();
                PlayerPrefs.DeleteKey("playerLevelScore");
                PlayerPrefs.DeleteKey("CurrentLevel");
                //PlayerSounds.instance.PlayFallSound();
                break;
        }
        OnGameStateChanged?.Invoke(newState);
    }
    public void StartGame()
    {
        UpdateGameState(GameState.IsStarted);
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public enum GameState
    {
        Onhold,
        IsStarted,
        LevelComplete,
        LevelFail
    };
}
