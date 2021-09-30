using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Setup", menuName = "ScriptableObjects/Setup")]

public class Setup : ScriptableObject
{
    /*
     * Start setup of the game. Could use ODIN and a dictionnary to improve display and allow a dynamic setup according to the existing ball type
     */
    public int nbWhiteBall;
    public int nbRedBall;
    public int nbYellowBall;
    public int startScore;
}
