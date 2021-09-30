using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EndConditions", menuName = "ScriptableObjects/EndConditions")]
public class EndCondition : ScriptableObject
{
    [Header("Win Score Condition")]
    public int WinScore;
    [Header("Lose Score Condition")]
    public int LoseScore;
}
