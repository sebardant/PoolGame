using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    /*
     * An action is a ScriptableObject which can be added to a BallBehaviour to do something when a ball go out or is pushed in a hole. 
     */

    /// <summary>
    /// In this function (In inherited), a action can be done to affect the game state (add point to the score for example)
    /// </summary>
    /// <param name="_ball"></param>
    public abstract void Do(Ball _ball);
}

