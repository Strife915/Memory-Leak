using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore instance;
    public int playerHighScore;
    public int playerLevelScore;
    public int playerLevelProgressScore;

    private void Start()
    {
        instance = this;
        playerHighScore = PlayerPrefs.GetInt("playerHighScore");
        playerLevelScore = PlayerPrefs.GetInt("playerLevelScore");
    }
    public void IncrementPlayerLevelScore()
    {
        playerLevelScore++;
        PlayerPrefs.SetInt("playerLevelScore", playerLevelScore);
    }
    public void SetHighScore()
    {
        if (playerLevelScore > playerHighScore)
        {
            playerHighScore = playerLevelScore;
            PlayerPrefs.SetInt("playerHighScore", playerHighScore);
        }
            
    }
}
