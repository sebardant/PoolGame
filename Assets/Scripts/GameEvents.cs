using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    /*
     * A simple event system. Could be imrpoved with a dynamic handling of the diffrents type of events. 
     */
    public static GameEvents m_instance;
    private void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
        }
    }

    /// <summary>
    /// When the player did choose his controller
    /// </summary>
    public event UnityAction<Controller> OnControlSelected;
    public void ControlSelected(Controller _gi)
    {
        if (OnControlSelected != null)
        {
            OnControlSelected(_gi);
        }
    }

    /// <summary>
    /// When a ball fall in a hole
    /// </summary>
    public event UnityAction<Ball> OnBallIn;
    public void BallIn(Ball _ball)
    {
        if (OnBallIn != null)
        {
            OnBallIn(_ball);
        }
    }

    /// <summary>
    /// When a ball goes out of the table
    /// </summary>
    public event UnityAction<Ball> OnBallOut;
    public void BallOut(Ball _ball)
    {
        if (OnBallOut != null)
        {
            OnBallOut(_ball);
        }
    }
    /// <summary>
    /// Is at least one ball is still moving ?
    /// </summary>
    public event UnityAction<bool> OnBallMoving;
    public void BallMoving(bool _moving)
    {
        if (OnBallMoving != null)
        {
            OnBallMoving(_moving);
        }
    }

    /// <summary>
    /// When all balls has been created
    /// </summary>
    public event UnityAction OnAllBallsCreated;
    public void AllBallsCreated()
    {
        if (OnControlSelected != null)
        {
            OnAllBallsCreated();
        }
    }

    /// <summary>
    /// When a ball ned to be respawned
    /// </summary>
    public event UnityAction<Ball> OnRespawnBall;
    public void RespawnBall(Ball _ball)
    {
        if (OnRespawnBall != null)
        {
            OnRespawnBall(_ball);
        }
    }

    /// <summary>
    /// When a ball ned to be destroyed
    /// </summary>
    public event UnityAction<Ball> OnBallDestroyed;
    public void BallDestroyed(Ball _ball)
    {
        if (OnBallDestroyed != null)
        {
            OnBallDestroyed(_ball);
        }
    }

    /// <summary>
    /// When the game end
    /// </summary>
    public event UnityAction OnEndGame;
    public void EndGame()
    {
        if (OnEndGame != null)
        {
            OnEndGame();
        }
    }

    /// <summary>
    /// When an action AddPoint has been edited
    /// </summary>
    public event UnityAction OnAddPointEdited;
    public void AddPointEdited()
    {
        if (OnAddPointEdited != null)
        {
            OnAddPointEdited();
        }
    }

    public static GameEvents Instance
    {
        get
        {
            if (!m_instance)
            {
                m_instance = new GameEvents();
            }
            return m_instance;
        }
    }

}
