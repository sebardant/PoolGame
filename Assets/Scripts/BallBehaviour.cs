using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BallBehaviour", menuName = "ScriptableObjects/BallBehaviour")]
public class BallBehaviour : ScriptableObject
{
    /*
     * Different setting of a ball. 
     */

    [Tooltip("Name of the ball type")]
    public string Name;
    [Tooltip("Ball type corresponding to this behaviour")]
    public BallType ballType;
    [Tooltip("Color of the ball")]
    public Color baseColor;
    [Tooltip("Speed of the ball when shooted (Only the IsPlayer ball)")]
    public float speed = 0;
    [Tooltip("Is a ball is playable ?")]
    public bool isPlayer;
    [Tooltip("List of action to execute when a ball fall in a hole")]
    public List<Action> In;
    [Tooltip("List of action to execute when a ball goes out of the table")]
    public List<Action> Out;

    [Tooltip("Positive gain probability from the ball (DON'T EDIT)")]
    public int positiveValue = 0;
    private bool positiveValueCalculatedFromGame;

    private void Awake()
    {
        GameEvents.Instance.OnAddPointEdited += Update;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnAddPointEdited -= Update;

    }

    public void Update()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        SetPositiveValue();
    }

    /// <summary>
    /// Calculate the potential value a ball can offer to a player. 
    /// SHOULD BE IMPROVED: Easier in french, sorry. Actuellement, le total comprend les points potentiel gagnable lorsque la boule rentre dans un trou ET sort du terrain. Par exemple, une 
    /// balle qui donne 1 point en allant dans un trou et 1 point en sortant du terrain, sa Positive value sera de 2. Or, si elle est detruite apres etre entrée dans un trou, alors elle aura fait gagner 1 point seulement et 
    /// elle ne pourra jamais faire gagner au joueur le second. Cela pose donc un probleme au calcule fait dans le GameManager pour savoir si la partie est encore gagnable. 
    /// </summary>
    /// <param name="_fromGame">Has the value been calculated from the BallManager already</param>
    public void SetPositiveValue(bool _fromGame = false)
    {
        if (_fromGame && positiveValueCalculatedFromGame)
        {
            return;
        }

        int newPositiveValue = 0;
        for (int i = 0; i < In.Count; i++)
        {
            if (In[i].GetType().Equals(typeof(AddPoints)))
            {
                AddPoints ap = In[i] as AddPoints;
                if (ap.valueToAdd > 0)
                {
                    newPositiveValue += ap.valueToAdd;
                }
            }
        }
        
        for (int i = 0; i < Out.Count; i++)
        {
            if (Out[i].GetType().Equals(typeof(AddPoints)))
            {
                AddPoints ap = Out[i] as AddPoints;
                if (ap.valueToAdd > 0)
                {
                    newPositiveValue += ap.valueToAdd;
                }
            }
        }

        if(_fromGame)
            positiveValueCalculatedFromGame = true;
        positiveValue = newPositiveValue;
    }
}
