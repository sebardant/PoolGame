using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Differents type of ball you can have in the game
/// </summary>
public enum BallType
{
    NONE, 
    WHITE, 
    RED, 
    YELLOW
}

public class BallManager : MonoBehaviour
{
    private List<Ball> balls = new List<Ball>();
    [SerializeField] private List<BallBehaviour> m_ballBehaviours;
    [SerializeField] private SpriteRenderer m_spawnArea;
    [SerializeField] private Ball m_ballPrefab;
    private bool m_moving = false;

    /// <summary>
    /// We consider that when the balls are moving, the player can't play. Here we check every update the velocity of each ball. 
    /// COULD BE IMPROVED: THIS is expensive so we could charge the ball itself to indicate that she is moving and whene on ball are moving, we allow the player to play. 
    /// </summary>
    private void Update()
    {
        bool newMoving = false;

        for(int i = 0; i < balls.Count; i++)
        {
            if(balls[i].Rigidbody2D.velocity != Vector2.zero)
            {
                newMoving = true;
            }
        }
        if(m_moving != newMoving)
        {
            m_moving = newMoving;
            GameEvents.Instance.BallMoving(m_moving);
        }
    }

    void Start()
    {
        GameEvents.Instance.OnBallIn += In;
        GameEvents.Instance.OnBallOut += Out;
        GameEvents.Instance.OnRespawnBall += RespawnBall;
        GameEvents.Instance.OnBallDestroyed += RemoveBall;

    }

    private void OnDestroy()
    {
        balls = new List<Ball>();
        GameEvents.Instance.OnBallIn -= In;
        GameEvents.Instance.OnBallOut -= Out;
        GameEvents.Instance.OnRespawnBall -= RespawnBall;
        GameEvents.Instance.OnBallDestroyed -= RemoveBall;

    }

    /// <summary>
    /// Initialize the balls on the table with different parameters given
    /// </summary>
    /// <param name="_nbWhiteBall">Nb of white ball wanted (Playable)</param>
    /// <param name="_nbRedBall">Nb of red ball wanted</param>
    /// <param name="_nbWYellowBall">Nb of yellow ball wanted</param>
    /// <returns></returns>
    public IEnumerator Init(int _nbWhiteBall, int _nbRedBall, int _nbWYellowBall)
    {
        RemoveAllBalls();

        for (int i= 0; i < _nbWhiteBall; i++)
        {
            yield return SpawnBall(BallType.WHITE);
        }

        for (int i = 0; i < _nbRedBall; i++)
        {
            yield return SpawnBall(BallType.RED);
        }

        for (int i = 0; i < _nbWYellowBall; i++)
        {
            yield return SpawnBall(BallType.YELLOW);
        }
        GameEvents.Instance.AllBallsCreated();
    }

    /// <summary>
    /// Find the right BallBehaviour corresponding to the ballType
    /// </summary>
    /// <param name="_ballType">Ball type wanted</param>
    /// <returns>the right ballBehaviour</returns>
    public BallBehaviour GetBallBehaviour(BallType _ballType)
    {
        foreach(BallBehaviour bb in m_ballBehaviours)
        {
            if (bb.ballType == _ballType)
                return bb;
        }
        return null;
    }

    /// <summary>
    /// Called by an event, execute every action indicated in the ball behaviour when the ball fall in a hole. 
    /// </summary>
    /// <param name="_ball">ball which fell</param>
    public void In(Ball _ball)
    {
        if (_ball.BallBehaviour)
        {
            foreach (Action bh in _ball.BallBehaviour.In)
            {
                bh.Do(_ball);
            }
        }
    }

    /// <summary>
    /// Called by an event, execute every action indicated in the ball behaviour when the ball goes out of the table. 
    /// </summary>
    /// <param name="_ball">ball goes out of the table</param>
    public void Out(Ball _ball)
    {
        if (_ball.BallBehaviour)
        {
            foreach (Action bh in _ball.BallBehaviour.Out)
            {
                bh.Do(_ball);
            }
        }
    }

    /// <summary>
    /// Create and spawn a new ball. 
    /// </summary>
    /// <param name="_ballType">Type of the new ball</param>
    /// <returns></returns>
    public IEnumerator SpawnBall(BallType _ballType)
    {
        Ball ball = Instantiate(m_ballPrefab, transform);
        yield return null;
        balls.Add(ball);
        ball.Init(GetBallBehaviour(_ballType));
        ball.transform.position = GetFreeArea(ball);
    }

    /// <summary>
    /// Respawn a ball at a free position on the table
    /// </summary>
    /// <param name="_ball">Ball to respawn</param>
    public void RespawnBall(Ball _ball)
    {
        Vector2 newPos = GetFreeArea(_ball);
        _ball.transform.position = newPos;
        _ball.gameObject.SetActive(true);
    }

    /// <summary>
    /// Remove a ball from the table. Delet it from balls list and destroy it
    /// </summary>
    /// <param name="_ball">the ball to remove</param>
    public void RemoveBall(Ball _ball)
    {
        balls.Remove(_ball);
        Destroy(_ball.gameObject);
    }

    /// <summary>
    /// Remove and destroy all the balls from the table
    /// </summary>
    public void RemoveAllBalls()
    {
        for (int i = balls.Count - 1; i >= 0; i--)
        {
            Ball ball = balls[i];
            RemoveBall(ball);
        }
    }


    /// <summary>
    /// Find a place where a ball can spawn without any contact with an other collider
    /// </summary>
    /// <param name="_ball">Ball to spawn to get its position</param>
    /// <returns>a position as a Vector2</returns>
    private Vector2 GetFreeArea(Ball _ball)
    {
        bool found = false;
        Vector2 newPos = Vector2.zero;
        while (!found)
        {
            newPos = new Vector2(Random.Range(m_spawnArea.bounds.min.x, m_spawnArea.bounds.max.x), Random.Range(m_spawnArea.bounds.min.y, m_spawnArea.bounds.max.y));
            Collider2D[] checkResult = Physics2D.OverlapCircleAll(newPos, _ball.GetComponent<Collider2D>().bounds.size.x / 2);

            if (checkResult.Length == 0)
            {
                found = true;
            }
        }
        return newPos;
    }

    /// <summary>
    /// Get the total of point a player can win with the balls on the table. 
    /// </summary>
    /// <returns></returns>
    public int GetPossiblePositiveValue()
    {
        int positiveValue = 0;
        for(int i = 0; i < balls.Count; i++)
        {
            positiveValue += balls[i].PositiveValue;
        }
        return positiveValue;
    }
}
