using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public static PlayerCollisions instance;
    public bool isColliding;      
    private void Start()
    {
        instance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        //Check Player Fall or Not
        if(other.gameObject.CompareTag("FallCheck"))
        {
            PlayerSounds.instance.PlayFallSound();
            GameManager.instace.UpdateGameState(GameManager.GameState.LevelFail);           
        }    
    }
    private void OnCollisionEnter(Collision collision)
    {
        MeshRenderer meshrenderer = collision.gameObject.GetComponent<MeshRenderer>();
        var GameState = GameManager.instace.State;
        var isStartedState = GameManager.GameState.IsStarted;
        var isGameOverState = GameManager.GameState.LevelFail;
        var isLevelCompleteState = GameManager.GameState.LevelComplete;
        if(isColliding)
        {
            return;
        }
     //Compare collision game object with next targeted color from our list
        if (meshrenderer.material.color == PlayerMoveDecision.instance.ReturnNextColor())
        {
            //In order to prevent multiple collisions
            isColliding = true;
            //In order to get next target color
            if(PathCreator.instance.listIndex < PathCreator.instance.objectCount-1)
            {
                PathCreator.instance.listIndex++;
            }
            //Prevent to get more points on finish point
            if(GameState == isStartedState)
            {
                PlayerScore.instance.playerLevelProgressScore++;
                PlayerScore.instance.IncrementPlayerLevelScore();
                PlayerSounds.instance.PlayBounceSound();
                GameUI.instance.levelProgressSlider.value++;
                GameUI.instance.UpdateScoreText();
            }
            //To check level finish or not 
            if(PlayerScore.instance.playerLevelProgressScore == GameUI.instance.levelProgressSlider.maxValue && GameState != isLevelCompleteState)
            {
                PlayerSounds.instance.PlayLevelComplete();
                GameManager.instace.UpdateGameState(isLevelCompleteState);
            }
            StartCoroutine(Reset());
        }
        else
        {
            if(GameState != isGameOverState)
            {
                PlayerSounds.instance.PlayFallSound();
                GameManager.instace.UpdateGameState(isGameOverState);
            }
        }
    }
    
    IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();
        isColliding = false;
    }
    
}
