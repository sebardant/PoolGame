using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Disappear", menuName = "ScriptableObjects/Behaviours/Disappear")]
public class Disappear : Action
{
    /*
     * Action to destroy a ball when she become useless after falling in a hole or getting outside.
     */
    public override void Do(Ball _ball)
    {
        _ball.Destroy();
    }
}
