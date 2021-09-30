using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Respawn", menuName = "ScriptableObjects/Behaviours/Respawn")]
public class Respawn : Action
{
    /*
     * Action to make a ball respawn on the table
     */
    public override void Do(Ball _ball)
    {
        _ball.gameObject.SetActive(false);
        GameEvents.Instance.RespawnBall(_ball);
    }
}
