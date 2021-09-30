using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;
    private int m_score;

    [Header("Needed References")]
    public PlayerBehaviour playerPrefab;
    public Controller inputs;
    [SerializeField] private ControlPanel controlPanel;
    [SerializeField] private EndGamePanel endGamePanel;
    [SerializeField] private BallManager ballManager;
    [SerializeField] private Text m_scoreText;

    [Header("Scriptable Objects")]
    [Tooltip("List of inputs available in game")]
    [SerializeField] private List<Controller> inputsAvailables;
    [Tooltip("Win and lose condition of the games")]
    [SerializeField] private EndCondition endCondition;
    [Tooltip("Differentes settings to setup the game")]
    [SerializeField] private Setup setupRules;

    private bool m_isPlaying;
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
        Score = 0;

    }

    private void Start()
    {
        m_isPlaying = false;
        GameEvents.Instance.OnControlSelected += InitializeGame;
        GameEvents.Instance.OnAllBallsCreated += StartGame;

        ControlSelection(); //Here we call the ControlSelection at the launch of the game. 
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnControlSelected -= InitializeGame;
        GameEvents.Instance.OnAllBallsCreated -= StartGame;
    }

    /// <summary>
    /// Here we 're gonna check every update if the end condition are completed, then we will call the endgame func. 
    /// </summary>
    private void Update()
    {
        if (m_isPlaying)//We check if the game has started with th eboolean m_isPlaying
        {
            if (Score >= endCondition.WinScore) //If the score is equal to the win objective, we win
                EndGame(true);
            if (Score <= endCondition.LoseScore || ballManager.GetPossiblePositiveValue() < (endCondition.WinScore - Score)) //If the score is equal to the lose objective OR we don't have enough potential point on the table, we lose. 
                EndGame(false);
        }
    }

    /// <summary>
    /// Call the end of the game.
    /// </summary>
    /// <param name="_win">If the player di or did not win.</param>
    public void EndGame(bool _win)
    {
        endGamePanel.Init(_win);
        m_isPlaying = false;
        GameEvents.Instance.EndGame();
    }

    /// <summary>
    /// Function called when we want to go back to the Controller selection panel (After clicking on the "replay" button at the end of a game for exemple)
    /// </summary>
    public void ControlSelection()
    {
        endGamePanel.Display(false);
        controlPanel.Init(inputsAvailables);
    }

    /// <summary>
    /// Function called when the player selected a controler. We can then initialize the game.
    /// </summary>
    public void InitializeGame(Controller _inputs)
    {
        controlPanel.loadingAnimation.SetActive(true);
        controlPanel.panel.SetActive(false);
        Score = setupRules.startScore;
        inputs = _inputs;
        StartCoroutine(ballManager.Init(setupRules.nbWhiteBall, setupRules.nbRedBall, setupRules.nbYellowBall));
    }

    /// <summary>
    /// Function called when the game has been initialized. Start the game and allow the player to play. 
    /// </summary>
    public void StartGame()
    {
        m_isPlaying = true;
        controlPanel.Display(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit ! ");
        Application.Quit();
    }

    /// <summary>
    /// Add points to the global score
    /// </summary>
    /// <param name="_add">Number to add</param>
    public void AddScore(int _add)
    {
        Score += _add;
    }

    public static GameManager Instance
    {
        get
        {
            if (!m_instance)
            {
                m_instance = new GameManager();
            }
            return m_instance;
        }
    }

    private int Score
    {
        set
        {
            m_score = value;
            m_scoreText.text = "Score : "+m_score;
        }
        get
        {
            return m_score;
        }
    }
}
