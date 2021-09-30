using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Controller _inputs;
    bool isPlayable = false;
    public CueBehaviour cue;
    public Ball ball;
    public float power = 0;
    public SpriteRenderer cueRenderer;

    private void Start()
    {
        IsPlayable = false;
        GameEvents.Instance.OnAllBallsCreated += BallsCreated;
        GameEvents.Instance.OnBallMoving += BallMoving;
        GameEvents.Instance.OnEndGame += Destroy;

    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnAllBallsCreated -= BallsCreated;
        GameEvents.Instance.OnBallMoving -= BallMoving;
        GameEvents.Instance.OnEndGame -= Destroy;
    }

    /// <summary>
    /// Control the cue position rotation and loading state only when IsPlayable is true
    /// </summary>
    void Update()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        cue.lineRenderer.enabled = false;

        if (IsPlayable)
        {
            cue.lineRenderer.enabled = true;
            cue.transform.rotation = _inputs.Rotate(cue.transform);
            power = cue.IsUsingCue(Input.GetKey(_inputs.hitButton));
            if (power != -1)
            {
                ball.Push(cue.transform.right, power);
            }
        }
    }

    public void Init(Ball _ball)
    {
        _inputs = GameManager.Instance.inputs;
        ball = _ball;
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void Enable(bool _bool)
    {
        IsPlayable = _bool;
    }

    private void BallsCreated()
    {
        Enable(true);
    }

    private void BallMoving(bool _bool)
    {
        Enable(!_bool);
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public bool IsPlayable
    {
        set
        {
            isPlayable = value;

            if (isPlayable)
            {
                cueRenderer.enabled = true;
            }
            else
            {
                cueRenderer.enabled = false;
            }
        }
        get
        {
            return isPlayable;
        }
    }
}
