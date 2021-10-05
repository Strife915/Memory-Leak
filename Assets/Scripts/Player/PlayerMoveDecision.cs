using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveDecision : MonoBehaviour
{
    public static PlayerMoveDecision instance;
    public Color nextTileColor;
    private void Start()
    {
        instance = this;
    }
    //private void Update()
    //{   
    //    if (GameManager.instace.State == GameManager.GameState.IsStarted)
    //    {
    //        nextTileColor = ReturnNextColor();
              //Its to see next color on editor
    //    }
    //}
    //Here we check next color in order to compare on collision
    public Color ReturnNextColor()
    {
        return PathCreator.instance.sortedPathColor[PathCreator.instance.listIndex];
    }
}
