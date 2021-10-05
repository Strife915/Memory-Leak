using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject levelStartMenu, gamePlayMenu, levelFailMenu, levelCompleteMenu;
    public Text firstTileCount, secondTileCount, thirdTileCount, scoreText, highScoreText, finishScreenScoreText, levelStartHighScoreText, currentLevelText, nextLevelText, levelCompleteHighScoreText, levelCompleteCurrentScoreText;
    public Slider levelProgressSlider;
    public int currentLevel;
    public static GameUI instance;


    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnHold;
        GameManager.OnGameStateChanged += GameManager_Isplaying;
        GameManager.OnGameStateChanged += GameManager_LevelFail;
        GameManager.OnGameStateChanged += GameManager_LevelComplete;
    }
    private void Start()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        instance = this;
        UpdateSlideBarLevelTexts();
    }

    private void GameManager_LevelComplete(GameManager.GameState state)
    {
        levelCompleteMenu.SetActive(state == GameManager.GameState.LevelComplete);
    }
    private void GameManager_LevelFail(GameManager.GameState state)
    {
        levelFailMenu.SetActive(state == GameManager.GameState.LevelFail);
    }
    private void GameManager_Isplaying(GameManager.GameState state)
    {
        gamePlayMenu.SetActive(state == GameManager.GameState.IsStarted);
    }
    private void GameManager_OnHold(GameManager.GameState state)
    {
        levelStartMenu.SetActive(state == GameManager.GameState.Onhold);
    }


    public void UpdateScoreText()
    {
        string score = PlayerScore.instance.playerLevelScore.ToString();
        scoreText.text = score;
        finishScreenScoreText.text = score;
        levelCompleteCurrentScoreText.text = score;
    }
    public void UpdateHighScoreText()
    {
        string highScore = PlayerScore.instance.playerHighScore.ToString();
        highScoreText.text = highScore;
        levelStartHighScoreText.text = highScore;
        levelCompleteHighScoreText.text = highScore;
    }
    public void UpdateSlideBarLevelTexts()
    {
        currentLevelText.text = (currentLevel+1).ToString();
        nextLevelText.text = (currentLevel + 2).ToString();
    }
    public void IncremenetLevel()
    {
        currentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
    }
  
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManager_OnHold;
        GameManager.OnGameStateChanged -= GameManager_Isplaying;
        GameManager.OnGameStateChanged -= GameManager_LevelFail;
        GameManager.OnGameStateChanged -= GameManager_LevelComplete;
    }

    



}
