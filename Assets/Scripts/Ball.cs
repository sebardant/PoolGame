using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private Rigidbody2D m_rigidbody2D;
    private BallBehaviour m_ballBehaviour;
    private SpriteRenderer m_spriteRenderer;
    [SerializeField]private List<AudioClip> sounds;
    private AudioSource audioSource;

    /// <summary>
    /// Ini the ball. 
    /// </summary>
    /// <param name="_ballBehaviour">Ball behaviour containing the different settings of the ball type</param>
    public void Init(BallBehaviour _ballBehaviour)
    {
        m_ballBehaviour = _ballBehaviour;
        if (m_ballBehaviour != null)
        {
            GameEvents.Instance.OnAddPointEdited += _ballBehaviour.Update;
            m_ballBehaviour.SetPositiveValue(true);
            audioSource = GetComponent<AudioSource>();
            m_spriteRenderer = GetComponent<SpriteRenderer>();
            m_spriteRenderer.color = m_ballBehaviour.baseColor;
            if (m_ballBehaviour.isPlayer)
            {
                PlayerBehaviour pb = Instantiate(GameManager.Instance.playerPrefab, transform);
                pb.Init(this);
            }
            m_rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }

    /// <summary>
    /// Function called when you want to add a force to the ball Rigidbody2D (Only use for the white(s) ball(s))
    /// </summary>
    /// <param name="direction">Direction of the shoot</param>
    /// <param name="power">Power of the shoot</param>
    public void Push(Vector2 direction, float power)
    {
        m_rigidbody2D.AddForce(direction * power * m_ballBehaviour.speed);
    }

    /// <summary>
    /// When the ball enter in collision with an other object, it play a random sound in the sounds list. 
    /// COULD BE IMPROVED: Currently, 2 ball who enter in collision both play a sound. Only one played sound would be needed. 
    /// </summary>
    /// <param name="collision">Description of the collision</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.clip = sounds[Random.Range(0, sounds.Count)];
        audioSource.Play();
    }

    /// <summary>
    /// Called when you want to destroy a ball. When arriving in a hole for example. The reason why we simply do not the gameobject here is that we need to delete the ball
    /// instance in the ball manager list containing all balls. 
    /// </summary>
    public void Destroy()
    {
        GameEvents.Instance.BallDestroyed(this);
    }

    /// <summary>
    /// Return the positive value of the ball. It correspond to the total of point that a ball can offer to the player by putting it in a hole or outside. 
    /// </summary>
    public int PositiveValue
    {
        get
        {
            return m_ballBehaviour.positiveValue;
        }
    }

    public Rigidbody2D Rigidbody2D { get => m_rigidbody2D; set => m_rigidbody2D = value; }
    public BallBehaviour BallBehaviour { get => m_ballBehaviour; set => m_ballBehaviour = value; }
}




