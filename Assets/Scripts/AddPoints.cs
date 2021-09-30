using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddPoints", menuName = "ScriptableObjects/Behaviours/AddPoints")]
public class AddPoints : Action
{
    [Tooltip("Score to add when this action is called")]
    public int valueToAdd = 0;

    public override void Do(Ball _ball)
    {
        GameManager.Instance.AddScore(valueToAdd);
    }

    /// <summary>
    /// When the valueToAdd value is edited, we launch an event so the Positive Value of a BallBehaviour can be updated 
    /// </summary>
    private void OnValidate()
    {
        GameEvents.Instance.AddPointEdited();
    }
}
